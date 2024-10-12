using Finaflow.Api.Common.Api;
using Finaflow.Core.Handler;
using Finaflow.Core.Models;
using Finaflow.Core.Requests;
using Finaflow.Core.Responses;

namespace Finaflow.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint {
     public static void Map(IEndpointRouteBuilder app) => app.MapPut("/{id}", HandleAsync)
     .WithName("Categories: Update")
     .WithSummary("Atualiza a categoria")
     .WithDescription("Atualiza a categoria")
     .WithOrder(5)
     .Produces<Response<Category?>>();

     private static async Task<IResult> HandleAsync(
        ICategoryHandler handler, 
        UpdateCategoryRequest request, 
        long id
     ){
        
        request.UserId = ApiConfiguration.UserId;
        request.Id = id;

        var response = await handler.UpdateAsync(request);

        return response.IsSuccess 
        ? TypedResults.Ok(response) 
        : TypedResults.BadRequest(response);
    }
}