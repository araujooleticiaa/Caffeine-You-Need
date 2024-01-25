using Caffeine.You.Need.Data;
using Caffeine.You.Need.Interface;
using Caffeine.You.Need.Models;
using Microsoft.EntityFrameworkCore;

namespace Caffeine.You.Need.Service;

public class CaffeineRepository : ICaffeineRepository
{
    private readonly DataContext _context;

    public CaffeineRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Recommendation>> CreateRecommendations(List<RecommendationRequest> recommendation)
    {
        validationRecommendations(recommendation);
        return await _context.Recommendations.ToListAsync();
    }

    private async void validationRecommendations(List<RecommendationRequest> recommendation)
    {
        foreach (RecommendationRequest itemRecommendation in recommendation)
        {
            var listCoffes = await _context.Coffees.Where(x => x.Code == itemRecommendation.Code).ToListAsync();
            var listCoffesSumCaffeine = _context.Coffees.Where(x => x.Code == itemRecommendation.Code).Select(x => x.CaffeineLevel).Sum();
            
            List<Recommendation> newRecommendations = new List<Recommendation>();

            foreach (var coffee in listCoffes)
            {
                double initialAmount = listCoffesSumCaffeine + coffee.CaffeineLevel;
                double finalAmount = 175;
                double halfLife = 300;
                double timeElapsed = itemRecommendation.Time;

                var timeLast = timeElapsed - halfLife * Math.Log(finalAmount / initialAmount, 2);
                string formattedTime = timeLast.ToString("00");

                if (timeLast < 0)
                    newRecommendations.Add(new Recommendation { Name = coffee.Name, Code = coffee.Code, Wait = "0" });
                else
                    newRecommendations.Add(new Recommendation { Name = coffee.Name, Code = coffee.Code, Wait = formattedTime });
            }
            await _context.Recommendations.AddRangeAsync(newRecommendations);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<List<Coffee>> GetCoffees()
    {
        List<Coffee> coffees = new List<Coffee>()
        {
            new Coffee { Name = "Black Coffe", Code = "blk", CaffeineLevel = 95},
            new Coffee { Name = "Espresso", Code = "esp" , CaffeineLevel = 63},
            new Coffee { Name = "Cappuccino", Code = "cap", CaffeineLevel = 63},
            new Coffee { Name = "Latte", Code = "lat", CaffeineLevel = 63},
            new Coffee { Name = "Flat White", Code = "wht", CaffeineLevel = 63},
            new Coffee { Name = "Cold Brew", Code = "cld", CaffeineLevel = 120},
            new Coffee { Name = "Decaf Coffee", Code = "dec", CaffeineLevel = 5}
        };

        await _context.Coffees.AddRangeAsync(coffees);
        await _context.SaveChangesAsync();

        return await _context.Coffees.ToListAsync();
    }
}