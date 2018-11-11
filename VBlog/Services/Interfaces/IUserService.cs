using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VBlog.Services.Messages;
using VBlog.Services.Messages.Requests;
using VBlog.Services.Messages.Responses;

namespace VBlog.Services.Interfaces
{
    public interface IUserService
    {
        Task<APIResult<LoginResponse>> LoginAsync(LoginRequest request);
    }
}
