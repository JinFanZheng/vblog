using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBlog.Services.Messages.Responses
{
    public class LoginResponse
    {
        public string Guid { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
