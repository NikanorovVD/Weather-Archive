using DataLayer.Entities;
using ServiceLayer.Abstractions;
using System.Linq.Expressions;

namespace ServiceLayer.Services
{
    public class WeatherRecordDateFilterService : IWeatherRecordDateFilterService
    {
        public Expression<Func<WeatherRecord, bool>> CreateFilter(int year, int? month)
        {
            if (month != null) return r => r.DateTime.Year == year && r.DateTime.Month == month;
            else return r => r.DateTime.Year == year;
        }
    }
}
