using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YiSpace.Models.API
{
    public class API_MemberLogin
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }
    }
}
