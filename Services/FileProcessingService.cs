using InfotecsTestWebApi.Models;
using InfotecsTestWebApi.Models.Entities;
using InfotecsTestWebApi.Services.Csv;
using System.ComponentModel.DataAnnotations;
namespace InfotecsTestWebApi.Services;

public class FileProcessingService
{
    private readonly CsvAggregator _csvAggregator;
    private readonly CsvReading _csvReading;
    private readonly CsvValidator _csvValidator;
    private readonly ApplicationContext _db;
    public FileProcessingService(
         CsvReading csvReading,
         CsvValidator validator,
         CsvAggregator aggregator,
         ApplicationContext db)
    {
        _csvReading = csvReading;
        _csvValidator = validator;
        _csvAggregator = aggregator;
        _db = db;
    }
    public string ProcessFile(string filePath, string fileName)
    {
        var records = _csvReading.readcsv(filePath);
        var validationResult = _csvValidator.Validate(records);
        if (!validationResult.IsValid)
            return validationResult.ErrorMessage!;
        var aggregationResult = _csvAggregator.Aggregate(records);
        using var transaction = _db.Database.BeginTransaction();
        try
        {
            var existingFile = _db.Files
                .FirstOrDefault(f => f.FileName == fileName);

            if (existingFile != null)
            {
                var oldValues = _db.Values
                    .Where(v => v.FileId == existingFile.Id);

                var oldResult = _db.Results
                    .Where(r => r.FileId == existingFile.Id);

                _db.Values.RemoveRange(oldValues);
                _db.Results.RemoveRange(oldResult);
                _db.Files.Remove(existingFile);

                _db.SaveChanges();
            }
            var fileEntity = new FileEntity
            {
                FileName = fileName
            };
            _db.Files.Add(fileEntity);
            _db.SaveChanges();
            var valuesEntities = records.Select(r => new ValuesEntity
            {
                DateTimeStart = r.DateTimeStart,
                TimeCompletion = r.TimeCompletion,
                Value = r.Value,
                FileId = fileEntity.Id
            }).ToList();

            var resultEntity = new ResultsEntity
            {
                MinDate = aggregationResult.MinDate,
                Delta = aggregationResult.Delta,
                AvgTime = aggregationResult.AvgTime,
                AvgValue = aggregationResult.AvgValue,
                Median = aggregationResult.Median,
                MaxValue = aggregationResult.MaxValue,
                MinValue = aggregationResult.MinValue,
                FileId = fileEntity.Id
            };
            _db.Values.AddRange(valuesEntities);
            _db.Results.AddRange(resultEntity);
            _db.SaveChanges();
            transaction.Commit();
            Console.WriteLine($"Сохраняем значения {valuesEntities.Count}");
            return "Файл успешно обработан";
        }

        catch
        {
            transaction.Rollback();
            throw;
        }
    }

}
