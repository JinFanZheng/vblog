using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VBlog.Services.Messages;
using VBlog.Services.Messages.Models;

namespace VBlog.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<APIResult<List<CategoryModel>>> GetAllAsync();
        Task<APIResult<SaveResponse>> InsertAsync(CategoryModel model);
        Task<APIResult<string>> DeleteAsync(int id);
    }
}
