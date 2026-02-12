using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool HasDelivery { get; set; }
    public int CategoryId { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
}
public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRestaurantCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(dto => dto.ContactEmail)
            .Cascade(CascadeMode.Stop)
            .EmailAddress().WithMessage("Please provide a valid email address.")
            .MustAsync(CheckEmailExist).WithMessage("This contact email already exists.")
            .When(dto => !string.IsNullOrWhiteSpace(dto.ContactEmail));

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$").WithMessage("Please provide a valid postal code (XX-XXX).")
            .When(dto => !string.IsNullOrWhiteSpace(dto.PostalCode));

        RuleFor(dto => dto.ContactNumber)
            .Cascade(CascadeMode.Stop)
            .Matches(@"^(010|011|012|015)\d{8}$").WithMessage("Please provide a valid Egyptian number.")
            .MustAsync(CheckNumberExist).WithMessage("This contact number already exists.")
            .When(dto => !string.IsNullOrWhiteSpace(dto.ContactNumber));



        RuleFor(dto => dto.CategoryId)
            .MustAsync(CheckCategoryExists)
            .WithMessage("Insert a valid Category");


    }
    private async Task<bool> CheckNumberExist(string number, CancellationToken token)
    {
        var isExist = await _unitOfWork.RestaurantRepository.IsContactNumberExist(number);
        return !isExist;
    }

    private async Task<bool> CheckEmailExist(string email, CancellationToken token)
    {
        var isExist = await _unitOfWork.RestaurantRepository.IsContactEmailExist(email);
        return !isExist;
    }
    private async Task<bool> CheckCategoryExists(int categoryId, CancellationToken token)
    {

        var category = await _unitOfWork.CategoryRepository.GetById(categoryId);
        return category != null;
    }

}

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> _logger,
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Creating a new restaurant");

        var restaurant = _mapper.Map<Restaurant>(request);

        await _unitOfWork.RestaurantRepository.CreateAsync(restaurant);

        await _unitOfWork.CommitAsync();

        return restaurant.Id;
    }
}