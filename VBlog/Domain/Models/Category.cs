using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBlog.Domain.Models
{
    /// <summary>
    /// 文章分类
    /// </summary>
    [TableName("Categories")]
    public class Category:EntityCore
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
