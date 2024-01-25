using Caffeine.You.Need.Models;

namespace Caffeine.You.Need.Interface;
public interface ICaffeineRepository
{
    Task<List<Coffee>> GetCoffees();
    Task<List<Recommendation>> CreateRecommendations(List<RecommendationRequest> recommendation);
}