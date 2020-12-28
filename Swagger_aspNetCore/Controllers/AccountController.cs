using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swagger_aspNetCore.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Swagger_aspNetCore.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        #region initialize
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Login
        /// <summary>
        /// <![CDATA[获取访问令牌]]>
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        public RestfulData<TokenObj> Login(string user, string password)//同步方法
        {
            var result = new RestfulData<TokenObj>();
            try
            {
                if (string.IsNullOrEmpty(user)) throw new ArgumentNullException("user", "用户名不能为空！");
                if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password", "密码不能为空！");

                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name,user),
                    new Claim(ClaimTypes.NameIdentifier,password),
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                var expires = DateTime.Now.AddDays(30);//
                var token = new JwtSecurityToken(
                            issuer: _configuration["issuer"],
                            audience: _configuration["audience"],
                            claims: claims,
                            notBefore: DateTime.Now,
                            expires: expires,
                            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

                //生成Token
                string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                result.code = 200;
                result.data = new TokenObj() { token = jwtToken, expires = expires.ToFileTimeUtc() };
                result.message = "授权成功！";
                result.url = "https://github.com/Nightstars";
                return result;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                result.code = 400;
                return result;
            }
        }
        #endregion
    }
}
