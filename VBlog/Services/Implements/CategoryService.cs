using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VBlog.Domain.Models;
using VBlog.Services.Interfaces;
using VBlog.Services.Messages;
using VBlog.Services.Messages.Models;

namespace VBlog.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly IDatabase _ctx;

        public CategoryService(IDatabase ctx)
        {
            _ctx = ctx;
        }
        public async Task<APIResult<List<CategoryModel>>> GetAllAsync()
        {
            var res = new APIResult<List<CategoryModel>>();
            try
            {
                var result = await _ctx.Query<Category>().ToListAsync();
                if (result == null || result.Count == 0)
                {
                    res.Message = "暂无数据.";
                    return res;
                }

                res.Data = result.Select(p => new CategoryModel
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList();
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
        public async Task<APIResult<SaveResponse>> InsertAsync(CategoryModel model)
        {
            var res = new APIResult<SaveResponse>();
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }
                var entity = new Category
                {
                    Name = model.Name
                };

                var result = await _ctx.InsertAsync(entity);
                var flag = result != null && Convert.ToInt32(result) > 0;

                res.Data = new SaveResponse
                {
                    Id = entity.Id,
                    Guid = entity.Guid
                };
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
        public async Task<APIResult<string>> DeleteAsync(int id)
        {
            var res = new APIResult<string>();
            try
            {
                var result = await _ctx.DeleteMany<Article>().Where(whereExpression: p => p.Id == id).ExecuteAsync();
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
    }
}
