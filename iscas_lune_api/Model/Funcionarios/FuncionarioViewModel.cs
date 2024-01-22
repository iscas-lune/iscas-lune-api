using iscas_lune_api.Domain.Entities;
using iscaslune.Api.Model.Base;

namespace iscas_lune_api.Model.Funcionarios;

public class FuncionarioViewModel : BaseModel<Funcionario, FuncionarioViewModel>
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telefone { get; set; }

    public override FuncionarioViewModel? ForModel(Funcionario? entity)
    {
        if(entity == null) return null; 

        Id = entity.Id;
        DataCriacao = entity.DataCriacao;
        Numero = entity.Numero;
        Nome = entity.Nome;
        Email = entity.Email;
        Telefone = entity.Telefone;

        return this;
    }
}
