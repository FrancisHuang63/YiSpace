using DAL.Models;
using DAL.ViewModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DataAccess
{
    public class TicketIssueDAO : CommonDAO<TicketIssueModel>
    {
        public TicketIssueDAO()
        {
            SetTableName(TicketIssueModel.TableName);
        }

        public IEnumerable<TicketIssueViewModel> GetViewItems(Dictionary<string, object> param = null, string orderby = "")
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(Information.ConnectionStrings))
            {
                string whereClause = "";
                List<string> where = new List<string>();

                param = param ?? new Dictionary<string, object>();
                foreach (var item in param)
                {
                    var ienum = item.Value.GetType().GetInterface("IEnumerable`1");
                    if (ienum != null)
                    {
                        where.Add($" [{item.Key}] IN @{item.Key} ");
                    }
                    else
                    {
                        where.Add($" [{item.Key}] = @{item.Key} ");
                    }
                    
                }
                whereClause = string.Join(" AND ", where);

                string sql = $@"SELECT t.*
                                , (ISNULL((SELECT [Name] FROM [{UserModel.TableName}] WHERE ID = t.AssignUserID), '')) AS AssignUserName 
                                , (ISNULL((SELECT [Name] FROM [{UserModel.TableName}] WHERE ID = t.Creator), '')) AS CreatorName 
                                , (ISNULL((SELECT [Name] FROM [{UserModel.TableName}] WHERE ID = t.Modifier), '')) AS ModifierName 
                                FROM [{TicketIssueModel.TableName}] t " + (where.Count > 0 ? $" WHERE {whereClause} " : "" );
                if (!string.IsNullOrEmpty(orderby))
                    sql += orderby;
                return conn.Query<TicketIssueViewModel>(sql, param);
            }
        }
    }
}
