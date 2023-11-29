
using LabPlatform.Models;
using LabPlatform.Models.DTOs;
using LabPlatform.Schemas;
using LabPlatform.Services.Statics;
using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;

namespace LabPlatform;

[ApiController]
[Route("api/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _ArticleService;

    public ArticleController(IArticleService ArticleService)
    {
        _ArticleService = ArticleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<ArticleDTO> articles = await _ArticleService.GetAll();
        return Ok(Res.Provider(articles, "Articulos obtenidos", true));
    }

    [HttpGet]
    [Route("all/{type}")]

    public async Task<IActionResult> GetNotices(string type)
    {
        List<ArticleDTO> articles = await _ArticleService.GetAll();
        var filter = articles.Where(article => article.ArticleType == type).ToList();

        var min = filter.Select(article => TinyMapper.Map<ArticleMinDTO>(article)).ToList();
        return Ok(Res.Provider(min, "Articulos obtenidos", true));

    }

    [HttpGet]
    [Route("one/{title}")]
    public async Task<IActionResult> GetNoticeByTitle(string title)
    {
        title = title.Replace("-", " ");

        List<ArticleDTO> articles = await _ArticleService.GetAll();

        var article = articles.FirstOrDefault(a => a.Title.ToLower() == title.ToLower());

        if (article == null)
        {
            return NotFound(Res.Provider(new { }, $"{title} no encontrado", false));
        }
        return Ok(Res.Provider(article, $"{title} ha sido encontrado", true));

    }


    [HttpPost]
    public async Task<IActionResult> Insertar([FromBody] CreateArticle article)
    {
        Article newArticle = TinyMapper.Map<Article>(article);


        bool result = await _ArticleService.Create(newArticle);
        if (result)
            return Ok(Res.Provider(newArticle, "Agregado con exito", true));

        return Ok(Res.Provider(new { }, "No se pudo agregar", false));

    }

    [HttpDelete]
    [Route("{Id:int}")]
    public async Task<IActionResult> Delete(int Id)
    {
        bool result = await _ArticleService.Delete(Id);

        if (result)
            return Ok(Res.Provider(new { }, "Eliminado con exito", true));

        return Ok(Res.Provider(new { }, "No se pudo eliminar", false));
    }
}