namespace InfotecsTestWebApi.Models.Entities
{
    public class ValuesEntity
    {
        public int Id { get; set; }
        public DateTime DateTimeStart { get; set; }
        public int TimeCompletion { get; set; }
        public double Value { get; set; }
        public int FileId { get; set; }
        public FileEntity File { get; set; }
    }
}
