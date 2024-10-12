using Finaflow.Api.Common.Api;
using Finaflow.Core;
using Finaflow.Core.Handler;
using Finaflow.Core.Models;
using Finaflow.Core.Requests;
using Finaflow.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Finaflow.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync)
    .WithName("Categories: Get All")
    .WithSummary("Recupera todas as categorias")
    .WithDescription("Recupera todas as categorias")
    .WithOrder(3)
    .Produces<PagedResponse<List<Category>?>>();

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler, 
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber, 
        [FromQuery] int pageSize = Configuration.DefaultPageSize
    ){
        
        var request = new GetAllCategoriesRequest {
            UserId = ApiConfiguration.UserId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await handler.GetAllAsync(request);
        
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}