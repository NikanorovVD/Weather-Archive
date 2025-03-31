using ServiceLayer.Models;

namespace ServiceLayer.Abstractions
{
    public interface IParseWeatherXlsService
    {
        /// <summary>
        /// Считывает погодные данные из .xls или .xlsx файла
        /// </summary>
        /// <param name="stream">Поток для чтения файла</param>
        /// <param name="fileName">Имя файла</param>
        /// <param name="fileType">Тип файла</param>
        /// <returns>Список записей со считанными данными</returns>
        public IEnumerable<WeatherRecordDto> ParseXls(Stream stream, string fileName, ExcelFileType fileType);

        /// <summary>
        /// Считывает погодные данные из .xls или .xlsx файла
        /// </summary>
        /// <param name="stream">Поток для чтения файла</param>
        /// <param name="fileName">Имя файла используемое для определения расширения</param>
        /// <returns>Список записей со считанными данными</returns>
        public IEnumerable<WeatherRecordDto> ParseXls(Stream stream, string fileName);
    }
}
