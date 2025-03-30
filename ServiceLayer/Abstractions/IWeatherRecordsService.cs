using DataLayer.Entities;
using ServiceLayer.Models;
using System.Linq.Expressions;

namespace ServiceLayer.Abstractions
{
    public interface IWeatherRecordsService
    {
        /// <summary>
        /// Создание списка записей
        /// </summary>
        /// <param name="recordDtos"></param>
        /// <returns></returns>
        public Task CteateRecordsAsync(IEnumerable<WeatherRecordDto> recordDtos);

        /// <summary>
        /// Получение спсиска записей
        /// </summary>
        /// <param name="filter">условие фильрации</param>
        /// <param name="pageSize">размер страницы</param>
        /// <param name="pageNumber">номер страницы с 0</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Объект PageDto с найденными записями</returns>
        public Task<PageDto<WeatherRecordDto>> GetWeatherRecordsAsync
            (Expression<Func<WeatherRecord, bool>> filter, int pageSize, int pageNumber, CancellationToken cancellationToken);
        
        /// <summary>
        /// Получение списка всех годов, по которым есть хотя бы одна запись
        /// </summary>
        /// <returns>Список годов</returns>
        public Task<IEnumerable<int>> GetYearsHavingData();
    }
}
