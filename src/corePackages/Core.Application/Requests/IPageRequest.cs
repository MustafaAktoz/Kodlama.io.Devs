namespace Core.Application.Requests;

public interface IPageRequest
{
    int Page { get; set; }
    int PageSize { get; set; }
}