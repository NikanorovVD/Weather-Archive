using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Abstractions;
using ServiceLayer.Models;
using WeatherArchive.Server.Models;

namespace WeatherArchive.Server.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherArchiveController : ControllerBase
    {
        private readonly IParseWeatherXlsService _parseService;
        private readonly IWeatherRecordsService _weatherRecordsService;
        private readonly IWeatherRecordDateFilterService _filterService;

        public WeatherArchiveController(IParseWeatherXlsService parseService, IWeatherRecordsService weatherRecordsService, IWeatherRecordDateFilterService filterService)
        {
            _parseService = parseService;
            _weatherRecordsService = weatherRecordsService;
            _filterService = filterService;
        }

        /// <summary>
        /// Загрузка архивов погодных условий на сервер
        /// </summary>
        /// <param name="files">Файлы с данными погодных условий в формате .xls или .xlsx</param>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadArchiveFilesAsync(IEnumerable<IFormFile> files)
        {
            IEnumerable<WeatherRecordDto> records = files.SelectMany(f => _parseService.ParseXls(f.OpenReadStream()));
            await _weatherRecordsService.CteateRecordsAsync(records);
            return Ok();
        }

        /// <summary>
        /// Получение архивных погодных данных
        /// </summary>
        /// <param name="year">Год для фильтрации</param>
        /// <param name="month">Месяц для фильтрации (1 - январь), если не указан, то фильтр только по году</param>
        /// <param name="cancellationToken"></param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="pageNumber">Номер страницы (нумерация с нуля)</param>
        /// <returns></returns>
        [HttpGet("archive")]
        public async Task<WeatherRecordsResponse> GetWeatherRecordsAsync(int year, int? month, CancellationToken cancellationToken, int pageSize = 8, int pageNumber = 0)
        {
            var filter = _filterService.CreateFilter(year, month);
            PageDto<WeatherRecordDto> page = await _weatherRecordsService.GetWeatherRecordsAsync(filter, pageSize, pageNumber, cancellationToken);
            return new WeatherRecordsResponse
            {
                Data = page.Values,
                Count = page.Values.Count(),
                PageNumber = page.PageNumber,
                PageSize = page.PageSize,
                TotalPages = page.TotalPages
            };
        }

        /// <summary>
        /// Получение списка всех годов, по которым есть архивные данные
        /// </summary>
        /// <returns></returns>
        [HttpGet("years")]
        public async Task<IEnumerable<int>> GetAvailableYears()
        {
            return await _weatherRecordsService.GetYearsHavingData();
        }
    }
}
