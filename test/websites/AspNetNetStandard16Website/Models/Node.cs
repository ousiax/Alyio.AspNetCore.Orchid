using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetNet451Website.Models
{
    /// <summary>
    /// 爬升 速度将我推向椅背
    /// </summary>
    public sealed class Node
    {
        /// <summary>
        /// 模糊的城市 慢慢飞出我的视线
        /// </summary>
        [Required]
        public Node Parent { get; set; }

        /// <summary>
        /// 呼吸 提醒我活着的证明
        /// </summary>
        [MaxLength(100)]
        public string FilterOn { get; set; }

        /// <summary>
        /// 飞机正在抵抗地球 我正在抵抗你
        /// </summary>
        public List<Node> Children { get; set; }

        /// <summary>
        /// 远离地面 快接近三万英尺的距离
        /// </summary>
        public List<object> FilterValues { get; set; }

        /// <summary>
        /// 思念像黏着身体的引力 还拉着泪不停的往下滴
        /// </summary>
        public IDictionary<string, object> Options { get; set; }

        public Node()
        {
        }
    }
}