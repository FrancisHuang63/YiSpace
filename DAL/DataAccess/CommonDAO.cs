using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.Linq;
using Dapper;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DataAccess
{
    public class CommonDAO<TModel>
    {
        protected string _tableName;

        public void SetTableName(string tableName)
        {
            _tableName = tableName;
        }

        public TModel GetItem(object value, string columnName = "ID")
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(Information.ConnectionStrings))
            {
                string sql = $"SELECT * FROM [{_tableName}] WHERE [{columnName}] = @Param ";
                return conn.Query<TModel>(sql, new { Param = value }).FirstOrDefault();
            }
        }

        public TModel GetItem(Dictionary<string, object> param)
        {
            if (param.Count() == 0)
                throw new Exception("參數不得為空");

            string whereClause = "";
            List<string> where = new List<string>();

            foreach (var item in param)
            {
                where.Add($" [{item.Key}] = @{item.Key} ");
            }
            whereClause = string.Join(" AND ", where);

            using (var conn = new System.Data.SqlClient.SqlConnection(Information.ConnectionStrings))
            {
                string sql = $"SELECT * FROM [{_tableName}] WHERE {whereClause} ";
                return conn.Query<TModel>(sql, param).FirstOrDefault();
            }
        }

        public IEnumerable<TModel> GetItems(Dictionary<string, object> param, string orderby = "")
        {
            if (param.Count == 0)
                throw new Exception("參數不得為空");

            string whereClause = "";
            List<string> where = new List<string>();

            foreach (var item in param)
            {
                where.Add($" [{item.Key}] = @{item.Key} ");
            }
            whereClause = string.Join(" AND ", where);

            using (var conn = new System.Data.SqlClient.SqlConnection(Information.ConnectionStrings))
            {
                string sql = $"SELECT * FROM [{_tableName}] WHERE {whereClause} ";
                if (!string.IsNullOrEmpty(orderby))
                    sql += orderby;
                return conn.Query<TModel>(sql, param);
            }
        }

        public IEnumerable<TModel> GetAll(string orderby = "")
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("1", "1");
            return GetItems(param, orderby);
        }

        /// <summary>
        /// 將項目新增至資料庫
        /// </summary>
        /// <param name="item">新增物件</param>
        /// <param name="identityColumnName">識別欄位名稱</param>
        /// <param name="isWithIdentity">新增紀錄時是否寫入識別欄位</param>
        public void CreateItem(TModel item, string identityColumnName = "ID", bool isWithIdentity = true)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(Information.ConnectionStrings))
            {
                IEnumerable<PropertyInfo> info = typeof(TModel)
                    .GetProperties()
                    .Where(m => m.PropertyType.Name != typeof(IEnumerable<>).Name &&
                        m.PropertyType.Name != typeof(List<>).Name &&
                        m.GetCustomAttributes(typeof(NotMappedAttribute), false).Length == 0 && // 不加入被設為 NotMapped 的欄位 
                        (isWithIdentity ? true : m.Name != identityColumnName) &&// 如果不加入 identity 的話就只選擇名稱和 Identity Column Name 不同的欄位
                        m.GetValue(item) != null); // 不加入沒有被寫入值的欄位

                string sql = $@" 
                        INSERT INTO [{_tableName}]({string.Join(", ", info.Select(m => m.Name))}) VALUES({string.Join(", ", info.Select(m => $"@{m.Name}"))}) 
";

                conn.Execute(sql, item);
            }
        }

        public void UpdateItemExcept(TModel item, string[] updateExceptColumns, string identityColumnName = "ID")
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(Information.ConnectionStrings))
            {
                IEnumerable<string> updateInaertColumns = typeof(TModel).GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof(NotMappedAttribute), false).Length == 0 && !updateExceptColumns.Contains(p.Name))
                    .Select(m => $"[{m.Name}] = @{m.Name} ")
                                                                              ;

                string sql = $" UPDATE [{_tableName}] SET {string.Join(", ", updateInaertColumns)} WHERE [{identityColumnName}] = @{identityColumnName} ";
                conn.Execute(sql, item);
            }
        }

        public void UpdateItem(TModel item, string[] updateColumns, string identityColumnName = "ID")
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(Information.ConnectionStrings))
            {
                IEnumerable<string> updateInaertColumns = typeof(TModel).GetProperties()
                    .Where(m => updateColumns.Contains(m.Name)
                            && m.GetCustomAttributes(typeof(NotMappedAttribute), false).Length == 0).Select(m => $"[{m.Name}] = @{m.Name} ");

                string sql = $" UPDATE [{_tableName}] SET {string.Join(", ", updateInaertColumns)} WHERE [{identityColumnName}] = @{identityColumnName} ";

                conn.Execute(sql, item);
            }
        }
        public void UpdateItem(TModel item, string[] updateColumns, string[] identityColumnName)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(Information.ConnectionStrings))
            {
                string whereCond = "";
                IEnumerable<string> updateInaertColumns = typeof(TModel).GetProperties()
                    .Where(m => updateColumns.Contains(m.Name)
                            && m.GetCustomAttributes(typeof(NotMappedAttribute), false).Length == 0).Select(m => $"[{m.Name}] = @{m.Name} ");
                for (int i = 0; i < identityColumnName.Length; i++)
                {
                    whereCond += (string.IsNullOrEmpty(whereCond) ? "" : " AND ") + string.Format(" [{0}]=@{0} ", identityColumnName[i]);
                }
                string sql = $" UPDATE [{_tableName}] SET {string.Join(", ", updateInaertColumns)} WHERE {whereCond} ";

                conn.Execute(sql, item);
            }
        }

        public void SetItem(TModel item, bool isNew, string[] updateExceptColumns, string identityColumnName = "ID")
        {
            if (isNew)
                CreateItem(item);
            else
                UpdateItemExcept(item, updateExceptColumns, identityColumnName);
        }

        public int Delete(IEnumerable<long> ids, string columnName = "ID")
        {
            if (ids == null || ids.Count() == 0)
                return 0;

            using (var conn = new System.Data.SqlClient.SqlConnection(Information.ConnectionStrings))
            {
                string sql = $"DELETE [{_tableName}] WHERE {columnName} IN ({string.Join(",", ids)})";
                return conn.Execute(sql);
            }
        }

        public int Delete(object value, string columnName = "ID")
        {
            try
            {
                using (var conn = new System.Data.SqlClient.SqlConnection(Information.ConnectionStrings))
                {
                    string sql = $"DELETE [{_tableName}] WHERE {columnName} = @Value";
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("Value", value);

                    return conn.Execute(sql, param);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
