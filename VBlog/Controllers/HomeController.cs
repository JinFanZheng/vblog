using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VBlog.Services.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        public HomeController(ICategoryService categoryService, IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            if (categories.Success)
            {
                ViewBag.Categories = categories.Data;
            }
            var articles = await _articleService.GetPagesAsync(new Services.Messages.Requests.GetArticlePagesRequest(1, 10));
            Console.WriteLine(articles);
            if (articles.Success)
            {
                ViewBag.Articles = articles.Data;
            }
            return View();
        }

        public async Task<IActionResult> Detail(string guid)
        {
            var article = await _articleService.GetDetailAsync(guid);
            if (article.Success)
            {
                return View(article.Data);
            }
            return View();
        }
    }
}
