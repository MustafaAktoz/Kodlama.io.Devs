namespace Core.Application.Requests;

public class PageRequest:IPageRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}
