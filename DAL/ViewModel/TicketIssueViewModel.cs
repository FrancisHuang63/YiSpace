using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.ViewModel
{
    public class TicketIssueViewModel : TicketIssueModel
    {
        /// <summary>
        /// 種類
        /// </summary>
        public string Type_Desc { get { return Type.Description(); } }

        /// <summary>
        /// 嚴重性
        /// </summary>
        public string Severity_Desc { get { return Severity.Description(); } }

        /// <summary>
        /// 優先權
        /// </summary>
        public string Priority_Desc { get { return Priority.Description(); } }

        /// <summary>
        /// 狀態
        /// </summary>
        public string State_Desc { get { return State.Description(); } }

        public string CreateTime_Str { get { return CreateTime.ToString("yyyy/MM/dd"); } }

        public string ModifyTime_Str { get { return ModifyTime.ToString("yyyy/MM/dd"); } }

        /// <summary>
        /// 被指派者名稱
        /// </summary>
        public string AssignUserName { get; set; }

        /// <summary>
        /// 建立者名稱
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 更新者名稱
        /// </summary>
        public string ModifierName { get; set; }
    }
}
