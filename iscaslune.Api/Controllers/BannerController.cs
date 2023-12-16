using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Application.Services;
using iscaslune.Api.Dtos.Banners;
using iscaslune.Api.Dtos.Categorias;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iscaslune.Api.Controllers;

[ApiController]
[Route("api/banner")]

public class BannerController(IBannerService bannerService) 
    : ControllerBaseIscasLune
{
    private readonly IBannerService _bannerService = bannerService;

    [HttpPost("create")]
    public async Task<IActionResult> CreateBanner(CreateBannerDto createBannerDto)
    {
		try
		{
            var result = await _bannerService.CreateBannerAsync(createBannerDto);
            return HandleCreated(result, $"api/banner/get?id={result?.Id}");
		}
		catch (Exception ex)
		{
            return HandleError(ex.Message);
		}
    }

    [EnableCors("iscasluneorigin")]
    [HttpGet("list")]
    public async Task<IActionResult> ListBanner()
    {
        try
        {
            var result = await _bannerService.GetBannersAsync();
            return HandleGet(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }
}
