using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ASP_NET.Areas.Order.Models
{
    public class SearchOrderService
    {
        private static string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString.ToString();
        }
        /// <summary>
        /// 查詢負責員工(EmpId & EmpName)
        /// </summary>
        /// <returns>List<Models.Order></returns>
        public static List<Models.Order> GetEmployee_Id_Name()
        {
            Models.Order result = new Models.Order();
            DataTable dt = new DataTable();
            string sql = "SELECT EmployeeID As EmpId , LastName + ' ' + FirstName As EmpName FROM HR.Employees";

            using (SqlConnection conn = new SqlConnection(GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            return OrderServiceConfig.MapOrderDataToList(dt);

            /*List<Models.Order> result_Order = new List<Order>();

            foreach (DataRow row in dt.Rows)
            {
                result_Order.Add(new Order()
                {
                    EmpId = (int)row["EmpId"],
                    EmpName = row["EmpName"].ToString()
                });
            }
            return result_Order;*/
        }
        /// <summary>
        /// 查詢出貨公司(ShipperId & ShipperName)
        /// </summary>
        /// <returns>List<Models.Order></returns>
        public static List<Models.Order> GetShipper_Id_Name()
        {
            Models.Order result = new Models.Order();
            DataTable dt = new DataTable();
            string sql = "SELECT ShipperID As ShipperId , CompanyName As ShipperName FROM Sales.Shippers";

            using (SqlConnection conn = new SqlConnection(GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            return OrderServiceConfig.MapOrderDataToList(dt);

            /*List<Models.Order> result_Order = new List<Order>();

            foreach (DataRow row in dt.Rows)
            {
                result_Order.Add(new Order()
                {
                    ShipperId = (int)row["ShipperId"],
                    ShipperName = row["ShipperName"].ToString()
                });
            }
            return result_Order;*/
        }
        /// <summary>
        /// 查詢表單
        /// </summary>
        /// <param name="SearchOrder_Data"></param>
        /// <returns></returns>
        public List<Models.Order> GetSearchOrder(Models.Return_SearchOrder_Data SearchOrder_Data)
        {
            Models.Order result = new Models.Order();
            DataTable dt = new DataTable();
            string sql = Order_SQL(SearchOrder_Data);
            using (SqlConnection conn = new SqlConnection(GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }


            List<Models.Order> result_Order = new List<Order>();

            foreach (DataRow row in dt.Rows)
            {
                result_Order.Add(new Order()
                {
                    OrderId = (int)row["OrderId"],
                    CustName = row["CustName"].ToString(),
                    OrderDate = row["OrderDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["Orderdate"],
                    ShippedDate = row["ShippedDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["ShippedDate"]
                });
            }
            return result_Order;
        }
        /// <summary>
        /// 搜尋Order SQL處理
        /// </summary>
        /// <param name="SearchOrder_Data"></param>
        /// <returns></returns>
        public string Order_SQL(Models.Return_SearchOrder_Data SearchOrder_Data)
        {
            Boolean filter = false;
            string sql = @"SELECT salesorder.OrderID As OrderId , customers.CompanyName As CustName ,
                               CONVERT(DATETIME, salesorder.OrderDate, 111) As OrderDate, CONVERT(DATETIME, salesorder.ShippedDate, 111) As ShippedDate
                               FROM Sales.Orders As salesorder
                               INNER JOIN Sales.Customers As customers On salesorder.CustomerID = customers.CustomerID WHERE ";
            if (SearchOrder_Data.OrderId != "")
            {
                sql += "salesorder.OrderID = '" + SearchOrder_Data.OrderId + "' AND "; filter = true;
            }
            if (SearchOrder_Data.CustName != "")
            {
                sql += "customers.CompanyName LIKE '%" + SearchOrder_Data.CustName + "%' AND "; filter = true;
            }
            if (SearchOrder_Data.EmpId != "")
            {
                sql += "salesorder.EmployeeID = '" + SearchOrder_Data.EmpId + "' AND "; filter = true;
            }
            if (SearchOrder_Data.ShipperId != "")
            {
                sql += "salesorder.ShipperID = '" + SearchOrder_Data.ShipperId + "' AND "; filter = true;
            }
            if (SearchOrder_Data.OrderDate != "")
            {
                sql += "CONVERT(CHAR(10), salesorder.OrderDate, 120) = '" + SearchOrder_Data.OrderDate + "' AND "; filter = true;
            }
            if (SearchOrder_Data.ShippedDate != "")
            {
                sql += "CONVERT(CHAR(10), salesorder.ShippedDate, 120) = '" + SearchOrder_Data.ShippedDate + "' AND "; filter = true;
            }
            if (SearchOrder_Data.RequiredDate != "")
            {
                sql += "CONVERT(CHAR(10), salesorder.RequiredDate, 120) = '" + SearchOrder_Data.RequiredDate + "' AND "; filter = true;
            }

            if (filter)
            {
                sql = sql.Substring(0, sql.Length - 5);
            }
            else
            {
                sql = sql.Substring(0, sql.Length - 7);
            }
            return sql;
        }
    }
}