using DAL.DataAccess;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YiSpace.Services
{
    public class SrvUser
    {
        public static UserModel GetCurrentUser(Microsoft.AspNetCore.Http.HttpContext context)
        {
            UserModel curUser = null;
            string userId = null;
            var identity = context.User.Identity;
            if (identity == null)
            {
                return null;
            }

            System.Security.Claims.ClaimsIdentity claimsIdentity = identity as System.Security.Claims.ClaimsIdentity;

            if (claimsIdentity != null)
            {
                System.Security.Claims.Claim first = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.Sid);
                userId = first.Value;
            }

            if (userId != null)
            {
                curUser = SrvUser.GetItem(long.Parse(userId));
            }
            return curUser;
        }

        public static UserModel GetItem(string account, string password)
        {
            UserDAO dao = new UserDAO();
            return dao.GetItem(account, password);
        }

        public static UserModel GetItem(long id)
        {
            UserDAO dao = new UserDAO();
            return dao.GetItem(id);
        }

        public static IEnumerable<UserModel> GetItems(Dictionary<string, object> parms, string orderBy = "")
        {
            UserDAO dao = new UserDAO();
            return dao.GetItems(parms, orderBy);
        }

        public static void SetItem(UserModel item, bool isNew, string[] updateExceptColumns, string identityColumnName = "ID")
        {
            UserDAO dao = new UserDAO();
            dao.SetItem(item, isNew, updateExceptColumns, identityColumnName);
        }
    }
}
