using iscas_lune_api.Model.Login;

namespace iscas_lune_api.Application.Interfaces;

public interface ILoginService
{
    Task<ResponseLoginModel> LoginAsync(RequestLoginModel requestLoginModel);
}
