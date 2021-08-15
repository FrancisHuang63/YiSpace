using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YiSpace.Models.API
{
    /// <summary>
    /// token model
    /// </summary>
    public class ApiToken
    {
        private readonly HttpContext _httpContext;

        public ApiToken(Microsoft.AspNetCore.Http.HttpContext context)
        {
            _httpContext = context;
        }

        /// <summary>
        /// 開始時間
        /// </summary>
        public string issued { get; set; }
        /// <summary>
        /// 有效時間
        /// </summary>
        public string expire { get; set; }
        /// <summary>
        /// 會員ID
        /// </summary>
        public long? memberId { get; set; }

        public ApiToken GetJwtAuthObject()
        {
            if (!string.IsNullOrWhiteSpace(_httpContext.Request.Headers["Authorization"].ToString()))
            {
                var authHeader = _httpContext.Request.Headers["Authorization"];
                var authBits = authHeader.ToString().Replace("Bearer ", "").Replace(" ", "");
                var secret = DAL.Information.APISecret.ToString();
                var result = Jose.JWT.Decode<ApiToken>(authBits, System.Text.Encoding.UTF8.GetBytes(secret), Jose.JwsAlgorithm.HS256);
                return result;
            }
            return null;
        }
    }
}