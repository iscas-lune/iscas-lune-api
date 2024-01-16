using iscas_lune_api.Dtos.Carrinhos;

namespace iscas_lune_api.Model.Carrinho;

public class CarrinhoModel
{
    public Guid UsuarioId { get; set; }
    public List<AddCarrinhoDto> Produtos { get; set; } = new();
}
