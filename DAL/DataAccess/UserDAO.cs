using System;
using System.Collections.Generic;
using DAL.Models;

namespace DAL.DataAccess
{
    public class UserDAO : CommonDAO<UserModel>
    {
        public UserDAO()
        {
            SetTableName(UserModel.TableName);
        }

        public UserModel GetItem(string account, string password)
        {
            string sha256Pwd = Tools.GetSHA256(password);
            Dictionary<string, object> parms = new Dictionary<string, object>()
            {
                { nameof(UserModel.Account), account },
                { nameof(UserModel.Password), sha256Pwd }
            };

            return GetItem(parms);
        }
    }
}
