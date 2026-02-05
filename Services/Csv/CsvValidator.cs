using InfotecsTestWebApi.Models;
namespace InfotecsTestWebApi.Services.Csv
{
    public class CsvValidator
    {
        public ValidationResult Validate(List<CsvValue> records)
        {
            if (records.Count > 10000)
                return ValidationResult.Fail("Превышено максимальное количество строк файла");

            if (records.Count < 1)
                return ValidationResult.Fail("Файл без записей");
            
            if (records == null)
                return ValidationResult.Fail("Данные отсутствуют");

            DateTime minAllowedDate = new DateTime(2000, 1, 1);
            DateTime now = DateTime.Now;
            int lineNumber = 1;
            foreach(var record in records)
            {
                if(record.DateTimeStart > now || record.DateTimeStart < minAllowedDate)
                    return ValidationResult.Fail($"Некоректная дата на линии {lineNumber}");
                if (record.TimeCompletion < 0)
                    return ValidationResult.Fail($"Некоректное время выполнения на линии {lineNumber}");

                if (record.Value < 0)
                    return ValidationResult.Fail($"Некоректное значение показателя на линии {lineNumber}");
                lineNumber++;
            }
            return ValidationResult.Success();


          
        }
    }
}
