using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.BLL.Infrastructure
{
    public class AggregateValidationException : Exception
    {
        public List<ValidationResult> ValidationResults { get; protected set; } = new List<ValidationResult>();

        public AggregateValidationException(string message) : base(message)
        {
        }

        public AggregateValidationException(string message, List<ValidationResult> validationResults) : base(message)
        {
            ValidationResults = validationResults;
        }
    }
}
