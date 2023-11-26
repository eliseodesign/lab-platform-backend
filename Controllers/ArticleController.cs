
using LabPlatform.Models;
using LabPlatform.Models.DTOs;
using LabPlatform.Schemas;
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
            return Ok(articles);
        }

        [HttpGet]
        [Route("{type}")]

        public async Task<IActionResult> GetNotices(string type)
        {
            List<ArticleDTO> articles = await _ArticleService.GetAll();
            var filter = articles.Where(article => article.ArticleType == type).ToList();

            var min = filter.Select(article => TinyMapper.Map<ArticleMinDTO>(article)).ToList();
            return Ok(min);
        }

        [HttpGet]
        [Route("{title}")]
        public async Task<IActionResult> GetNoticeById(string title)
        {
            title = title.Replace("-", " ");

            List<ArticleDTO> articles = await _ArticleService.GetAll();

            var article = articles.FirstOrDefault(a => a.Title.ToLower() == title.ToLower());

            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }


        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] CreateArticle article)
        {
            Article newArticle = TinyMapper.Map<Article>(article);


            bool result = await _ArticleService.Create(newArticle);

            return StatusCode(StatusCodes.Status200OK, new { success = result });
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            bool result = await _ArticleService.Delete(Id);

            return StatusCode(StatusCodes.Status200OK, new { success = result });
        }
}