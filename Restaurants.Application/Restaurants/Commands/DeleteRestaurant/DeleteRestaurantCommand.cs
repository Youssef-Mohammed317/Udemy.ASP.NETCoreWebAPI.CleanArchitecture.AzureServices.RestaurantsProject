using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public record DeleteRestaurantCommand(int Id) : IRequest<bool>;

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
public class DeleteRestaurantCommandHandler(IUnitOfWork _unitOfWork,
    ILogger<DeleteRestaurantCommandHandler> _logger) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {

        _logger.LogInformation($"Delete restaurant Id => {{{request.Id}}}");

        var restaurant = await _unitOfWork.RestaurantRepository.GetById(request.Id);

        if (restaurant == null)
        {
            throw new KeyNotFoundException($"Restaurant with id {request.Id} not found.");
        }
        _unitOfWork.RestaurantRepository.Delete(restaurant);

        var affected = await _unitOfWork.CommitAsync();

        return affected > 0;

    }
}