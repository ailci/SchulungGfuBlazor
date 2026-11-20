using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Validations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class NoFutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateOnly dateToCheck)
        {
            if (dateToCheck > DateOnly.FromDateTime(DateTime.Today))
            {
                return new ValidationResult(ErrorMessage ?? "Datum liegt in der Zukunft");
            }
        }

        return ValidationResult.Success;
    }
}