using System.ComponentModel.DataAnnotations;

namespace iscas_lune_api.Model.Login;

public class RequestLoginModel
{
    [Required]
    [MaxLength(255)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MaxLength(1000)]
    public string Senha { get; set; } = string.Empty;
}
