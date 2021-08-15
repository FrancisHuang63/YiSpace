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
    public class GetTicketIssueController : ControllerBase
    {
        /// <summary>
        /// 取得議題，需傳入會員Token
        /// </summary>
        /// <param name="ID">議題ID</param>
        /// <returns></returns>
        [HttpGet]
        public APIResponseBaseModel<TicketIssueModel> Get(long ID)
        {
            APIResponseBaseModel<TicketIssueModel> result = new APIResponseBaseModel<TicketIssueModel>();

            try
            {
                result.Data = SrvTicketIssue.GetItem(ID);
                result.Success = true;

            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message.ToString();
            }

            return result;
        }
    }
}
