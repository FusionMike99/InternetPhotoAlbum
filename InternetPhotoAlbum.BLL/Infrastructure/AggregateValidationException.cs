using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.BLL.Infrastructure
{
    /// <summary>
    /// Exception for aggregate model validation results
    /// </summary>
    public class AggregateValidationException : Exception
    {
        public List<ValidationResult> ValidationResults { get; protected set; } = new List<ValidationResult>();

        /// <summary>
        /// Inject exception message
        /// </summary>
        /// <param name="message">Exception message</param>
        public AggregateValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Inject exception message and list of validation results
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="validationResults">List of validation results</param>
        public AggregateValidationException(string message, List<ValidationResult> validationResults) : base(message)
        {
            ValidationResults = validationResults;
        }
    }
}
