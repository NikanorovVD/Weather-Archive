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

        public async Task<IEnumerable<WeatherRecordDto>> GetWeatherRecordsAsync(Expression<Func<WeatherRecord, bool>> filter, int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
            return await _dbContext.WeatherRecords
                .Where(filter)
                .Page(pageSize, pageNumber)
                .OrderBy(r => r.DateTime)
                .ProjectTo<WeatherRecordDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }       
    }
}
