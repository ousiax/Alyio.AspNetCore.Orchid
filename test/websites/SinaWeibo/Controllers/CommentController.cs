using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SinaWeibo.Models;

namespace SinaWeibo.Controllers
{
    /// <summary>
    /// 评论
    /// </summary>
    [Route("comments")]
    public class CommentController
    {
        ///// <summary>
        ///// 根据微博ID返回某条微博的评论列表
        ///// </summary>
        ///// <returns></returns>
        /// <summary>
        /// 根据微博ID返回某条微博的评论列表
        /// </summary>
        /// <param name="access_token">采用OAuth授权方式为必填参数，OAuth授权后获得。</param>
        /// <param name="id">需要查询的微博ID。</param>
        /// <param name="since_id">若指定此参数，则返回ID比since_id大的评论（即比since_id时间晚的评论），默认为0。</param>
        /// <param name="max_id">若指定此参数，则返回ID小于或等于max_id的评论，默认为0。</param>
        /// <param name="count">单页返回的记录条数，默认为50。</param>
        /// <param name="page">返回结果的页码，默认为1。</param>
        /// <param name="filter_by_author">作者筛选类型，0：全部、1：我关注的人、2：陌生人，默认为0。 </param>
        /// <returns></returns>
        [HttpGet("show.json")]
        public IList<Comment> Show(
            [FromQuery] string access_token,
            [FromQuery]long id,
            [FromQuery] long since_id,
            [FromQuery]long max_id,
            [FromQuery]int count,
            [FromQuery]int page,
            [FromQuery]int filter_by_author)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取当前登录用户所发出的评论列表 
        /// </summary>
        /// <param name="access_token">采用OAuth授权方式为必填参数，OAuth授权后获得。</param>
        /// <param name="since_id">若指定此参数，则返回ID比since_id大的评论（即比since_id时间晚的评论），默认为0。</param>
        /// <param name="max_id">若指定此参数，则返回ID小于或等于max_id的评论，默认为0。</param>
        /// <param name="count">单页返回的记录条数，默认为50。</param>
        /// <param name="page">返回结果的页码，默认为1。</param>
        /// <param name="filter_by_source">来源筛选类型，0：全部、1：来自微博的评论、2：来自微群的评论，默认为0。  </param>
        /// <returns></returns>
        [HttpGet("by_me.json")]
        public IList<Comment> ByMe(
            [FromQuery] string access_token,
            [FromQuery] long since_id,
            [FromQuery]long max_id,
            [FromQuery]int count,
            [FromQuery]int page,
            [FromQuery]int filter_by_source)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取当前登录用户所接收到的评论列表  
        /// </summary>
        /// <param name="access_token">采用OAuth授权方式为必填参数，OAuth授权后获得。</param>
        /// <param name="since_id">若指定此参数，则返回ID比since_id大的评论（即比since_id时间晚的评论），默认为0。</param>
        /// <param name="max_id">若指定此参数，则返回ID小于或等于max_id的评论，默认为0。</param>
        /// <param name="count">单页返回的记录条数，默认为50。</param>
        /// <param name="page">返回结果的页码，默认为1。</param>
        /// <param name="filter_by_author">作者筛选类型，0：全部、1：我关注的人、2：陌生人，默认为0。</param>
        /// <param name="filter_by_source">来源筛选类型，0：全部、1：来自微博的评论、2：来自微群的评论，默认为0。  </param>
        /// <returns></returns>
        [HttpGet("to_me.json")]
        public IList<Comment> ToMe(
            [FromQuery] string access_token,
            [FromQuery] long since_id,
            [FromQuery]long max_id,
            [FromQuery]int count,
            [FromQuery]int page,
            [FromQuery]int filter_by_author,
            [FromQuery]int filter_by_source)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 对一条微博进行评论
        /// </summary>
        /// <param name="access_token">采用OAuth授权方式为必填参数，OAuth授权后获得。</param>
        /// <param name="comment">评论内容，必须做URLencode，内容不超过140个汉字。 </param>
        /// <param name="id">需要评论的微博ID。 </param>
        /// <param name="comment_ori">当评论转发微博时，是否评论给原微博，0：否、1：是，默认为0。 </param>
        /// <param name="rip">开发者上报的操作用户真实IP，形如：211.156.0.1。</param>
        /// <returns></returns>
        [HttpPost("create.json")]
        public Comment Create(
            [FromQuery] string access_token,
            [FromQuery] long comment,
            [FromQuery]long id,
            [FromQuery]int comment_ori,
            [FromQuery]int rip)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除一条评论
        /// </summary>
        /// <param name="access_token">采用OAuth授权方式为必填参数，OAuth授权后获得。 </param>
        /// <param name="cid">要删除的评论ID，只能删除登录用户自己发布的评论。</param>
        /// <returns></returns>
        [HttpDelete("destroy.json")]
        public Comment Destroy(string access_token, long cid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 回复一条评论
        /// </summary>
        /// <param name="access_token">采用OAuth授权方式为必填参数，OAuth授权后获得。 </param>
        /// <param name="cid">需要回复的评论ID。 </param>
        /// <param name="id">需要评论的微博ID。 </param>
        /// <param name="comment">回复评论内容，必须做URLencode，内容不超过140个汉字。 </param>
        /// <param name="without_mention">回复中是否自动加入“回复@用户名”，0：是、1：否，默认为0。 </param>
        /// <param name="comment_ori">当评论转发微博时，是否评论给原微博，0：否、1：是，默认为0。 </param>
        /// <param name="rip">开发者上报的操作用户真实IP，形如：211.156.0.1。 </param>
        /// <returns></returns>
        [HttpPost("reply.json")]
        public Comment Reply(
                  [FromQuery] string access_token,
                  [FromQuery] long cid,
                  [FromQuery] long id,
                  [FromQuery] string comment,
                  [FromQuery] int without_mention,
                  [FromQuery] int comment_ori,
                  [FromQuery] string rip)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 微博.开放平台
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost("helo.json")]
        public Comment Helo(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
