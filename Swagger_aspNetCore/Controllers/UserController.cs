using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_aspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        #region Get
        /// <summary>
        /// 带一个参数的Get请求
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public Object Get(string name)
        {
            string result = $"My name is {name}";
            return new { status = "Success", data = result };
        }
        #endregion

        #region Post
        /// <summary>
        /// 带一个参数的Post请求
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public string Post(string name)
        {
            return $"Add user {name}";
        }
        #endregion

        #region Put
        /// <summary>
        /// 带一个参数的Put请求
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPut]
        public string Put(int id, string name)
        {
            return $"Update user set name as {name} where id is {id}";
        }
        #endregion

        #region Delete
        /// <summary>
        /// 带一个参数的Delete请求
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete]
        public string Delete(string name)
        {
            return $"Delete {name}";
        }
        #endregion
    }
}
