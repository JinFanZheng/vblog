using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VBlog.Services.Messages;
using VBlog.Services.Messages.Models;

namespace VBlog.Services.Interfaces
{
    public interface ITagService
    {
        Task<APIResult<List<TagModel>>> GetAllAsync();

        Task<APIResult<SaveResponse>> InsertAsync(TagModel model);
        Task<APIResult<string>> DeleteAsync(int id);
    }
}
