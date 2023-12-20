using System.ComponentModel.DataAnnotations;

namespace iscas_lune_api.Dtos.Usuarios;

public class EsqueceuSenhaDto
{
    [Required]
    [MaxLength(255)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
}
