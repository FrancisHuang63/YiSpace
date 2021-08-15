using DAL.DataAccess;
using DAL.Models;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YiSpace.Services
{
    public class SrvTicketIssue
    {
        public static IEnumerable<TicketIssueViewModel> GetTicketIssueViewItems(Dictionary<string, object> param)
        {
            TicketIssueDAO dao = new TicketIssueDAO();
            return dao.GetViewItems(param, $"ORDER BY {nameof(TicketIssueModel.CreateTime)} DESC");
        }

        public static TicketIssueModel GetItem(long id)
        {
            TicketIssueDAO dao = new TicketIssueDAO();
            return dao.GetItem(id);
        }

        public static int Delete(long id)
        {
            TicketIssueDAO dao = new TicketIssueDAO();
            return dao.Delete(id);
        }

        public static void SetItem(TicketIssueModel item, bool isNew, string[] updateExceptColumns, string identityColumnName = "ID")
        {
            TicketIssueDAO dao = new TicketIssueDAO();
            dao.SetItem(item, isNew, updateExceptColumns, identityColumnName);
        }
    }
}
