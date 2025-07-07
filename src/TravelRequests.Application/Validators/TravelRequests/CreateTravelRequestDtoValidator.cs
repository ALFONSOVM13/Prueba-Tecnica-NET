using FluentValidation;
using TravelRequests.Application.DTOs.TravelRequests;

namespace TravelRequests.Application.Validators.TravelRequests;

public class CreateTravelRequestDtoValidator : AbstractValidator<CreateTravelRequestDto>
{
    public CreateTravelRequestDtoValidator()
    {
        RuleFor(x => x.OriginCity)
            .NotEmpty().WithMessage("La ciudad de origen es requerida")
            .MaximumLength(100).WithMessage("La ciudad de origen no puede exceder los 100 caracteres");

        RuleFor(x => x.DestinationCity)
            .NotEmpty().WithMessage("La ciudad de destino es requerida")
            .MaximumLength(100).WithMessage("La ciudad de destino no puede exceder los 100 caracteres");

        RuleFor(x => x.DepartureDate)
            .NotEmpty().WithMessage("La fecha de salida es requerida")
            .GreaterThan(DateTime.Now).WithMessage("La fecha de salida debe ser posterior a la fecha actual");

        RuleFor(x => x.ReturnDate)
            .NotEmpty().WithMessage("La fecha de retorno es requerida")
            .GreaterThan(x => x.DepartureDate).WithMessage("La fecha de retorno debe ser posterior a la fecha de salida");

        RuleFor(x => x.Justification)
            .NotEmpty().WithMessage("La justificación es requerida")
            .MaximumLength(500).WithMessage("La justificación no puede exceder los 500 caracteres");
    }
} 