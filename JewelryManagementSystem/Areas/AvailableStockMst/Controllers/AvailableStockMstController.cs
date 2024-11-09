using JewelryManagementSystem.Areas.AvailableStockMst.Models;
using JewelryManagementSystem.Areas.ProductMst.Models;
using JewelryManagementSystem.DAL;
using JewelryManagementSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JewelryManagementSystem.Areas.AvailableStockMst.Controllers
{
	[Area("AvailableStockMst")]
	[Route("AvailableStockMst/[Controller]/[action]")]
	public class AvailableStockMstController : Controller
	{
		#region Service
		private readonly IProductMst _productService;
		#endregion

		#region Constructor
		public AvailableStockMstController(IProductMst productService)
		{
			_productService = productService;
		}
		#endregion


		public IActionResult Index()
		{
			return View("AvailableStockMst_Index");
		}

		[HttpPost]
		public IActionResult FillAvailableStock()
		{
			try
			{
				DataTable dtAvailableStockMst = _productService.GetAllAvailableStock();

				List<AvailableStockMstModel> availableStockList = new List<AvailableStockMstModel>();

				if (dtAvailableStockMst != null && dtAvailableStockMst.Rows.Count > 0)
				{
					availableStockList = dtAvailableStockMst
							.Rows.Cast<DataRow>() // Ensure you're working with DataRow objects
							.Select(row => new AvailableStockMstModel
							{
								ProductName = CCommon.NullOrEmptyToString(row["PRODUCTNAME"]),
								CategoryName = CCommon.NullOrEmptyToString(row["CATEGORYNAME"]),
								Description = CCommon.NullOrEmptyToString(row["DESCRIPTION"]),
								Price = CCommon.GetInt(row["PRICE"]),
								IncomingStockQuantity = CCommon.GetInt(row["INCOMING_QUANTITY"]),
								OutgoingStockQuantity = CCommon.GetInt(row["OUTGOING_QUANTITY"]),
								TotalQuantity = CCommon.GetInt(row["TOTAL_QUANTITY"])
							})
							.ToList();
				}

				return Json(new
				{
					AvailableStockMst = availableStockList
				});

			}
			catch (Exception ex)
			{
				return Json(new
				{
					success = false,
					message = "An error occurred while retrieving supplier.",
					error = ex.Message // Optionally include the error message for debugging
				});
			}

		}

	}
}
