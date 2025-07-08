using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FunkoProject.Web.Models.ViewModels.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IBrowserFile file && file.Size > _maxFileSize)
            {
                return new ValidationResult(ErrorMessage ?? $"Maksymalny rozmiar pliku to {_maxFileSize} bajtów.");
            }

            return ValidationResult.Success;
        }
    }

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IBrowserFile file)
            {
                var extension = Path.GetExtension(file.Name).ToLower();
                if (!_extensions.Contains(extension))
                {
                    return new ValidationResult(ErrorMessage ?? $"Niedozwolone rozszerzenie pliku: {extension}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
