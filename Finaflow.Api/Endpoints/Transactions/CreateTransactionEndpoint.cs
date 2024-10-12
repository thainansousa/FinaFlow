using Finaflow.Api.Common.Api;
using Finaflow.Core.Handler;
using Finaflow.Core.Models;
using Finaflow.Core.Requests;
using Finaflow.Core.Responses;

namespace Finaflow.Api.Endpoints.Transactions;

public class CreateTransactionEndpoint :IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
    .WithName("Transactions: Create")
    .WithSummary("Cria uma nova transação")
    .WithDescription("Cria uma nova transação")
    .WithOrder(1)
    .Produces<Response<Transaction?>>();
    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler, 
        CreateTransactionRequest request
    ){
        
        request.UserId = ApiConfiguration.UserId;

        var response = await handler.CreateAsync(request);

        return response.IsSuccess ? TypedResults.Created($"v1/transactions/{response.Data?.Id}") : TypedResults.BadRequest(response);
        
    }
}