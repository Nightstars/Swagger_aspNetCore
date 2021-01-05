using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_aspNetCore.Services
{
    public interface IUserService
    {
        public bool ValidateCredentials(String username, String password);
    }
}
