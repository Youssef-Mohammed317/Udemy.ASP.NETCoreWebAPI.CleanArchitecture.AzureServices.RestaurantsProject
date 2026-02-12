using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> _logger,
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Creating a new restaurant {@restaurant}", request);

        var restaurant = _mapper.Map<Restaurant>(request);

        await _unitOfWork.RestaurantRepository.CreateAsync(restaurant);

        await _unitOfWork.CommitAsync();

        return restaurant.Id;
    }
}