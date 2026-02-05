using System.IO;
using System.Globalization;
using CsvHelper;
using InfotecsTestWebApi.Models;
namespace InfotecsTestWebApi.Services.Csv
{
    public class CsvReading
    {
        public List<CsvValue> readcsv(string filename)
        {
            var config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvReader(reader, config))
            {
             
                var records = new List<CsvValue>();
                csv.Read();
                csv.ReadHeader();
                int lineNumber = 1;
                while (csv.Read())
                {
                    try
                    {
                        var date = csv.GetField<DateTime>("Date");
                        var record = new CsvValue
                        {
                            DateTimeStart = DateTime.SpecifyKind(date, DateTimeKind.Utc),
                            TimeCompletion = csv.GetField<int>("ExecutionTime"),
                            Value = csv.GetField<double>("Value")
                        };
                        records.Add(record);
                        lineNumber++;


                    }
                    catch
                    {
                        throw new Exception($"Ошибка формата в строке{lineNumber}");
                    }
                }
                return records;
            }
        }
    }
}
