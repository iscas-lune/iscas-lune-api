
using iscas_lune_api.Discord.Client;
using iscas_lune_api.Discord.Models;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.ITextSharp.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace iscas_lune_api.Workers;

public class PedidosEmAbertoWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly double _delay = 
        double.Parse(Environment.GetEnvironmentVariable("DELAY_WORKER") ?? "1");
    private readonly IDiscordNotification _discordNotification;
    public PedidosEmAbertoWorker(IServiceProvider serviceProvider, IDiscordNotification discordNotification)
    {
        _serviceProvider = serviceProvider;
        _discordNotification = discordNotification;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Executando worker");
                using var scoped = _serviceProvider.CreateScope();
                var pedidosEmAbertoRepository = scoped.ServiceProvider.GetRequiredService<IPedidosEmAbertoRepository>();

                var pedidoEmAberto = await pedidosEmAbertoRepository.GetFirstOrDefautlAsync();

                if (pedidoEmAberto != null)
                {
                    Console.WriteLine("Enviando pedido em aberto!");
                    var pdf = scoped.ServiceProvider.GetRequiredService<IPdfPedido>();
                    var result = await pdf.GeneratePdfAsync(pedidoEmAberto.PedidoId);

                    if (result)
                    {
                        var discordModel = new DiscordModel()
                        {
                            Username = "Hook Pedido",
                            Content = "Novo pedido!",
                            Embeds = new Embeds[] 
                            {
                                new() {
                                    Description = $"Pedido gerado com sucesso!\n Id do pedido => {pedidoEmAberto.PedidoId} \nConfira se e-mail",
                                    Title = "Pedido"
                                }
                            }
                        };

                        await _discordNotification.NotifyAsync(discordModel);
                        await pedidosEmAbertoRepository.DeleteAsync(pedidoEmAberto);
                        Console.WriteLine("Pedido gerado com sucesso!");
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(_delay), stoppingToken);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
    }
}
