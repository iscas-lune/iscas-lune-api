namespace iscaslune.Api.Infrastructure.Filters.Model;

public abstract class FilterModel
{
    public string OrderBy { get; set; } = "DataCriacao";
    public bool Asc { get; set; } = false;
}
