namespace InfotecsTestWebApi.Models
{
    public class FilterModel
    {
        public string? FileName { get; set; }
        public DateTime? MinDateFrom { get; set; }
        public DateTime? MinDateTo { get; set; }
        public double? AvgValueFrom { get; set; }
        public double? AvgValueTo { get; set; }
        public double? AvgTimeFrom { get; set; }
        public double? AvgTimeTo { get; set; }
    }
}
