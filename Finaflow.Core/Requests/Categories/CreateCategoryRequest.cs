using System.ComponentModel.DataAnnotations;

namespace Finaflow.Core.Requests;

public class CreateCategoryRequest : Request {

    [Required(ErrorMessage = "Título inválido")]
    [MaxLength(80, ErrorMessage = "O título deve conter o máximo de 80 caracteres!")]
    public string Title {get; set;} = string.Empty;
    
    [Required(ErrorMessage = "Descrição Inválida")]
    public string? Description {get; set;} = string.Empty;

}