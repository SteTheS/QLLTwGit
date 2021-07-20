using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLlaptop.Models
{
    public class Query
    {
        private DBConnection db;
        public Query()
        {
            db = new DBConnection();
        }
        //public bool UpdateThuonghieu(ThuongHieu model)
        //{
        //    string sql = "Update Thuonghieu set Tenthuonghieu= N'"+model.Tenthuonghieu+"',Gioithieu=N'"+model.Gioithieu"' where Mathuonghieu" + model.Mathuonghieu;
        //    SqlConnection con = db.GetConnection();
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    con.Open();
        //    var kq = cmd.ExecuteNonQuery();
        //    con.Close();
        //    return kq > 0;
        //}
    }
}