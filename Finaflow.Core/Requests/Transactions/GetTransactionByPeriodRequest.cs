namespace Finaflow.Core.Requests;

public class GetTransactionByPeriodRequest : PagedRequest {
    public DateTime? StartDate {get; set;}
    public DateTime? EndDate {get; set;}
}