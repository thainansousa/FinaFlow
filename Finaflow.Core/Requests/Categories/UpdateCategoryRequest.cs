using System.ComponentModel.DataAnnotations;

namespace Finaflow.Core.Requests;

public class UpdateCategoryRequest : Request {
    public long Id {get; set;}
    
    [Required(ErrorMessage = "Título Inválido!")]
    [MaxLength(80, ErrorMessage = "O título deve conter o máximo de 80 caracteres!")]
    public string Title {get; set;} = string.Empty;

    [Required(ErrorMessage = "Descrião inválida!")]
    public string? Description {get; set;} = string.Empty;
}