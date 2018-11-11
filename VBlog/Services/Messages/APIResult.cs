using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBlog.Services.Messages
{
    /// <summary>
    /// 接口统一结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class APIResult<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set; } = 200;
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}
