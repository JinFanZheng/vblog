using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NPoco;
using VBlog.Services.Interfaces;
using VBlog.Services.Messages;
using VBlog.Services.Messages.Models;
using VBlog.Services.Messages.Requests;
using VBlog.Services.Messages.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VBlog.Controllers
{
    public class ArticleController : ApiControllerBase
    {

        private readonly IArticleService _service;
        public ArticleController(IArticleService service)
        {
            _service = service;
        }
        [HttpPost("insert")]
        public async Task<APIResult<SaveResponse>> Insert([FromBody]ArticleModel model) => await _service.SaveAsync(model);
        [HttpPost("update")]
        public async Task<APIResult<SaveResponse>> Update([FromBody]ArticleModel model) => await _service.SaveAsync(model);
        [HttpGet("delete")]
        public async Task<APIResult<string>> Delete(string guid) => await _service.DeleteAsync(guid);
        [HttpPost("getpages")]
        public async Task<APIResult<Page<ArticleModel>>> GetPages([FromBody]GetArticlePagesRequest request) => await _service.GetPagesAsync(request);

    }
}
