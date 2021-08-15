using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAL.Models
{
    /// <summary>
    /// 票務議題
    /// </summary>
    public class TicketIssueModel
    {
        public const string TableName = "TicketIssue";

        public long ID { get; set; }

        [Display(Name = "種類")]
        /// <summary>
        /// 種類
        /// </summary>
        public TicketIssueType Type { get; set; }

        [Display(Name = "嚴重性")]
        /// <summary>
        /// 嚴重性
        /// </summary>
        public TicketIssueSeverity Severity { get; set; }

        [Display(Name = "優先權")]
        /// <summary>
        /// 優先權
        /// </summary>
        public TicketIssuePriority Priority { get; set; }

        [Display(Name = "狀態")]
        /// <summary>
        /// 狀態
        /// </summary>
        public TicketIssueState State { get; set; }

        [Display(Name = "摘要")]
        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }

        [Display(Name = "描述")]
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        [Display(Name = "被分派者")]
        /// <summary>
        /// 被指派者ID
        /// </summary>
        public long AssignUserID { get; set; }

        /// <summary>
        /// 建立者ID
        /// </summary>
        public long Creator { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改者ID
        /// </summary>
        public long? Modifier { get; set; }

        /// <summary>
        /// 修改時間
        /// </summary>
        public DateTime? ModifyTime { get; set; }
    }

    /// <summary>
    /// 票務議題類型
    /// </summary>
    public enum TicketIssueType
    {
        [Description("錯誤")]
        Bug = 1,
        [Description("功能請求")]
        FeatureRequest,
        [Description("測試")]
        TestCase
    }

    /// <summary>
    /// 票務議題嚴重性
    /// </summary>
    public enum TicketIssueSeverity
    {
        [Description("低")]
        Low = 1,
        [Description("中")]
        Medium,
        [Description("高")]
        High,
    }

    /// <summary>
    /// 票務議題優先權
    /// </summary>
    public enum TicketIssuePriority
    {
        [Description("低")]
        Low = 1,
        [Description("普通")]
        Normal,
        [Description("高")]
        High,
        [Description("緊急")]
        Urgent,
        [Description("需即時")]
        Immediate
    }

    /// <summary>
    /// 票務議題狀態
    /// </summary>
    public enum TicketIssueState
    {
        [Description("未解決")]
        Unresolved = 1,
        [Description("已解決")]
        Resolved
    }
}