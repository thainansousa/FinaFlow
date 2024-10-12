namespace Finaflow.Core.Requests;

public abstract class PagedRequest : Request {
    public int PageNumber = Configuration.DefaultPageNumber;
    public int PageSize = Configuration.DefaultPageSize;
}