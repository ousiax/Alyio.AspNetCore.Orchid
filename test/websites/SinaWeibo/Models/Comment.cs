using System;

namespace SinaWeibo.Models
{
    /// <summary>
    /// 评论
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// 评论创建时间
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// 评论的ID
        /// </summary>
        public Int64 id { get; set; }

        /// <summary>
        /// 评论的内容
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 评论的来源
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// 评论作者的用户信息字段
        /// </summary>
        public User user { get; set; }

        /// <summary>
        /// 评论的MID
        /// </summary>
        public string mid { get; set; }

        /// <summary>
        /// 字符串型的评论ID
        /// </summary>
        public string idstr { get; set; }

        /// <summary>
        /// 评论的微博信息字段
        /// </summary>
        public Status status { get; set; }

        /// <summary>
        /// 评论来源评论，当本评论属于对另一评论的回复时返回此字段
        /// </summary>
        public object reply_comment { get; set; }
    }
}
