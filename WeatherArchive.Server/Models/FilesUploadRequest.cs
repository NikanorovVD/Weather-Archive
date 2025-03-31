namespace WeatherArchive.Server.Models
{
    public class FilesUploadRequest
    {
        /// <summary>
        /// Файлы с данными погодных условий в формате .xls или .xlsx
        /// </summary>
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
