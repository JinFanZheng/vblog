using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBlog.Domain.Models
{
    /// <summary>
    /// 基类
    /// </summary>
    [PrimaryKey("Id")]
    public class EntityCore
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// GUID
        /// </summary>
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
