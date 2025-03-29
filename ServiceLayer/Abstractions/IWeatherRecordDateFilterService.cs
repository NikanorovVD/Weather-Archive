using DataLayer.Entities;
using System.Linq.Expressions;

namespace ServiceLayer.Abstractions
{
    public interface IWeatherRecordDateFilterService
    {
        /// <summary>
        /// Создаёт Expression для фильтрации WeatherRecord по дате
        /// </summary>
        /// <param name="year">Год</param>
        /// <param name="month">Месяц, если не указан фильтр только по году</param>
        /// <returns></returns>
        public Expression<Func<WeatherRecord, bool>> CreateFilter(int year, int? month);
        
    }
}
