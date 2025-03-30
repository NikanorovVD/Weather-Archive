using DataLayer.Entities;
using ServiceLayer.Models;
using System.Linq.Expressions;

namespace ServiceLayer.Abstractions
{
    public interface IWeatherRecordsService
    {
        public Task CteateRecordsAsync(IEnumerable<WeatherRecordDto> recordDtos);
        public Task<PageDto<WeatherRecordDto>> GetWeatherRecordsAsync
            (Expression<Func<WeatherRecord, bool>> filter, int pageSize, int pageNumber, CancellationToken cancellationToken);
        public Task<IEnumerable<int>> GetYearsHavingData();
    }
}
