using iscas_lune_api.Domain.Entities;
using iscaslune.Api.Dtos.Base;
using System.ComponentModel.DataAnnotations;
using static BCrypt.Net.BCrypt;

namespace iscas_lune_api.Dtos.Usuarios;

public class CreateUsuarioDto : BaseDto<Usuario>
{
    [Required]
    [MaxLength(255)]
    public string Nome { get; set; } = string.Empty;
    [Required]
    [MaxLength(255)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MaxLength(1000)]
    public string Senha { get; set; } = string.Empty;
    [Required]
    [MaxLength(1000)]
    [Compare("Senha", ErrorMessage = "As senha não conferem!")]
    public string ReSenha { get; set; } = string.Empty;
    [MaxLength(15)]
    public string? Telefone { get; set; } = string.Empty;
    [MaxLength(20)]
    public string? Cnpj { get; set; } = string.Empty;

    public override Usuario ForEntity()
    {
        var senha = HashPassword(Senha, 10);

        var date = DateTime.Now;
        return new Usuario(
            Guid.NewGuid(),
            date,
            date,
            0,
            Email,
            senha,
            Nome,
            Telefone,
            Cnpj);
    }
}
