using Finaflow.Api.Common.Api;
using Finaflow.Core;
using Finaflow.Core.Handler;
using Finaflow.Core.Models;
using Finaflow.Core.Requests;
using Finaflow.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Finaflow.Api.Endpoints.Transactions;

public class GetTransactionByPeriodEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync)
    .WithName("Transactions: Get By Period")
    .WithSummary("Recupera todas as transações por periodo")
    .WithDescription("Recupera todas as transações por periodo")
    .WithOrder(3)
    .Produces<PagedResponse<List<Transaction>?>>();

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler, 
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber, 
        [FromQuery] int pageSize = Configuration.DefaultPageSize, 
        [FromQuery] DateTime? startDate = null, 
        [FromQuery] DateTime? endDate = null){
        
        var request = new GetTransactionByPeriodRequest {
            UserId = ApiConfiguration.UserId,
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await handler.GetByPeriodAsync(request);
        
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}