using System;

namespace SinaWeibo.Models
{
    public class Status
    {
        /// <summary>
        /// 微博创建时间
        /// </summary>
        public string created_at { get; set; }


        /// <summary>
        /// 微博ID
        /// </summary>
        public Int64 id { get; set; }


        /// <summary>
        /// 微博MID
        /// </summary>
        public Int64 mid { get; set; }


        /// <summary>
        /// 字符串型的微博ID
        /// </summary>
        public string idstr { get; set; }


        /// <summary>
        /// 微博信息内容
        /// </summary>
        public string text { get; set; }


        /// <summary>
        /// 微博来源
        /// </summary>
        public string source { get; set; }


        /// <summary>
        /// 是否已收藏，true：是，false：否
        /// </summary>
        public Boolean favorited { get; set; }


        /// <summary>
        /// 是否被截断，true：是，false：否
        /// </summary>
        public Boolean truncated { get; set; }


        /// <summary>
        /// （暂未支持）回复ID
        /// </summary>
        public string in_reply_to_status_id { get; set; }


        /// <summary>
        /// （暂未支持）回复人UID
        /// </summary>
        public string in_reply_to_user_id { get; set; }


        /// <summary>
        /// （暂未支持）回复人昵称
        /// </summary>
        public string in_reply_to_screen_name { get; set; }


        /// <summary>
        /// 缩略图片地址，没有时不返回此字段
        /// </summary>
        public string thumbnail_pic { get; set; }


        /// <summary>
        /// 中等尺寸图片地址，没有时不返回此字段
        /// </summary>
        public string bmiddle_pic { get; set; }


        /// <summary>
        /// 原始图片地址，没有时不返回此字段
        /// </summary>
        public string original_pic { get; set; }


        /// <summary>
        /// 地理信息字段 详细
        /// </summary>
        public object geo { get; set; }


        /// <summary>
        /// 微博作者的用户信息字段 详细
        /// </summary>
        public object user { get; set; }


        /// <summary>
        /// 被转发的原微博信息字段，当该微博为转发微博时返回 详细
        /// </summary>
        public object retweeted_status { get; set; }


        /// <summary>
        /// 转发数
        /// </summary>
        public int reposts_count { get; set; }


        /// <summary>
        /// 评论数
        /// </summary>
        public int comments_count { get; set; }


        /// <summary>
        /// 表态数
        /// </summary>
        public int attitudes_count { get; set; }


        /// <summary>
        /// 暂未支持
        /// </summary>
        public int mlevel { get; set; }


        /// <summary>
        /// 微博的可见性及指定可见分组信息。该object中type取值，0：普通微博，1：私密微博，3：指定分组微博，4：密友微博；list_id为分组的组号
        /// </summary>
        public object visible { get; set; }


        /// <summary>
        /// 微博配图ID。多图时返回多图ID，用来拼接图片url。用返回字段thumbnail_pic的地址配上该返回字段的图片ID，即可得到多个图片url。
        /// </summary>
        public object pic_ids { get; set; }


        /// <summary>
        /// 微博流内的推广微博ID
        /// </summary>
        public object[] ad { get; set; }
    }
}
