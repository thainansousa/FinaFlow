using System.ComponentModel.DataAnnotations;
using Finaflow.Core.Enums;

namespace Finaflow.Core.Requests;

public class CreateTransactionRequest : Request {
    
    [Required(ErrorMessage = "Título Inválido!")]
    public string Title {get; set;} = string.Empty;

    [Required(ErrorMessage = "Tipo Inválido!")]
    public ETransactionType Type {get; set;} = ETransactionType.Withdraw;

    [Required(ErrorMessage = "Valor Inválido!")]
    public decimal Amonut {get; set;}

    [Required(ErrorMessage = "Categoria Inválida!")]
    public long CategoryId {get; set;}

    [Required(ErrorMessage = "Data Inválida!")]
    public DateTime? PaidOrReceiveAt {get; set;}
}