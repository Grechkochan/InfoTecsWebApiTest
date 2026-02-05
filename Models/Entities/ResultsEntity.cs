namespace InfotecsTestWebApi.Models.Entities
{
    public class ResultsEntity
    {
        public int Id { get; set; }
        public double Delta {  get; set; }
        public DateTime MinDate { get; set; }
        public double AvgTime { get; set; }
        public double AvgValue { get; set; }
        public double Median {  get; set; }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public int FileId { get; set; }
        public FileEntity File { get; set; }
    }
}
