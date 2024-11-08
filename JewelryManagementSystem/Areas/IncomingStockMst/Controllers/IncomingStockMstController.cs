﻿using JewelryManagementSystem.Areas.IncomingStockMst.Models;
using JewelryManagementSystem.Areas.ProductMst.Models;
using JewelryManagementSystem.DAL;
using JewelryManagementSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Data;

namespace JewelryManagementSystem.Areas.IncomingStockMst.Controllers
{
    [Area("IncomingStockMst")]
    [Route("IncomingStockMst/[Controller]/[action]")]
    public class IncomingStockMstController : Controller
    {
        #region Service
        private readonly IIncomingStockMst _incomingStockService;
        private readonly IProductMst _productService;
        private readonly ICategoryMst _categoryService;
        private readonly ISupplierMst _supplierService;
        #endregion

        #region Constructor
        public IncomingStockMstController(IProductMst productService, ICategoryMst categoryService, ISupplierMst supplierService, IIncomingStockMst incomingStockService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _incomingStockService = incomingStockService;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            FillDropDown();
            return View("IncomingStockMst_Index");
        }
        #endregion

        #region Fill Incoming Stock
        public IActionResult FillIncomingStock()
        {
            string p_sId = string.IsNullOrEmpty(Request.Form["p_sId"]) ? Guid.Empty.ToString() : Request.Form["p_sId"].ToString();
            try
            {
                Guid.TryParse(p_sId, out Guid p_uId);
                DataTable dtIncomingStock = _incomingStockService.GetAllIncomingStock(p_uId);


                List<IncomingStockMstModel> incomingStockList = new List<IncomingStockMstModel>();

                if (dtIncomingStock != null && dtIncomingStock.Rows.Count > 0)
                {
                    incomingStockList = dtIncomingStock
                            .Rows.Cast<DataRow>() // Ensure you're working with DataRow objects
                            .Select(row => new IncomingStockMstModel
                            {
                                ID = Guid.TryParse(row["ID"].ToString(), out Guid guid) ? guid : Guid.Empty,
                                ProductId = Guid.TryParse(row["PRODUCTID"].ToString(), out Guid productid) ? productid : Guid.Empty,
                                ProductName = CCommon.NullOrEmptyToString(row["PRODUCTNAME"]),
                                SupplierId = Guid.TryParse(row["SUPPLIERID"].ToString(), out Guid supplierid) ? supplierid : Guid.Empty,
                                SupplierName = CCommon.NullOrEmptyToString(row["SUPPLIERNAME"]),
                                CategoryId = Guid.TryParse(row["CATEGORYID"].ToString(), out Guid categoryid) ? categoryid : Guid.Empty,
                                CategoryName = CCommon.NullOrEmptyToString(row["CATEGORYNAME"]),
                                Quantity = CCommon.GetInt(row["QUANTITY"]),
                                Description = CCommon.NullOrEmptyToString(row["DESCRIPTION"]),
                                ReceivedDate = CCommon.NullOrDefaultDateTime(row["RECEIVEDDATE"]),
                                CreationDate = CCommon.NullOrDefaultDateTime(row["CREATIONDATE"]),
                                ModificationDate = CCommon.NullOrDefaultDateTime(row["MODIFICATIONDATE"])
                                
                            })
                            .ToList();
                }

                return Json(new
                {
                    IncomingStockMst = incomingStockList
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
        #endregion

        #region Fill Category and Supplier DropDown
        public void FillDropDown()
        {
            try
            {
                //DataTable dtCategory = _categoryService.GetAllCategory(Guid.Empty);
                //DataTable dtSupplier = _supplierService.GetAllSupplier(Guid.Empty);
                //DataTable dtProduct = _productService.ddlFillProduct(Guid.Empty);
                //dtProduct
                DataSet ds = _incomingStockService.ddlFillProduct(Guid.Empty);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables.Contains("dtCategory"))
                    {
                        ViewBag.ddlCategory = ds.Tables["dtCategory"].AsEnumerable()
                                           .Select(row => new SelectListItem
                                           {
                                               Value = row["ID"].ToString(),
                                               Text = row["NAME"].ToString()
                                           })
                                           .ToList();
                    }
                    else
                    {
                        ViewBag.ddlCategory = new List<SelectListItem>();
                    }

                    if (ds.Tables.Contains("dtSupplier"))
                    {
                        ViewBag.ddlSupplier = ds.Tables["dtSupplier"].AsEnumerable()
                                            .Select(row => new SelectListItem
                                            {
                                                Value = row["ID"].ToString(),
                                                Text = row["NAME"].ToString()
                                            })
                                            .ToList();
                    }
                    else
                    {
                        ViewBag.ddlSupplier = new List<SelectListItem>();
                    }

                    if (ds.Tables.Contains("dtProduct"))
                    {
                        ViewBag.dtProduct = ds.Tables["dtProduct"];
                    }
                }

               

                //if (dtCategory != null && dtCategory.Rows.Count > 0)
                //{
                //    ViewBag.ddlCategory = dtCategory.AsEnumerable()
                //                            .Select(row => new SelectListItem
                //                            {
                //                                Value = row["ID"].ToString(),
                //                                Text = row["NAME"].ToString()
                //                            })
                //                            .ToList();
                //}
                //else
                //{
                //    ViewBag.ddlCategory = new List<SelectListItem>();
                //}

                //if (dtSupplier != null && dtSupplier.Rows.Count > 0)
                //{
                //    ViewBag.ddlSupplier = dtSupplier.AsEnumerable()
                //                            .Select(row => new SelectListItem
                //                            {
                //                                Value = row["ID"].ToString(),
                //                                Text = row["NAME"].ToString()
                //                            })
                //                            .ToList();
                //}
                //else
                //{
                //    ViewBag.ddlSupplier = new List<SelectListItem>();
                //}
            }
            catch (Exception)
            {
                ViewBag.ddlCategory = new List<SelectListItem>();
                ViewBag.ddlSupplier = new List<SelectListItem>();
            }
        }
        #endregion

        #region Fill Product DropDown
        [HttpPost]
        public IActionResult ddlFillProduct()
        {
            string p_sId = string.IsNullOrEmpty(Request.Form["p_sId"]) ? Guid.Empty.ToString() : Request.Form["p_sId"].ToString();
            Guid.TryParse(p_sId, out Guid p_uId);

            try
            {
                DataSet ds = _incomingStockService.ddlFillProduct(p_uId);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables.Contains("dtProduct"))
                {
                    var result = ds.Tables["dtProduct"].AsEnumerable()
                                .Select(row => new SelectListItem
                                {
                                    Value = row["ID"].ToString(),
                                    Text = row["NAME"].ToString(),
                                }).ToList();

                    return Json(result);
                }
                return Json(new List<SelectListItem>());

            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while fetching products." });
            }
        }


        #endregion
    }
}
