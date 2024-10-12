using Finaflow.Api.Data;
using Finaflow.Api.Handlers;
using Finaflow.Core;
using Finaflow.Core.Handler;
using Microsoft.EntityFrameworkCore;

namespace Finaflow.Api.Common.Api;

public static class BuildExtension {
    public static void AddConfiguration(this WebApplicationBuilder builder){

        ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;

        Configuration.FrontEndUrl = builder.Configuration.GetValue<string>("FrontEndUrl") ?? string.Empty;
    }

    public static void AddDocumentation(this WebApplicationBuilder builder){
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x => {x.CustomSchemaIds(n => n.FullName);});
    }
    
    public static void AddDataContexts(this WebApplicationBuilder builder) {
        builder.Services.AddDbContext<AppDbContext>(
            x => {x.UseSqlServer(ApiConfiguration.ConnectionString);}
        );
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder){
        builder.Services.AddCors(
            options => options.AddPolicy(
                ApiConfiguration.CorsPolicyName,
                policy => policy.WithOrigins([
                    Configuration.BackendUrl,
                    Configuration.FrontEndUrl
                ])
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            )
        );
    }

    public static void AddServices(this WebApplicationBuilder builder){

        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
        
    }

}