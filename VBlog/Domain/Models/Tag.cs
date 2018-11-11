using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBlog.Domain.Models
{
    /// <summary>
    /// 标签
    /// </summary>
    [TableName("Tags")]
    public class Tag:EntityCore
    {
        /// <summary>
        /// 标签
        /// </summary>
        public string Name { get; set; }
    }
}
