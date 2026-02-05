using InfotecsTestWebApi.Models;
namespace InfotecsTestWebApi.Services.Csv
{
    public class CsvAggregator
    {
        public double Delta { get; set; }
        public DateTime MinDate { get; set; }
        public double AvgTime { get; set; }
        public double AvgValue { get; set; }
        public double Median { get; set; }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }

        public CsvAggregator Aggregate(List<CsvValue> records)
        {
            var minDate = records.Min(r => r.DateTimeStart);
            var maxDate = records.Max(r => r.DateTimeStart);
            var avgCompletionTime = records.Average(r => r.TimeCompletion);
            var valuesOrdered = records
                .Select(r => r.Value)
                .OrderBy(v => v)
                .ToList();
            double median;
            int count = valuesOrdered.Count();
            if (count % 2 == 0)
                median = (valuesOrdered[count / 2 - 1] + valuesOrdered[count / 2]) / 2.0;
            else
                median = (valuesOrdered[count / 2]);

            return new CsvAggregator
            {
                Delta = (maxDate - minDate).TotalSeconds,
                MinDate = minDate,
                AvgTime = avgCompletionTime,
                AvgValue = valuesOrdered.Average(),
                Median = median,
                MaxValue = valuesOrdered.Max(),
                MinValue = valuesOrdered.Min()
            };
        }
    }
}
