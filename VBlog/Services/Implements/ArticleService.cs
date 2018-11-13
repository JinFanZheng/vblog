using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VBlog.Domain.Models;
using VBlog.Extensions;
using VBlog.Helpers;
using VBlog.Services.Interfaces;
using VBlog.Services.Messages;
using VBlog.Services.Messages.Models;
using VBlog.Services.Messages.Requests;
using VBlog.Services.Messages.Responses;

namespace VBlog.Services.Implements
{
    public class ArticleService : IArticleService
    {
        private readonly IDatabase _ctx;

        public ArticleService(IDatabase ctx)
        {
            _ctx = ctx;
        }

        public async Task<APIResult<string>> DeleteAsync(string guid)
        {
            var res = new APIResult<string>();
            try
            {
                var item = await _ctx.Query<Article>().Where(whereExpression: p => p.Guid == guid).FirstOrDefaultAsync();
                if (item == null)
                {
                    res.Success = true;
                    res.Message = "操作成功.";
                    return res;
                }
                var snapshot = _ctx.StartSnapshot(item);
                // 逻辑删除
                item.IsDelete = true;

                var result = await _ctx.UpdateAsync(item, snapshot.UpdatedColumns());
                var flag = result > 0;

                res.Success = true;
                res.Message = "操作成功.";
            }
            catch (Exception ex)
            {
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }

        public async Task<APIResult<ArticleModel>> GetDetailAsync(string guid)
        {
            var res = new APIResult<ArticleModel>();
            try
            {
                var model = await _ctx.Query<Article>()
                    .Where(whereExpression: p => p.Guid == guid)
                    .FirstOrDefaultAsync();
                if (model == null)
                {
                    res.Message = "数据不存在.";
                    return res;
                }
                res.Data = new ArticleModel
                {
                    Guid = model.Guid,
                    CreateTime = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Id = model.Id,
                    Sort = model.Sort,
                    IsEnabled = model.IsEnabled,
                    Title = model.Title,
                    Summary = model.Summary,
                    Content = model.Content,
                    Keyword = model.Keyword,
                    Description = model.Description,
                    Tag = model.Tag,
                    Category = model.Category,
                    IsTop = model.IsTop,
                    CanComment = model.CanComment
                };
                res.Success = true;
                res.Message = "获取成功.";
            }
            catch (Exception ex)
            {
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }

        public async Task<APIResult<Page<ArticleModel>>> GetPagesAsync(GetArticlePagesRequest request)
        {
            var res = new APIResult<Page<ArticleModel>>();
            try
            {
                var result = await _ctx.Query<Article>()
                    .HasWhere(request.Title, p => p.Title.Contains(request.Title))
                    .HasWhere(request.Category, p => p.Category.Contains(request.Category))
                    .HasWhere(request.Tag, p => p.Tag.Contains(request.Tag))
                    .OrderByDescending(p => p.Id)
                    .ThenByDescending(p => p.IsTop)
                    .ThenByDescending(p => p.CreateTime)
                    .ToPageAsync(request.PageIndex, request.PageSize);
                if (result == null || result.Items == null || result.Items.Count == 0)
                {
                    res.Message = "暂无数据.";
                    return res;
                }

                res.Data = result.ToViewPage(model => new ArticleModel
                {
                    Key = model.Id.ToString(),
                    Guid = model.Guid,
                    CreateTime = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Id = model.Id,
                    Sort = model.Sort,
                    IsEnabled = model.IsEnabled,
                    Title = model.Title,
                    Summary = model.Summary,
                    // Content = model.Content,
                    Keyword = model.Keyword,
                    Description = model.Description,
                    Tag = model.Tag,
                    Category = model.Category,
                    IsTop = model.IsTop,
                    CanComment = model.CanComment
                });
                res.Success = true;
                res.Message = "获取成功.";
            }
            catch (Exception ex)
            {
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }

        public async Task<APIResult<SaveResponse>> SaveAsync(ArticleModel model)
        {
            var res = new APIResult<SaveResponse>();
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }
                Article entity;
                var flag = false;
                if (string.IsNullOrEmpty(model.Guid))
                {
                    entity = new Article
                    {
                        Sort = model.Sort,
                        IsEnabled = model.IsEnabled,
                        Title = model.Title,
                        Summary = model.Summary,
                        Content = model.Content,
                        Keyword = model.Keyword,
                        Description = model.Description,
                        Tag = model.Tag,
                        Category = model.Category,
                        IsTop = model.IsTop,
                        CanComment = model.CanComment
                    };
                    var result = await _ctx.InsertAsync(entity);

                    flag = result != null && Convert.ToInt32(result) > 0;
                }
                else
                {
                    entity = await _ctx.Query<Article>().Where(whereExpression: p => p.Guid == model.Guid).FirstOrDefaultAsync();
                    if (entity == null)
                    {
                        res.Message = "找不到数据.";
                    }

                    var snapshot = _ctx.StartSnapshot(entity);

                    entity.Sort = model.Sort;
                    entity.IsEnabled = model.IsEnabled;
                    entity.Title = model.Title;
                    entity.Summary = model.Summary;
                    entity.Content = model.Content;
                    entity.Keyword = model.Keyword;
                    entity.Description = model.Description;
                    entity.Tag = model.Tag;
                    entity.Category = model.Category;
                    entity.IsTop = model.IsTop;
                    entity.CanComment = model.CanComment;

                    flag = await _ctx.UpdateAsync(entity, snapshot.UpdatedColumns()) > 0;
                }
                res.Success = flag;
                res.Message = flag ? "保存成功." : "保存失败,请重试.";
                res.Data = new SaveResponse
                {
                    Id = entity.Id,
                    Guid = entity.Guid
                };
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.StatusCode = 500;
            }
            return res;
        }
    }
}
