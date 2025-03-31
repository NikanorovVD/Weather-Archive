using FluentValidation;
using WeatherArchive.Server.Models;

namespace WeatherArchive.Server.Validation
{
    public class FilesUploadRequestValidator: AbstractValidator<FilesUploadRequest>
    {
        private readonly IReadOnlySet<string> _allowedExtensions = new HashSet<string>([".xls", ".xlsx"]);
        public FilesUploadRequestValidator()
        {
            RuleForEach(r => r.Files)
                .Must(HaveAllowedExtension)
                .WithMessage($"Файлы должны иметь одно из следующих расширений: {string.Join(", ", _allowedExtensions)}"); 
        }

        private bool HaveAllowedExtension(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            return _allowedExtensions.Contains(extension); 
        }
    }
}
