﻿using System;
using System.Data.Common;
using System.Text;

namespace Trolley.Providers
{
    public class SqlServerProvider : BaseOrmProvider
    {
        public override DbConnection CreateConnection(string connString)
        {
            var factory = OrmProviderFactory.SqlServerFactory();
            var result = factory.CreateConnection();
            result.ConnectionString = connString;
            return result;
        }
        public override string GetQuotedColumnName(string columnName)
        {
            return "[" + columnName + "]";
        }
        public override string GetQuotedTableName(string tableName)
        {
            return "[" + tableName + "]";
        }
        public override string GetPagingExpression(string sql, int skip, int? limit, string orderBy = null)
        {
            if (String.IsNullOrEmpty(orderBy)) throw new ArgumentNullException("orderBy");
            StringBuilder buidler = new StringBuilder();
            buidler.Append(this.GetPagingSql(sql));
            if (!String.IsNullOrEmpty(orderBy)) buidler.Append(" " + orderBy);
            buidler.AppendFormat(" OFFSET {0} ROWS", skip);
            if (limit.HasValue) buidler.AppendFormat(" FETCH NEXT {0} ROWS ONLY", limit);
            return buidler.ToString();
        }
    }
}
