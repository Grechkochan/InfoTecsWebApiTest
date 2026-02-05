namespace InfotecsTestWebApi.Models.Entities
{
    public class FileEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public ICollection<ResultsEntity> Results { get; set; }
        public ICollection<ValuesEntity> Values { get; set; }
    }
}
