namespace Finaflow.Core.Requests;

public abstract class PagedRequest : Request {
    public int DefaultPageNumber = Configuration.DefaultPageNumber;
    public int DefaultPageSize = Configuration.DefaultPageSize;
}