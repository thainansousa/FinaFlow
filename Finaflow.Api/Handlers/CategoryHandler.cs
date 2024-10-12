using Finaflow.Api.Data;
using Finaflow.Core.Handler;
using Finaflow.Core.Models;
using Finaflow.Core.Requests;
using Finaflow.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Finaflow.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler {
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request){
        
        var category = new Category {
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description
        };
        
        try {

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            
            return new Response<Category?>(data: category, code: 201, message:"Categoria criada com Sucesso!");
        } catch {
            return new Response<Category?>(data: null, code: 500, message: "Não foi possivel criar a categoria.");
        }

    }
    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request){

        try{
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null){
                return new Response<Category?>(data: null, code: 404, message: "Categoria não encontrada.");
            }

            category.Title = request.Title;
            category.Description = request.Description;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(data: category, message:"Categoria atualizada com Sucesso!");
        }
        catch{
            return new Response<Category?>(data: null, code: 500, message: "Não foi possivel atualizar a categoria.");
        }

    }
    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request){
        
        try{
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null){
                return new Response<Category?>(data: null, code: 404, message: "Categoria não encontrada.");
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(data: category, message:"Categoria excluida com Sucesso!");
        }
        catch{
            return new Response<Category?>(data: null, code: 500, message: "Não foi possivel excluir a categoria.");
        }
    }
    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request){
        try{

            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return category is null ? new Response<Category?>(data: null, code: 404, message: "Categoria não encontrada.") : new Response<Category?>(category);
        }
        catch{
    
            return new Response<Category?>(data: null, code: 500, message: "Erro ao buscar categoria.");
        }
    }
    public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request){

        try{
            var query = context.Categories.AsNoTracking().Where(x => x.UserId == request.UserId).OrderBy(x => x.Title);

            var categories = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Category>?>(categories, count, currentPage: request.PageNumber, request.PageSize);
        }
        catch{
            
            return new PagedResponse<List<Category>?>(data: null, code: 500, message: "Error ao buscar categorias.");
        }
        
    }

}