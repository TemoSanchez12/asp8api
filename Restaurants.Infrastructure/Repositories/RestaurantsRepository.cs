
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository : IRestaurantsRepositoy
{
    private readonly RestaurantsDbContext _dbContext;

    public RestaurantsRepository(RestaurantsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await _dbContext.Restaurants.ToListAsync();
        return restaurants;
    }

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageNumber, int pageSize, string? sortBy, SortDirection? sortDirection)
    {
        var searchPhraseLow = searchPhrase?.ToLower() ?? "";

        var baseQuery = _dbContext.Restaurants
            .Where(r => r.Name.ToLower().Contains(searchPhraseLow) || r.Description.ToLower().Contains(searchPhraseLow));

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {

            var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name), restaurant => restaurant.Name },
                { nameof(Restaurant.Category), restaurant => restaurant.Category},
                { nameof(Restaurant.Description), restaurant => restaurant.Description},
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }


        var restaurants = await baseQuery.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

        return (restaurants, totalCount);
    }

    public async Task<Restaurant?> GetByIdAsync(Guid restaurantGuid)
    {
        var restaurant = await _dbContext.Restaurants
            .Include(restaurant => restaurant.Dishes)
            .FirstOrDefaultAsync(restaurant => restaurant.Id == restaurantGuid);

        return restaurant;
    }

    public async Task<Guid> Create(Restaurant restaurant)
    {
        await _dbContext.Restaurants.AddAsync(restaurant);
        await _dbContext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task<Restaurant?> Delete(Guid guid)
    {
        var restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(res => res.Id == guid);
        if (restaurant == null)
            return null;

        _dbContext.Restaurants.Remove(restaurant);

        await _dbContext.SaveChangesAsync();
        return restaurant;
    }

    public async Task<Restaurant?> Update(Restaurant restaurant)
    {
        var restaurantToUpdate = await _dbContext.Restaurants.FirstOrDefaultAsync(res => res.Id == restaurant.Id);
        if (restaurantToUpdate == null)
            return null;

        restaurantToUpdate.Name = restaurant.Name;
        restaurantToUpdate.Description = restaurant.Description;
        restaurantToUpdate.Category = restaurant.Category;
        restaurant.HasDelivery = restaurant.HasDelivery;
        restaurant.ContactEmail = restaurant.ContactEmail;
        restaurant.ContactNumber = restaurant.ContactNumber;
        restaurantToUpdate.Address = restaurant.Address;

        await _dbContext.SaveChangesAsync();
        return restaurantToUpdate;
    }

    public async Task<IEnumerable<Restaurant>> GetRestaurantByOwner(string ownerId)
    {
        var restaurants = await _dbContext.Restaurants.Where(res => res.OwnerId == ownerId).ToListAsync();

        return restaurants;
    }
}
