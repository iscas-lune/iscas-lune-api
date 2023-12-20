using iscas_lune_api.Model.Usuarios;
using System.Text.Json.Serialization;

namespace iscas_lune_api.Model.Login;

public class ResponseLoginModel
{
    public ResponseLoginModel(UsuarioViewModel usuario, string token, string? error)
    {
        Usuario = usuario;
        Token = token;
        Error = error;
    }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Error { get; set; }
    public UsuarioViewModel Usuario { get; set; }
    public string Token { get; set; }
}
