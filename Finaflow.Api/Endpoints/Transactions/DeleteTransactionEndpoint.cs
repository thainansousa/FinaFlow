using Finaflow.Api.Common.Api;
using Finaflow.Core.Handler;
using Finaflow.Core.Models;
using Finaflow.Core.Requests;
using Finaflow.Core.Responses;

namespace Finaflow.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapDelete("/{id}", HandleAsync)
    .WithName("Transactions: Delete")
    .WithSummary("Exclui uma transação")
    .WithDescription("Excluir uma transação")
    .WithOrder(2)
    .Produces<Response<Transaction?>>();
    
    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler, 
        long id
    ){
        
        var request = new DeleteTransactionRequest {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var result = await handler.DeleteAsync(request);

        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}