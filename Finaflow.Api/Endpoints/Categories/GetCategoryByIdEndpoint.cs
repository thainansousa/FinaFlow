using Finaflow.Api.Common.Api;
using Finaflow.Core.Handler;
using Finaflow.Core.Models;
using Finaflow.Core.Requests;
using Finaflow.Core.Responses;

namespace Finaflow.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("/{id}", HandleAsync)
    .WithName("Categories: Get By Id")
    .WithSummary("Recupera a categoria pelo id")
    .WithDescription("Recupera a categoria pelo id")
    .WithOrder(4)
    .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler, 
        long id
        ){
        
        var request = new GetCategoryByIdRequest {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var result = await handler.GetByIdAsync(request);

        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}