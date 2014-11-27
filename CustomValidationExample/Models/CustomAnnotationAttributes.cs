using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomValidationExample.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DecimalGreaterThan : ValidationAttribute, IClientValidatable
    {
        readonly string _otherPropertyName;

        public DecimalGreaterThan(string otherPropertyName, string errorMessage)
            : base(errorMessage)
        {
            _otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;
            try
            {
                // Using reflection we can get a reference to the other property, in this example the project Start price
                var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);
                // Let's check that otherProperty is of type Decimal as we expect it to be
                if (otherPropertyInfo.PropertyType == new Decimal().GetType())
                {
                    Decimal toValidate = (Decimal)value;
                    Decimal referenceProperty = (Decimal)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                    // if the end date is lower than the start date, than the validationResult will be set to false and return
                    // a properly formatted error message
                    if (toValidate.CompareTo(referenceProperty) < 1)
                    {
                        validationResult = new ValidationResult(ErrorMessageString);
                    }
                }
                else
                {
                    validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type Decimal");
                }
            }
            catch (Exception ex)
            {
                // Do stuff, i.e. log the exception
                // Let it go through the upper levels, something bad happened
                throw ex;
            }

            return validationResult;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            //string errorMessage = this.FormatErrorMessage(metadata.DisplayName);
            string errorMessage = ErrorMessageString;

            // The value we set here are needed by the jQuery adapter
            ModelClientValidationRule decimalGreaterThanRule = new ModelClientValidationRule();
            decimalGreaterThanRule.ErrorMessage = errorMessage;
            decimalGreaterThanRule.ValidationType = "decimalgreaterthan"; // This is the name the jQuery adapter will use
            //"otherpropertyname" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
            decimalGreaterThanRule.ValidationParameters.Add("otherpropertyname", _otherPropertyName);

            yield return decimalGreaterThanRule;
        }
    }
}