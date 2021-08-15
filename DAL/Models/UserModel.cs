using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    /// <summary>
    /// 登入使用者
    /// </summary>
    public class UserModel
    {
        public const string TableName = "User";

        public long ID { get; set; }

        [Display(Name = "帳號")]
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }

        [Display(Name = "密碼")]
        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }

        [Display(Name = "使用者名稱")]
        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string Name { get; set; }

        [Display(Name = "使用者層級")]
        /// <summary>
        /// 使用者層級
        /// </summary>
        public UserLevel Level { get; set; }
    }

    /// <summary>
    /// 使用者身分層級
    /// </summary>
    public enum UserLevel
    {
        QA = 1,
        RD = 2,
        PM = 3,
        Administrator = 99,
    }
}
