using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Abstractions;
using ServiceLayer.Models;
using ServiceLayer.Models.Errors;
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
        /// <param name="request">Файлы с данными погодных условий в формате .xls или .xlsx</param>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadArchiveFilesAsync(FilesUploadRequest request)
        {
            try
            {
                IEnumerable<WeatherRecordDto> allRecords = [];
                foreach (var file in request.Files)
                {                
                    IEnumerable<WeatherRecordDto> fileRecords = _parseService.ParseXls(file.OpenReadStream(), file.FileName);
                    allRecords = allRecords.Union(fileRecords);
                }
                await _weatherRecordsService.CteateRecordsAsync(allRecords);
                return Ok();
            }
            catch (ValidationException ex) 
            { 
                return BadRequest(ex.Error); 
            }
        }

        /// <summary>
        /// Получение архивных погодных данных
        /// </summary>
        /// <param name="request">параметры запроса</param>
        /// <param name="cancellationToken"></param>
        /// <returns>объект WeatherRecordsResponse с найденными записями</returns>
        [HttpGet("archive")]
        public async Task<WeatherRecordsResponse> GetWeatherRecordsAsync([FromQuery] WeatherRecordsRequest request, CancellationToken cancellationToken)
        {
            var filter = _filterService.CreateFilter(request.Year, request.Month);
            PageDto<WeatherRecordDto> page = await _weatherRecordsService.GetWeatherRecordsAsync(filter, request.PageSize, request.PageNumber, cancellationToken);
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
        /// <returns>Список годов</returns>
        [HttpGet("years")]
        public async Task<IEnumerable<int>> GetAvailableYears()
        {
            return await _weatherRecordsService.GetYearsHavingData();
        }
    }
}
