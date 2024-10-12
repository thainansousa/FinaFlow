using Finaflow.Core.Models;
using Finaflow.Core.Requests;
using Finaflow.Core.Responses;

namespace Finaflow.Core.Handler;

public interface ITransactionHandler {

    Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
    Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
    Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
    Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
    Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request);
}