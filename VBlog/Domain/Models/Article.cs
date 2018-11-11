using NPoco;

namespace VBlog.Domain.Models
{
    [TableName("Articles")]
    public class Article:EntityCore
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// SEO 关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// SEO 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 所属分类
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 置顶
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 允许评论
        /// </summary>
        public bool CanComment { get; set; }
    }
}
