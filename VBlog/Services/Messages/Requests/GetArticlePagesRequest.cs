using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBlog.Services.Messages.Requests
{
    public class GetArticlePagesRequest
    {
        public GetArticlePagesRequest(int pageIndex,int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }

        public string Tag { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
    }
}
