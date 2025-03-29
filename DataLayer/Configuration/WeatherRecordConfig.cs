using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configuration
{
    internal class WeatherRecordConfig : IEntityTypeConfiguration<WeatherRecord>
    {
        public void Configure(EntityTypeBuilder<WeatherRecord> builder)
        {
            builder.HasKey(r => r.DateTime);
        }
    }
}
