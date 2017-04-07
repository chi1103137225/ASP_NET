using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_NET.Areas.Order.Models
{
    public class Return_SearchOrder_Data
    {
        public string OrderId { get; set; }
        public string CustName { get; set; }
        public string EmpId { get; set; }
        public string ShipperId { get; set; }
        public string OrderDate { get; set; }
        public string ShippedDate { get; set; }
        public string RequiredDate { get; set; }
    }
}