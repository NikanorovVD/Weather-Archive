using FluentValidation;
using WeatherArchive.Server.Models;

namespace WeatherArchive.Server.Validation
{
    public class WeatherRecordsRequestValidator: AbstractValidator<WeatherRecordsRequest>
    {
        public WeatherRecordsRequestValidator()
        {
            RuleFor(r => r.Year)
                .InclusiveBetween(1800, DateTime.UtcNow.Year);
            RuleFor(r => r.Month)
                .InclusiveBetween(1, 12);
            RuleFor(r => r.PageSize)
                .GreaterThan(0);
            RuleFor(r => r.PageNumber)
                .GreaterThanOrEqualTo(0);
        }
    }
}
