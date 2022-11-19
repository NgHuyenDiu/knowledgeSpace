using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeSpace.ViewModels
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var files = value as List<IFormFile>;
                foreach (IFormFile file in files)
                {
                    if (file != null)
                    {
                        var extension = Path.GetExtension(file.FileName);
                        if (!_extensions.Contains(extension.ToLower()))
                        {
                            return new ValidationResult(GetErrorMessage());
                        }
                    }
                }
            }
             
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Định dạng file hợp lệ bao gồm .png, .jpg, .doc, .docx, .pdf, .txt !";
        }
    }
}
