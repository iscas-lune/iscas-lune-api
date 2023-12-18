namespace iscas_lune_api.Model.Usuarios;

public class UsuarioCreateViewModel
{
    public UsuarioCreateViewModel(UsuarioViewModel usuario, string token, string? error)
    {
        Usuario = usuario;
        Token = token;
        Error = error;
    }
    public string? Error {  get; set; }
    public UsuarioViewModel Usuario { get; set; }
    public string Token { get; set; }
}
