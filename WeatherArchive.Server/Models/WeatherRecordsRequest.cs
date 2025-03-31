namespace WeatherArchive.Server.Models
{
    public class WeatherRecordsRequest
    {
        /// <summary>
        /// Год для фильтрации
        /// </summary>
        public int Year {  get; set; }

        /// <summary>
        /// Месяц для фильтрации (1 - январь), если не указан, то фильтр только по году
        /// </summary>
        public int? Month { get; set; }

        /// <summary>
        /// Количество записей на одной странице
        /// </summary>
        public int PageSize { get; set; } = 30;

        /// <summary>
        /// Номер страницы (нумерация с нуля)
        /// </summary>
        public int PageNumber { get; set; } = 0;  
    }
}
