using FluentValidation;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurntCommandValidator : AbstractValidator<DeleteRestaurantCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRestaurntCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Id).Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Invalid restaurant id.");
    }
}
