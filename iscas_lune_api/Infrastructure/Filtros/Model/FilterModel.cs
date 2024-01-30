namespace iscaslune.Api.Infrastructure.Filters.Model;

public abstract class FilterModel
{
    public string? Search { get; set; }
    public int Skip {  get; set; }
    public int Take { get; set; } = 10;
    public string OrderBy { get; set; } = "DataCriacao";
    public bool Asc { get; set; } = false;
}
