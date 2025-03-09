using FluentValidation;
using PatientApi.Modules.Patient.Patient.Requests;

namespace PatientApi.Modules.Patient.Patient.Validations;

public class CreatesPatientRequestValidation : AbstractValidator<CreatesPatientRequest>
{
    public CreatesPatientRequestValidation()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is not valid");
    }
}