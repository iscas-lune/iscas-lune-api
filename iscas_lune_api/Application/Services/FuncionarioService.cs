using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Exceptions;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Model.Funcionarios;
using iscas_lune_api.Model.Login;
using static BCrypt.Net.BCrypt;

namespace iscas_lune_api.Application.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly ITokenService _tokenService;

    public FuncionarioService(IFuncionarioRepository funcionarioRepository, ITokenService tokenService)
    {
        _funcionarioRepository = funcionarioRepository;
        _tokenService = tokenService;
    }

    public async Task<(string token, FuncionarioViewModel? funcionario)> LoginFuncionarioAsync(RequestLoginModel requestLoginModel)
    {
        var funcionario = await _funcionarioRepository
            .LoginFuncionarioAsync(requestLoginModel.Email);

        if (funcionario == null || !Verify(requestLoginModel.Senha, funcionario.Senha))
            throw new ExceptionApi("E-mail ou senha inválidos!");

        var token = _tokenService.GenerateTokenFuncionario(funcionario);

        return (token, new FuncionarioViewModel().ForModel(funcionario));
    }
}
