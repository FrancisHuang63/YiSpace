using DAL.Models;
using DAL.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiSpace.Attributes;
using YiSpace.Models;
using YiSpace.Services;

namespace YiSpace.API
{
    [Route("api/[controller]")]
    [ApiController]
    [JwtAuthActionFilter]
    [Header(Name = "Authorization", Type = "string", Default = "Bearer ")]
    public class GetTicketIssueListController : ControllerBase
    {
        /// <summary>
        /// 取得議題列表，需傳入會員Token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public APIResponseBaseModel<IEnumerable<TicketIssueViewModel>> Get()
        {
            APIResponseBaseModel<IEnumerable<TicketIssueViewModel>> result = new APIResponseBaseModel<IEnumerable<TicketIssueViewModel>>();

            try
            {
                result.Data = SrvTicketIssue.GetTicketIssueViewItems(new Dictionary<string, object>());
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
