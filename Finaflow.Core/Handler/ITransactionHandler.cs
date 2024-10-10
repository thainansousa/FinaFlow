using Finaflow.Core.Models;
using Finaflow.Core.Requests;
using Finaflow.Core.Responses;

namespace Finaflow.Core.Handler;

public interface ITransactionHandler {

    Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
    Task<Response<Transaction?>> UpdateAsync(CreateTransactionRequest request);
    Task<Response<Transaction?>> DeleteAsync(CreateTransactionRequest request);
    Task<Response<Transaction?>> GetByIdAsync(CreateTransactionRequest request);
    Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(CreateTransactionRequest request);
}