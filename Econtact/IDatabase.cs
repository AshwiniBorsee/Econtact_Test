using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econtact
{
    public interface IDatabase
    {
        DataTable ExecuteQuery(string sql, SqlParameter[] parameters);
        int ExecuteNonQuery(string sql, SqlParameter[] parameters);
    }
}
