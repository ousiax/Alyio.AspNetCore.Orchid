using System;
using System.Collections.Generic;
using AspNetNet451Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetNet451Website.Controllers
{
    /// <summary>
    /// 难以抗拒你的容颜
    /// </summary>
    [Route("api/nodes")]
    public class NodesController
    {
        /// <summary>
        /// 你仿佛没有见过我 只是让我梦成空 伤心 不语 退缩
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Node> Get()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 幻想也许是你假装不看我 让我得不到更珍惜所有
        /// </summary>
        /// <param name="id">这是ID</param>
        /// <param name="sword">这是剑</param>
        /// <returns></returns>
        [HttpGet("{id}/{sword}")]
        public Node Get(int id, string sword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  我试试对你微微笑 你总视而不见 何必 何必 何必
        /// </summary>
        /// <param name="value">这是Value</param>
        /// <returns></returns>
        [HttpPost]
        public Node Post([FromBody]Node value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 却又难以抗拒难以放弃 就算你对我说别再烦我
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Node Put(int id, [FromBody]Node value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 你难以靠近 难以不在想念
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
