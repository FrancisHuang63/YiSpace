using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiSpace.Services;

namespace YiSpace.Controllers
{
    [Authorize(Roles = "1,2,3,99")]
    public class MemberController : Controller
    {
        public IActionResult Edit(long? id)
        {
            UserModel model;
            if (id.HasValue)
            {
                model = SrvUser.GetItem(id.Value);
            }
            else
            {
                if (User.IsInRole(((int)UserLevel.Administrator).ToString()))
                {
                    model = new UserModel()
                    {
                        Level = UserLevel.QA
                    };
                }
                else
                {
                    model = SrvUser.GetCurrentUser(HttpContext);
                }
                
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(UserModel item, bool isNewPassword = false)
        {
            bool isNew = item.ID == 0;
            if (isNew)
            {
                item.ID = DAL.Tools.GetNewSN();
                item.Password = DAL.Tools.GetSHA256(item.Password);
                SrvUser.SetItem(item, isNew, new string[] { });
            }
            else
            {
                List<string> updExceptCol = new List<string>();
                updExceptCol.Add(nameof(item.ID));
                if (isNewPassword)
                {
                    item.Password = DAL.Tools.GetSHA256(item.Password);
                }
                else
                {
                    updExceptCol.Add(nameof(item.Password));
                }
                SrvUser.SetItem(item, isNew, updExceptCol.ToArray());
            }

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
