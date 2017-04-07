using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_NET.Areas.Order.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order/Order
        [HttpGet()]
        public ActionResult Index(Models.Return_SearchOrder_Data SearchOrder_Data)
        {
            { 
                List<SelectListItem> Employee_select = new List<SelectListItem>();
                List<Models.Order> Employee_data = Models.SearchOrderService.GetEmployee_Id_Name();
                Employee_select.Add(new SelectListItem()
                {
                    Text = "",
                    Value = "",
                    Selected = true
                });
                foreach (var item in Employee_data)
                {
                    Employee_select.Add(new SelectListItem()
                    {
                        Text = item.EmpName,
                        Value = item.EmpId.ToString()
                    });
                }
                List<SelectListItem> Shipper_select = new List<SelectListItem>();
                List<Models.Order> Shipper_data = Models.SearchOrderService.GetShipper_Id_Name();
                Shipper_select.Add(new SelectListItem()
                {
                    Text = "",
                    Value = "",
                    Selected = true
                });
                foreach (var item in Shipper_data)
                {
                    Shipper_select.Add(new SelectListItem()
                    {
                        Text = item.ShipperName,
                        Value = item.ShipperId.ToString()
                    });
                }
                ViewBag.Employee_select = Employee_select;
                ViewBag.Shipper_select = Shipper_select;
            }

            {
                Models.SearchOrderService SearchOrder = new Models.SearchOrderService();
                ViewBag.SearchResult = SearchOrder.GetSearchOrder(SearchOrder_Data);
            }
            return View();
        }
    }
}