using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer;
using DataLayer.Entities;
using DataLayer.QueryObjects;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Abstractions;
using ServiceLayer.Models;
using System.Linq.Expressions;

namespace ServiceLayer.Services
{
    public class WeatherRecordsService : IWeatherRecordsService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public WeatherRecordsService(AppDbContext dbCintext, IMapper mapper)
        {
            _dbContext = dbCintext;
            _mapper = mapper;
        }

        public async Task CteateRecordsAsync(IEnumerable<WeatherRecordDto> recordDtos)
        {
            IEnumerable<WeatherRecord> weatherRecords = _mapper.Map<IEnumerable<WeatherRecord>>(recordDtos);
            await _dbContext.WeatherRecords.AddRangeAsync(weatherRecords);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PageDto<WeatherRecordDto>> GetWeatherRecordsAsync(Expression<Func<WeatherRecord, bool>> filter, int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
            Page<WeatherRecord> page = _dbContext.WeatherRecords
                .Where(filter)
                .OrderBy(r => r.DateTime)
                .Page(pageSize, pageNumber);

            return new PageDto<WeatherRecordDto>
            {
                Values = await page.Query.ProjectTo<WeatherRecordDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                PageNumber = page.PageNumber,
                PageSize = page.PageSize,
                TotalPages = page.TotalPages
            };               
        }

        public async Task<IEnumerable<int>> GetYearsHavingData()
        {
            return await _dbContext.WeatherRecords
                .Select(r => r.DateTime.Year)
                .Distinct()
                .OrderBy(y => y)
                .ToListAsync();
        }
    }
}
