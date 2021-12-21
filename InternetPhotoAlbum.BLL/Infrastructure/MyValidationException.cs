using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.BLL.Infrastructure
{
    public class MyValidationException : Exception
    {
        public List<ValidationResult> ValidationResults { get; protected set; } = new List<ValidationResult>();

        public MyValidationException(string message) : base(message)
        {
        }

        public MyValidationException(string message, List<ValidationResult> validationResults) : base(message)
        {
            ValidationResults = validationResults;
        }
    }
}
