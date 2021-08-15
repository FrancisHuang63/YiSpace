using DAL.Models;
using DAL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using YiSpace.Models;
using YiSpace.Services;

namespace YiSpace.Controllers
{
    [Authorize(Roles = "1,2,3,99")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetIssueJson()
        {
            APIResponseBaseModel<IEnumerable<TicketIssueViewModel>> model = new APIResponseBaseModel<IEnumerable<TicketIssueViewModel>>();
            try
            {
                var curUser = SrvUser.GetCurrentUser(HttpContext);
                Dictionary<string, object> parms = new Dictionary<string, object>();
                model.Data = SrvTicketIssue.GetTicketIssueViewItems(parms);
                model.Success = true;
            }
            catch (Exception ex)
            {
                model.Message = ex.Message;
            }

            return Json(model);
        }

        public JsonResult GetCurUser()
        {
            APIResponseBaseModel<UserModel> model = new APIResponseBaseModel<UserModel>();
            try
            {
                model.Data = SrvUser.GetCurrentUser(HttpContext);
                model.Success = true;
            }
            catch (Exception ex)
            {
                model.Message = ex.Message;
            }

            return Json(model);
        }

        public JsonResult DelIssue(long id)
        {
            APIResponseBaseModel<bool> model = new APIResponseBaseModel<bool>();
            try
            {
                model.Data = SrvTicketIssue.Delete(id) > 0;
                model.Success = true;
            }
            catch (Exception ex)
            {
                model.Message = ex.Message;
            }

            return Json(model);
        }

        public IActionResult Edit(long? id)
        {
            UserModel curUser = SrvUser.GetCurrentUser(HttpContext);
            IEnumerable<UserModel> assignUsers = SrvUser.GetItems(new Dictionary<string, object>() { { nameof(UserModel.Level), (byte)UserLevel.RD } });

            TicketIssueModel model;
            if (id.HasValue)
            {
                model = SrvTicketIssue.GetItem(id.Value);
            }
            else
            {
                model = new TicketIssueModel()
                {
                    Type = curUser.Level == UserLevel.PM ? TicketIssueType.FeatureRequest : TicketIssueType.Bug,
                    State = TicketIssueState.Unresolved,
                };
            }

            ViewBag.AsignUser = assignUsers;
            ViewBag.CurUser = curUser;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(TicketIssueModel item)
        {
            UserModel curUser = SrvUser.GetCurrentUser(HttpContext);
            bool isNew = item.ID == 0;
            if (isNew)
            {
                item.ID = DAL.Tools.GetNewSN();
                item.Creator = curUser.ID;
                item.CreateTime = DateTime.Now;
                SrvTicketIssue.SetItem(item, isNew, new string[] { });
            }
            else
            {
                string[] updExceptCol = { nameof(item.ID), nameof(item.Creator), nameof(item.CreateTime) };
                item.Modifier = curUser.ID;
                item.ModifyTime = DateTime.Now;
                SrvTicketIssue.SetItem(item, isNew, updExceptCol);
            }

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
