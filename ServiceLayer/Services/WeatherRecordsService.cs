using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer;
using DataLayer.Entities;
using DataLayer.QueryObjects;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ServiceLayer.Abstractions;
using ServiceLayer.Models;
using ServiceLayer.Models.Errors;
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
            try
            {
                await _dbContext.WeatherRecords.AddRangeAsync(weatherRecords);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex) 
            {
                if (ex.InnerException is PostgresException pgEx &&
                    pgEx.SqlState == PgErrorCodes.DuplicatePKCode)
                        throw new ValidationException(new ValidationError(new(){ { "Files", ["Conflicting data. Records must not have the same datetime as the existing records"] } }));
                else throw;
            }
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
