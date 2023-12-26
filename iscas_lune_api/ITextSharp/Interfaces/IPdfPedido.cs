namespace iscas_lune_api.ITextSharp.Interfaces;

public interface IPdfPedido
{
    Task<bool> GeneratePdfAsync(Guid pedidoId);
}
