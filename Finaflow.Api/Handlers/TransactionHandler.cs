using Fina.Core.Common;
using Finaflow.Api.Data;
using Finaflow.Core.Enums;
using Finaflow.Core.Handler;
using Finaflow.Core.Models;
using Finaflow.Core.Requests;
using Finaflow.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Finaflow.Api.Handlers;

public class TransactionHandler(AppDbContext context) : ITransactionHandler {
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request){
        
        if(request is {Type: ETransactionType.Withdraw, Amount: >= 0}){
            request.Amount *= -1;
        }

        try{
           var transaction = new Transaction {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.Now,
                Amount = request.Amount,
                PaidOrReceivedAt = request.PaidOrReceiveAt,
                Title = request.Title,
                Type = request.Type
           };

           await context.Transactions.AddAsync(transaction);
           await context.SaveChangesAsync();

           return new Response<Transaction?>(data: transaction, message: "Nova transação criada com sucesso!");
        }
        catch{
            return new Response<Transaction?>(data: null, code: 500, message: "Erro ao criar nova transação criada com sucesso!");
        }
    }
    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request){
        if(request is {Type: ETransactionType.Withdraw, Amount: >= 0}){
            request.Amount *= -1;
        }

        try{
            var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if(transaction is null){
                return new Response<Transaction?>(data: null, code: 404, "Não foi possivel encontrar a transação.");
            }

            transaction.CategoryId = request.CategoryId;
            transaction.Amount = request.Amount;
            transaction.Title = request.Title;
            transaction.Type = request.Type;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, message: "Transação atualizada com sucesso!");

        }
        catch{ 
           return new Response<Transaction?>(data: null, code: 500, "Erro ao atualizar a transação.");
        }
    }
    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request){

        try{
            var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if(transaction is null){
                return new Response<Transaction?>(data: null, code: 404, "Não foi possivel encontrar a transação.");
            }

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, message: "Transação excluida com sucesso!");

        }
        catch{ 
           return new Response<Transaction?>(data: null, code: 500, "Erro ao excluir a transação.");
        }
    }
    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request){
        try{
            var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return transaction is null ? new Response<Transaction?>(data: null, code: 404, message: "Não foi possivel localizar a transação.") : new Response<Transaction?>(transaction);
        }
        catch{
            return new Response<Transaction?>(data: null, code: 500, "Erro ao tentar encontrar a transação.");
        }
    }
    public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request){
        try{
           request.StartDate ??= DateTime.Now.GetFirstDay();
           request.EndDate ??= DateTime.Now.GetLastDay(); 
        }
        catch{
            return new PagedResponse<List<Transaction>?>(data: null, code: 500, message: "Erro ao buscar transações.");
        }

        try{
           var query = context.Transactions.AsNoTracking().Where(x => x.PaidOrReceivedAt >= request.StartDate && x.PaidOrReceivedAt <= request.EndDate && x.UserId == request.UserId).OrderBy(x => x.PaidOrReceivedAt);

           var transactions = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

           var count = await query.CountAsync();

           return new PagedResponse<List<Transaction>?>(transactions, count, currentPage: request.PageNumber, request.PageSize); 
        }
        catch{
            return new PagedResponse<List<Transaction>?>(data: null, code: 500, message: "Erro ao buscar transações!");
        }
    }
}