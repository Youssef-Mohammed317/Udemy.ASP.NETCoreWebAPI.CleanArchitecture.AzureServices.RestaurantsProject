using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class UpdateRestaurantCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool HasDelivery { get; set; }
}
public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{

    public UpdateRestaurantCommandValidator()
    {
        RuleFor(x => x.Id)
             .GreaterThan(0).WithMessage("Invalid restaurant id.");

        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is required.");

    }
}

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> _logger,
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating a restaurant Id => {{{request.Id}}}");

        var restaurant = await _unitOfWork.RestaurantRepository.GetById(request.Id);

        if (restaurant == null)
        {
            throw new KeyNotFoundException($"Restaurant with id {request.Id} not found.");
        }

        _mapper.Map(request, restaurant);

        _unitOfWork.RestaurantRepository.Update(restaurant);

        var affected = await _unitOfWork.CommitAsync();

        return affected > 0;
    }
}