using InfotecsTestWebApi.Models;
using InfotecsTestWebApi.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace InfotecsTestWebApi.Services
{
    public class ValuesService
    {
        private readonly ApplicationContext _db;
        public ValuesService(ApplicationContext db)
        {
            _db = db; 
        }
        public List<ValueDto> GetValuesByFile(string fileName)
        {
            return _db.Values
                .Include(v => v.File)
                .Where(v => v.File.FileName == fileName)
                .OrderByDescending(v => v.DateTimeStart)
                .Take(10)
                .Select(v => new ValueDto
                {
                    DateTimeStart = v.DateTimeStart,
                    TimeCompletion = v.TimeCompletion,
                    Value = v.Value
                }).ToList();
        }
    }
}
