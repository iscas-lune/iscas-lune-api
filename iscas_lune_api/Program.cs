using dotenv.net;
using iscas_lune_api.CrossCutting;
using iscas_lune_api.Workers;
using iscaslune.Api;
using iscaslune.Api.CrossCutting;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        DotEnv.Load();
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.InjectDomain();
        builder.Services.InjectInfrastructure();
        builder.Services.InjectApplication();
        builder.Services.InjectJwt();
        builder.Services.InjectCors();
        builder.Services.InjectHttpClient();
        builder.Services.AddHostedService<PedidosEmAbertoWorker>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (EnvironmentVariable.GetVariable("AMBIENTE").Equals("develop"))
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}