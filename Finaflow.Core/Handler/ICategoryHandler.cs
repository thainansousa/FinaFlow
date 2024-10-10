using Finaflow.Core.Models;
using Finaflow.Core.Requests;
using Finaflow.Core.Responses;

namespace Finaflow.Core.Handler;

public interface ICategoryHandler {
    Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
    Task<Response<Category?>> UpdateAsync(CreateCategoryRequest request);
    Task<Response<Category?>> DeleteAsync(CreateCategoryRequest request);
    Task<Response<Category?>> GetByIdAsync(CreateCategoryRequest request);
    Task<PagedResponse<List<Category>?>> GetAllAsync(CreateCategoryRequest request);
}