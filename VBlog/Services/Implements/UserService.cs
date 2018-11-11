using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VBlog.Domain.Models;
using VBlog.Helpers;
using VBlog.Services.Interfaces;
using VBlog.Services.Messages;
using VBlog.Services.Messages.Requests;
using VBlog.Services.Messages.Responses;

namespace VBlog.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IDatabase _ctx;

        public UserService(IDatabase ctx)
        {
            _ctx = ctx;
        }
        public async Task<APIResult<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var res = new APIResult<LoginResponse>();
            try
            {
                var entity = await _ctx.Query<User>()
                    .Where(whereExpression: p => p.UserName == request.UserName)
                    .FirstOrDefaultAsync();
                if (entity == null)
                {
                    res.Message = "找不到用户信息.";
                    return res;
                }
                if (entity.Password != request.Password.ToMd5())
                {
                    res.Message = "用户名或密码错误.";
                    return res;
                }

                res.Data = new LoginResponse
                {
                    UserName = entity.UserName,
                    Id = entity.Id,
                    Guid = entity.Guid
                };
                res.Success = true;
                res.Message = "登录成功.";
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
