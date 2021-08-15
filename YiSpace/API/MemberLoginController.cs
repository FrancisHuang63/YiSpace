using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiSpace.Models;
using DAL.Models;
using YiSpace.Models.API;
using YiSpace.Services;
using Jose;

namespace YiSpace.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberLoginController : ControllerBase
    {
        /// <summary>
        /// 會員登入
        /// </summary>
        [HttpPost]
        public APIResponseBaseModel<UserModel> Post([FromBody] API_MemberLogin data)
        {
            APIResponseBaseModel<UserModel> result = new APIResponseBaseModel<UserModel>();

            try
            {
                var user = SrvUser.GetItem(data.Account, data.Password);
                if(user != null)
                {
                    result.Data = user;
                    result.Success = true;

                    #region 產生會員Token
                    var now = DateTime.UtcNow.AddHours(08);
                    var expire = now.AddMinutes(30);

                    var payload = new ApiToken(HttpContext)
                    {
                        issued = now.ToString(),
                        expire = expire.ToString(),
                        memberId = user.ID
                    };

                    result.MemberToken = JWT.Encode(payload, System.Text.Encoding.UTF8.GetBytes(DAL.Information.APISecret), JwsAlgorithm.HS256);
                    result.Expire = expire.ToString("yyyy.MM.dd HH:mm:ss");
                    #endregion
                }
                else
                {
                    result.Message = "帳號或密碼錯誤";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.ToString();
            }

            return result;
        }
    }
}
