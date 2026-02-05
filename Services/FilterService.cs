using InfotecsTestWebApi.Models;
using InfotecsTestWebApi.Models.Dto;
using InfotecsTestWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfotecsTestWebApi.Services
{
    public class FilterService
    {
        public readonly ApplicationContext _db;
        public FilterService(ApplicationContext db)
        { 
            _db = db; 
        }
        public List<ResultDto> GetFilteredData(FilterModel filter)
        {
            var query = _db.Results.Include(r => r.File).AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter.FileName))
                query = query.Where(r => r.File.FileName == filter.FileName);
            if (filter.MinDateFrom.HasValue)
            {
                query = query.Where(r => r.MinDate >= filter.MinDateFrom.Value);
            }

            if (filter.MinDateTo.HasValue)
            {
                query = query.Where(r => r.MinDate <= filter.MinDateTo.Value);
            }

            if (filter.AvgValueFrom.HasValue)
            {
                query = query.Where(r => r.AvgValue >= filter.AvgValueFrom.Value);
            }

            if (filter.AvgValueTo.HasValue)
            {
                query = query.Where(r => r.AvgValue <= filter.AvgValueTo.Value);
            }

            if (filter.AvgTimeFrom.HasValue)
            {
                query = query.Where(r => r.AvgTime >= filter.AvgTimeFrom.Value);
            }

            if (filter.AvgTimeTo.HasValue)
            {
                query = query.Where(r => r.AvgTime <= filter.AvgTimeTo.Value);
            }
            return query.Select(r => new ResultDto
            {
                Id = r.Id,
                MinDate = r.MinDate,
                Delta = r.Delta,
                AvgTime = r.AvgTime,
                AvgValue = r.AvgValue,
                Median = r.Median,
                MaxValue = r.MaxValue,
                MinValue = r.MinValue,
                FileName = r.File.FileName
            }).ToList();
        }
    }
}
