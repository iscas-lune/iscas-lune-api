using iscaslune.Api.Domain.Entities.Bases;
using static BCrypt.Net.BCrypt;

namespace iscas_lune_api.Domain.Entities;

public sealed class Usuario : BaseEntity
{
    public Usuario(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, string email, string senha, string nome, string? telefone)
        : base(id, dataCriacao, dataAtualizacao, numero)
    {
        Email = email;
        Senha = senha;
        Nome = nome;
        Telefone = telefone;
    }

    public string Email { get; private set; }
    public string Senha { get; private set; }
    public string Nome { get; private set; }
    public string? Telefone { get; private set; }

    public void UpdateSenha(string senha)
    {
        Senha = HashPassword(senha, 10); ;
    }

    public void Update(string email, string nome, string? telefone)
    {
        Email = email;
        Nome = nome;
        Telefone = telefone;
    }
}
