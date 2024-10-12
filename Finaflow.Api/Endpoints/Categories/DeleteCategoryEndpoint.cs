using Finaflow.Api.Common.Api;
using Finaflow.Core.Handler;
using Finaflow.Core.Models;
using Finaflow.Core.Requests;
using Finaflow.Core.Responses;

namespace Finaflow.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapDelete("/{id}", HandleAsync)
    .WithName("Categories: Delete")
    .WithSummary("Exclui uma categoria")
    .WithDescription("Excluir uma categoria"
    ).WithOrder(2)
    .Produces<Response<Category?>>();
    private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id){
        
        var request = new DeleteCategoryRequest {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        
        return result.IsSuccess ? TypedResults.Created($"v1/categories/{result.Data?.Id}") : TypedResults.BadRequest(result);
    }
}