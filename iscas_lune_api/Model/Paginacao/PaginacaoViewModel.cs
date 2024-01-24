namespace iscas_lune_api.Model.Paginacao;

public class PaginacaoViewModel<T>
{
    public List<T> Values { get; set; } = new();
    public int TotalPage { get; set; }
}
