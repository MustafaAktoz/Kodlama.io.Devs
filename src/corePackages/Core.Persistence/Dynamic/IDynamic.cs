namespace Core.Persistence.Dynamic;

public interface IDynamic
{
    IEnumerable<Sort>? Sort { get; set; }
    Filter? Filter { get; set; }
}