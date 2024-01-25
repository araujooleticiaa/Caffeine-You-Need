using System.Text.Json.Serialization;

namespace Caffeine.You.Need.Models;

public class BaseModel
{
    public BaseModel()
    {
        Id = Guid.NewGuid();
    }

    [JsonIgnore]
    public Guid Id { get; set; }
    public string Code { get; set; }
}