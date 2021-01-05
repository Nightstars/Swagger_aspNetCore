using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_aspNetCore.Services
{
    public class UserService : IUserService
    {
        #region ValidateCredentials
        /// <summary>
        /// ValidateCredentials
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateCredentials(string username, string password)
        {
            return username.Equals("christ") && password.Equals("123456");
        }
        #endregion
    }
}
