using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_aspNetCore.Models
{
    public class TokenObj
    {
        public string token { get; set; }//token内容

        public long expires { get; set; }//过期时间
    }
}
