using System.Text.Json.Serialization;

namespace DAL.Entities;

public class PostCategory
{
    public Guid CategoryGuid { get; set; }
    public Guid PostGuid { get; set; }
    [JsonIgnore]
    public virtual Category Category{ get; set; }
    [JsonIgnore]
    public virtual Post Post{ get; set; }
}