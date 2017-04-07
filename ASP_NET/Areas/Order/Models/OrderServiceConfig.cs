using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ASP_NET.Areas.Order.Models
{
    public class OrderServiceConfig
    {
        public static List<Models.Order> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Order> result = new List<Order>();

            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Order()
                {
                    OrderId = row["OrderId"] == DBNull.Value ? 0 : (int)row["OrderId"],
                    CustId = row["CustId"] == DBNull.Value ? string.Empty : row["CustId"].ToString(),
                    EmpId = row["EmpId"] == DBNull.Value ? 0 : (int)row["EmpId"],
                    OrderDate = row["OrderDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["Orderdate"],
                    RequiredDate = row["RequiredDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["RequiredDate"],
                    ShippedDate = row["ShippedDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["ShippedDate"],
                    CustName = row["CustName"] == DBNull.Value ? string.Empty : row["CustName"].ToString(),
                    EmpName = row["EmpName"] == DBNull.Value ? string.Empty : row["EmpName"].ToString(),
                    ShipAddress = row["ShipAddress"] == DBNull.Value ? string.Empty : row["ShipAddress"].ToString(),
                    ShipCity = row["ShipCity"] == DBNull.Value ? string.Empty : row["ShipCity"].ToString(),
                    ShipCountry = row["ShipCountry"] == DBNull.Value ? string.Empty : row["ShipCountry"].ToString(),
                    ShipName = row["ShipName"] == DBNull.Value ? string.Empty : row["ShipName"].ToString(),
                    ShipperId = row["ShipperId"] == DBNull.Value ? 0 : (int)row["ShipperId"],
                    Freight = row["ShipperId"] == DBNull.Value ? 0 : (decimal)row["Freight"],
                    ShipperName = row["ShipperName"] == DBNull.Value ? string.Empty : row["ShipperName"].ToString(),
                    ShipPostalCode = row["ShipPostalCode"] == DBNull.Value ? string.Empty : row["ShipPostalCode"].ToString(),
                    ShipRegion = row["ShipRegion"] == DBNull.Value ? string.Empty : row["ShipRegion"].ToString()
                    
                });
            }
            return result;
        }
    }
}