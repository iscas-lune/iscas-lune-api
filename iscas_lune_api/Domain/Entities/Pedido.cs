using iscaslune.Api.Domain.Entities.Bases;

namespace iscas_lune_api.Domain.Entities;

public sealed class Pedido : BaseEntity
{
    public Pedido(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, StatusPedido statusPedido, Guid usuarioId) : base(id, dataCriacao, dataAtualizacao, numero)
    {
        StatusPedido = statusPedido;
        UsuarioId = usuarioId;
    }

    public StatusPedido StatusPedido { get; private set; }
    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; set; } = null!;
    public decimal ValorTotal { get { return ItensPedido.Sum(x => x.ValorTotal); } }
    public List<ItensPedido> ItensPedido { get; set; } = new();
}

public enum StatusPedido
{
    Aberto,
    Faturado,
    RotaDeEntrega,
    Entregue
}
