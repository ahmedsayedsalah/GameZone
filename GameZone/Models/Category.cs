namespace GameZone.Models;

public class Category : BaseEntity
{
    public IList<Game> Games { get; set; }
}
