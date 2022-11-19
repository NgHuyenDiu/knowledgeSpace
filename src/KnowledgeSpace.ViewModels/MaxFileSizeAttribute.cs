
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeSpace.ViewModels.Contents
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
           if(value != null)
            {
                var files = value as List<IFormFile>;
                foreach (IFormFile file in files)
                {
                    if (file != null)
                    {
                        if (file.Length > _maxFileSize)
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
            return $"Độ lớn tối đa của file là :  { _maxFileSize} bytes.";
        }
    }
}
