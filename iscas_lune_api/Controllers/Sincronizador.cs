using iscaslune.Api.Domain.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;

namespace iscas_lune_api.Controllers;

[ApiController]
public class Sincronizador : ControllerBase
{
    private readonly IscasLuneContext _context;
    private readonly IscasLuneContextNovo _contextNovo;

    public Sincronizador(IscasLuneContext context)
    {
        var con = "User ID=userdbopemadm; Password=VMUEzSwBxepp; Host=opem-adm-db.ctma8dhskcgs.us-east-1.rds.amazonaws.com; Port=5432; Database=iscaslune; Pooling=true;";
        var opt = new DbContextOptionsBuilder<IscasLuneContextNovo>();
        opt.UseNpgsql(con,
            b => b.MigrationsAssembly(typeof(IscasLuneContextNovo).Assembly.FullName));
        _context = context;
        _contextNovo = new IscasLuneContextNovo(opt.Options);
    }

    [HttpGet("sincronizar")]
    public async Task<IActionResult> Sincronizar()
    {
        try
        {
            //await CopiarRegistrosAsync(_context.TabelaDePreco, _contextNovo);
            await CopiarRegistrosAsync(_context.ItensTabelaDePreco, _contextNovo);

            await _contextNovo.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private async Task CopiarRegistrosAsync<T>(DbSet<T> sourceSet, DbContext targetContext) where T : class
    {
        var registros = await sourceSet.AsNoTracking().ToListAsync();
        await targetContext.AddRangeAsync(registros);
    }

}
