using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public record GetRestaurantByIdQuery(int Id) : IRequest<RestaurantDto?>;

public class GetRestaurantByIdQueryHandler(IUnitOfWork _unitOfWork,
    ILogger<GetRestaurantByIdQueryHandler> _logger,
    IMapper _mapper ) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
{
    public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get Restaurant of Id => {requestId}", request.Id);

        var restaurant = await _unitOfWork.RestaurantRepository.GetById(request.Id);

        return _mapper.Map<RestaurantDto?>(restaurant);
    }
}

