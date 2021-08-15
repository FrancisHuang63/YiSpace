using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YiSpace.Models
{
    public class APIResponseBaseModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        /// <summary>
        /// 取得的 token 值, 若認證失敗為空白, token 有效期為 30 分鐘
        /// </summary>
        public string MemberToken { get; set; }
        /// <summary>
        /// MemberToken到期時間
        /// </summary>
        public string Expire { get; set; }
    }
}
