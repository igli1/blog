namespace DAL.Entities;

public class PostCategory
{
    public Guid CategoryGuid { get; set; }
    public Guid PostGuid { get; set; }
    public virtual Category Category{ get; set; }
    public virtual Post Post{ get; set; }
}