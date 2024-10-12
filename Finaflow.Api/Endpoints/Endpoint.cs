using Finaflow.Api.Common.Api;
using Finaflow.Api.Endpoints.Categories;
using Finaflow.Api.Endpoints.Transactions;

namespace Finaflow.Api.Endpoints;

public static class Endpoint{
    public static void MapEndpoints(this WebApplication app) {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/")
        .WithTags("Health Check")
        .MapGet("/", () => new {message = "OK"});
        
        endpoints.MapGroup("v1/categories").WithTags("Categories")
        .MapEndpoint<CreateCategoryEndpoint>()
        .MapEndpoint<UpdateCategoryEndpoint>()
        .MapEndpoint<DeleteCategoryEndpoint>()
        .MapEndpoint<GetAllCategoriesEndpoint>()
        .MapEndpoint<GetCategoryByIdEndpoint>();

        endpoints.MapGroup("v1/transactions").WithTags("Transactions")
        .MapEndpoint<CreateTransactionEndpoint>()
        .MapEndpoint<UpdateTransactionEndpoint>()
        .MapEndpoint<DeleteTransactionEndpoint>()
        .MapEndpoint<GetTransactionByIdEndpoint>()
        .MapEndpoint<GetTransactionByPeriodEndpoint>();
    }
        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) 
        where TEndpoint : IEndpoint {
            TEndpoint.Map(app);
            return app;
        }
}