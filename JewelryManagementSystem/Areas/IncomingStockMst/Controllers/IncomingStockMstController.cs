using JewelryManagementSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace JewelryManagementSystem.Areas.IncomingStockMst.Controllers
{
    [Area("IncomingStockMst")]
    [Route("IncomingStockMst/[Controller]/[action]")]
    public class IncomingStockMstController : Controller
    {
        private readonly IProductMst _productService;
        private readonly ICategoryMst _categoryService;
        private readonly ISupplierMst _supplierService;

        public IncomingStockMstController(IProductMst productService, ICategoryMst categoryService, ISupplierMst supplierService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
        }

        public IActionResult Index()
        {
            FillddlCategory();
            return View("IncomingStockMst_Index");
        }

        public IActionResult FillIncomingStock()
        {
            return View();
        }



        #region FillddlCategory
        public void FillddlCategory()
        {
            DataTable dtCategory = _categoryService.GetAllCategory(Guid.Empty);

            DataTable dtSupplier = _supplierService.GetAllSupplier(Guid.Empty);

            ViewBag.ddlCategory = dtCategory.AsEnumerable()
                                    .Select(row => new SelectListItem
                                    {
                                        Value = row["ID"].ToString(),
                                        Text = row["NAME"].ToString()
                                    })
                                    .ToList();
            ViewBag.ddlSupplier = dtSupplier.AsEnumerable()
                                    .Select(row => new SelectListItem
                                    {
                                        Value = row["ID"].ToString(),
                                        Text = row["NAME"].ToString()
                                    }).ToList();
        }
        #endregion


        #region ddlFillProduct
        [HttpPost]
        public IActionResult ddlFillProduct()
        {
            string p_sId = string.IsNullOrEmpty(Request.Form["p_sId"]) ? Guid.Empty.ToString() : Request.Form["p_sId"].ToString();

            Guid.TryParse(p_sId, out Guid p_uId);
             
            DataTable dtProduct = _productService.ddlFillProduct(p_uId);
            var result = dtProduct.AsEnumerable()
                    .Select(row => new SelectListItem
                    {
                        Value = row["ID"].ToString(),
                        Text = row["NAME"].ToString(),
                    }).ToList();

            return Json(result);
        }


        #endregion
    }
}
