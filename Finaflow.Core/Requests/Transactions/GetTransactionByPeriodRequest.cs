namespace Finaflow.Core.Requests;

public class GetTransactionByPeriodRequest : PagedRequest {
    public DateTime? StarDate {get; set;}
    public DateTime? EndDate {get; set;}
}