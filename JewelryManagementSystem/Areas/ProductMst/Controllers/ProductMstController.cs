using JewelryManagementSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JewelryManagementSystem.Areas.ProductMst.Controllers
{
    public class ProductMstController : Controller
    {
        private readonly IProductMst _productService;

        public ProductMstController(IProductMst productService)
        {
            _productService = productService;   
        }

        public IActionResult Index()
        {
            return View("ProductMst_Index");
        }

        public IActionResult FillProduct()
        {
            try
            {
                DataTable dt = _productService.FillProduct(Guid.Empty);
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }
    }
}
