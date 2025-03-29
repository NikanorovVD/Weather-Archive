using AutoMapper;
using DataLayer.Entities;
using ServiceLayer.Models;


namespace WeatherArchive.Server.Automapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            AllowNullCollections = true;

            // Data - Service
            CreateMap<WeatherRecord, WeatherRecordDto>();

            // Service - Data
            CreateMap<WeatherRecordDto, WeatherRecord>();
        }
    }
}
