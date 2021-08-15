using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiSpace.Attributes;
using YiSpace.Models;
using YiSpace.Models.API;
using YiSpace.Services;

namespace YiSpace.API
{
    [Route("api/[controller]")]
    [ApiController]
    [JwtAuthActionFilter]
    [Header(Name = "Authorization", Type = "string", Default = "Bearer ")]
    public class EditTicketIssueController : ControllerBase
    {
        /// <summary>
        /// 新增或編輯議題(新增者ID為0)，需傳入會員Token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public APIResponseBaseModel<bool> Post([FromBody] TicketIssueModel data)
        {
            APIResponseBaseModel<bool> result = new APIResponseBaseModel<bool>();

            try
            {
                bool isNew = data.ID == 0;
                ApiToken apiToken = new ApiToken(HttpContext);
                if (apiToken.memberId.HasValue)
                {
                    UserModel curUser = SrvUser.GetItem(apiToken.memberId.Value);
                    if (isNew)
                    {
                        data.ID = DAL.Tools.GetNewSN();
                        data.Creator = curUser.ID;
                        data.CreateTime = DateTime.Now;
                        SrvTicketIssue.SetItem(data, isNew, new string[] { });
                    }
                    else
                    {
                        string[] updExceptCol = { nameof(data.ID), nameof(data.Creator), nameof(data.CreateTime) };
                        data.Modifier = curUser.ID;
                        data.ModifyTime = DateTime.Now;
                        SrvTicketIssue.SetItem(data, isNew, updExceptCol);
                    }

                    result.Data = true;
                    result.Success = true;
                }
                else
                {
                    result.Data = false;
                    result.Message = "使用者不存在";
                    result.Success = true;
                }
            }
            catch (Exception e)
            {
                result.Data = false;
                result.Success = false;
                result.Message = e.Message.ToString();
            }

            return result;
        }
    }
}
