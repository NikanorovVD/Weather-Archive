using ServiceLayer.Models;

namespace WeatherArchive.Server.Models
{
    public class WeatherRecordsResponse
    {
        public int PageNumber {  get; set; }
        public int PageSize {  get; set; }
        public int Count {  get; set; }
        public int TotalPages {  get; set; }
        public IEnumerable<WeatherRecordDto> Data { get; set; }
    }
}
