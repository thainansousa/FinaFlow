using Finaflow.Api.Common.Api;
using Finaflow.Core.Handler;
using Finaflow.Core.Models;
using Finaflow.Core.Requests;
using Finaflow.Core.Responses;

namespace Finaflow.Api.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("/{id}", HandleAsync)
    .WithName("Transactions: Get By Id")
    .WithSummary("Recupera a transação pelo id")
    .WithDescription("Recupera a transação pelo id")
    .WithOrder(4)
    .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler, 
        long id
        ){
        
        var request = new GetTransactionByIdRequest {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}