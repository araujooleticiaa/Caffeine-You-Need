using System.Text.Json.Serialization;

namespace Caffeine.You.Need.Models;

public class Coffee : BaseModel
{
    public Coffee()
    {
        
    }

    public string Name { get; set; }

    [JsonIgnore]
    public double CaffeineLevel { get; set; }
}