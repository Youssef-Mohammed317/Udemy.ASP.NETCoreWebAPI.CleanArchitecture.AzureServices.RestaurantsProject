using AutoMapper;
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

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> _logger,
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating a restaurant Id => {requestId} with {@UpdatedRestaurant}",request.Id,request);

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