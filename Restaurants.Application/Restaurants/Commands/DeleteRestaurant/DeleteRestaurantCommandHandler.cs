using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(IUnitOfWork _unitOfWork,
    ILogger<DeleteRestaurantCommandHandler> _logger) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Delete restaurant Id => {requestId}", request.Id);

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