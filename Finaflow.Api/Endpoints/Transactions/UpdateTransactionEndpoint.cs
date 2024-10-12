using Finaflow.Api.Common.Api;
using Finaflow.Core.Handler;
using Finaflow.Core.Models;
using Finaflow.Core.Requests;
using Finaflow.Core.Responses;

namespace Finaflow.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint {
     public static void Map(IEndpointRouteBuilder app) => app.MapPut("/{id}", HandleAsync)
     .WithName("Transactions: Update")
     .WithSummary("Atualiza a transação")
     .WithDescription("Atualiza a transação")
     .WithOrder(5)
     .Produces<Response<Transaction?>>();

     private static async Task<IResult> HandleAsync(
        ITransactionHandler handler, 
        UpdateTransactionRequest request, 
        long id
        ){
        
        request.UserId = ApiConfiguration.UserId;

        request.Id = id;

        var response = await handler.UpdateAsync(request);
        
        return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}