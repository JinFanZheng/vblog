using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VBlog.Services.Messages;
using VBlog.Services.Messages.Models;
using VBlog.Services.Messages.Requests;
using VBlog.Services.Messages.Responses;

namespace VBlog.Services.Interfaces
{
    public interface IArticleService
    {
        Task<APIResult<SaveResponse>> SaveAsync(ArticleModel model);
        Task<APIResult<string>> DeleteAsync(string  guid);
        Task<APIResult<Page<ArticleModel>>> GetPagesAsync(GetArticlePagesRequest request);
        Task<APIResult<ArticleModel>> GetDetailAsync(string guid);
    }
}
