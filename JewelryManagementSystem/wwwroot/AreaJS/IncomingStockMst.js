var dtProduct = dtProduct;

$(document).ready(function () {
    
    FillIncomingStock();
});

function FillIncomingStock() {

    // Check if the DataTable is already initialized
    if ($.fn.DataTable.isDataTable('#IncomingStock_table')) {
        $('#IncomingStock_table').DataTable().clear().destroy();
    }

    let dataTable = $('#IncomingStock_table').DataTable({
        processing: false,
        serverSide: false,
        scrollX: true,
        fixedColumns: {
            right: 1
        },
        data: [], // Will be populated via AJAX
        columns: [
            {
                data: 'productName',
                className: 'text-center',
                render: function (data) {
                    return `
                        <div class="d-flex align-items-center">
                            <div class="ms-10">
                                <span class="fw-bold">${data}</span>
                            </div>
                        </div>
                    `;
                }
            },
            {
                data: 'categoryName',
                className: 'text-center',
                render: function (data) {
                    return `
                        <div class="d-flex align-items-center">
                            <div class="ms-12">
                                <span class="fw-bold">${data}</span>
                            </div>
                        </div>
                    `;
                }
            },
            {
                data: 'description',
                className: 'text-center',
                render: function (data) {
                    return `
                        <div class="d-flex align-items-center">
                            <div class="ms-10">
                                <span class="fw-bold">${data}</span>
                            </div>
                        </div>
                    `;
                }
            },
            {
                data: 'quantity',
                className: 'text-center',
                render: function (data) {
                    return `
                        <div class="d-flex align-items-center">
                            <div class="ms-15">
                                <span class="fw-bold">${data}</span>
                            </div>
                        </div>
                    `;
                }
            },
            {
                data: 'price',
                className: 'text-center',
                render: function (data) {
                    return `
                        <div class="d-flex align-items-center">
                            <div class="ms-15">
                                <span class="fw-bold">${data}</span>
                            </div>
                        </div>
                    `;
                }
            },
            {
                data: 'totalPrice',
                className: 'text-center',
                render: function (data) {
                    return `
                        <div class="d-flex align-items-center">
                            <div class="ms-10">
                                <span class="fw-bold">${data}</span>
                            </div>
                        </div>
                    `;
                }
            },
            {
                data: 'supplierName',
                className: 'text-center',
                render: function (data) {
                    return `
                        <div class="d-flex align-items-center">
                            <div class="ms-10">
                                <span class="fw-bold">${data}</span>
                            </div>
                        </div>
                    `;
                }
            },
            
            {
                data: 'receivedDate',
                className: 'text-center',
                render: function (data) {
                    const date = new Date(data);
                    return date.getFullYear() === 1 ?
                        '<span class="fw-bold">-</span>' :
                        `<span class="fw-bold">${formatDate(data)}</span>`;
                }
            },
            {
                data: 'id',
                className: 'text-center',
                render: function (data) {
                    return `<a href="#" class="btn btn-sm btn-icon btn-light btn-active-light-primary me-2" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete" onclick="btnDelete('${data}')">
                        <span class="svg-icon svg-icon-2 m-0">
                            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M5 9C5 8.44772 5.44772 8 6 8H18C18.5523 8 19 8.44772 19 9V18C19 19.6569 17.6569 21 16 21H8C6.34315 21 5 19.6569 5 18V9Z" fill="currentColor" />
                                <path opacity="0.5" d="M5 5C5 4.44772 5.44772 4 6 4H18C18.5523 4 19 4.44772 19 5V5C19 5.55228 18.5523 6 18 6H6C5.44772 6 5 5.55228 5 5V5Z" fill="currentColor" />
                                <path opacity="0.5" d="M9 4C9 3.44772 9.44772 3 10 3H14C14.5523 3 15 3.44772 15 4V4H9V4Z" fill="currentColor" />
                            </svg>
                        </span>
                    </a>
                    <a href="#" class="btn btn-sm btn-icon btn-light btn-active-light-primary me-3" data-bs-toggle="tooltip" data-bs-placement="top" title="Reply" onclick="btnEdit('${data}')">											
					<span class="svg-icon svg-icon-2 m-0">
						<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
							<path opacity="0.3" fill-rule="evenodd" clip-rule="evenodd" d="M2 4.63158C2 3.1782 3.1782 2 4.63158 2H13.47C14.0155 2 14.278 2.66919 13.8778 3.04006L12.4556 4.35821C11.9009 4.87228 11.1726 5.15789 10.4163 5.15789H7.1579C6.05333 5.15789 5.15789 6.05333 5.15789 7.1579V16.8421C5.15789 17.9467 6.05333 18.8421 7.1579 18.8421H16.8421C17.9467 18.8421 18.8421 17.9467 18.8421 16.8421V13.7518C18.8421 12.927 19.1817 12.1387 19.7809 11.572L20.9878 10.4308C21.3703 10.0691 22 10.3403 22 10.8668V19.3684C22 20.8218 20.8218 22 19.3684 22H4.63158C3.1782 22 2 20.8218 2 19.3684V4.63158Z" fill="currentColor" />
							<path d="M10.9256 11.1882C10.5351 10.7977 10.5351 10.1645 10.9256 9.77397L18.0669 2.6327C18.8479 1.85165 20.1143 1.85165 20.8953 2.6327L21.3665 3.10391C22.1476 3.88496 22.1476 5.15129 21.3665 5.93234L14.2252 13.0736C13.8347 13.4641 13.2016 13.4641 12.811 13.0736L10.9256 11.1882Z" fill="currentColor" />
							<path d="M8.82343 12.0064L8.08852 14.3348C7.8655 15.0414 8.46151 15.7366 9.19388 15.6242L11.8974 15.2092C12.4642 15.1222 12.6916 14.4278 12.2861 14.0223L9.98595 11.7221C9.61452 11.3507 8.98154 11.5055 8.82343 12.0064Z" fill="currentColor" />
						</svg>
					</span>
																		
					</a>`;
                }
            }
        ],
        order: [[1, 'desc']], // Sort by creation date by default
        pageLength: 10,
        lengthMenu: [[5, 10, 25, 50], [5, 10, 25, 50]]
    });

    formData = new FormData();
    formData.append('p_sId', '');

    $.ajax({
        type: "POST",
        url: "/IncomingStockMst/IncomingStockMst/FillIncomingStock",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            if (result != null) {
                
                dataTable.clear().rows.add(result.incomingStockMst).draw();
            }
        },
        error: function (req, status, message) {
            errorMessage(message, status)
        }
    });
    $('input[data-incomingstock-filter="search"]').on('keyup change clear', function () {
        const searchTerm = $(this).val();
        dataTable.search(searchTerm).draw();
    });
}
function btnNewIncomingStock() {
    $("#IncomingStockHeader").text("Add Incoming Stock");
    $("#ddlIncomingCategory").val('');
    $('#ddlIncomingCategory').removeClass('is-invalid');
    $("#ddlIncomingProduct").empty();
    $("#ddlIncomingProduct").append($("<option></option>").val("").html("--Select Product--"));
    $('#ddlIncomingProduct').removeClass('is-invalid');
    $("#txtIncomingDescription").val('');
    $('#txtIncomingDescription').removeClass('is-invalid');
    $("#txtIncomingPrice").val('');
    $('#txtIncomingPrice').removeClass('is-invalid');
    $("#ddlIncomingSupplier").val('');
    $('#ddlIncomingSupplier').removeClass('is-invalid');
    $("#txtIncomingQuantity").val('');
    $('#txtIncomingQuantity').removeClass('is-invalid');
    $("#txtIncomingReceivedDate").val('');
    $('#txtIncomingReceivedDate').removeClass('is-invalid');
    $('#btnIncomingStockSave').show();
    $('#btnIncomingStockUpdate').hide();
    $('#IncomingStock_modal').modal('show');
}
function btnIncomingStockSave(p_sMode) {

    if (!$('#ddlIncomingCategory').val()) {
        toastr.error('Category is required.', '', { timeOut: 5000 });
        $('#ddlIncomingCategory').addClass('is-invalid');
        $('#ddlIncomingCategory').focus();
        return;
    }
    else {
        $('#ddlIncomingCategory').removeClass('is-invalid');
    }

    if (!$('#ddlIncomingProduct').val()) {
        toastr.error('Product is required.', '', { timeOut: 5000 });
        $('#ddlIncomingProduct').addClass('is-invalid');
        $('#ddlIncomingProduct').focus();
        return;
    }
    else {
        $('#ddlIncomingProduct').removeClass('is-invalid');
    }


    if (!$('#ddlIncomingSupplier').val()) {
        toastr.error('Supplier is required.', '', { timeOut: 5000 });
        $('#ddlIncomingSupplier').addClass('is-invalid');
        $('#ddlIncomingSupplier').focus();
        return;
    }
    else {
        $('#ddlIncomingSupplier').removeClass('is-invalid');
    }

    if (!$('#txtIncomingQuantity').val()) {
        toastr.error('Quantity is required.', '', { timeOut: 5000 });
        $('#txtIncomingQuantity').addClass('is-invalid');
        $('#txtIncomingQuantity').focus();
        return;
    }
    else {
        $('#txtIncomingQuantity').removeClass('is-invalid');
    }
    if (!$('#txtIncomingReceivedDate').val()) {
        toastr.error('Received Date is required.', '', { timeOut: 5000 });
        $('#txtIncomingReceivedDate').addClass('is-invalid');
        $('#txtIncomingReceivedDate').focus();
        return;
    }
    else {
        $('#txtIncomingReceivedDate').removeClass('is-invalid');
    }

    formData = new FormData();
    formData.append('p_sId', p_sMode == "INSERT" ? "" : $('#IncomingStockID').val());
    formData.append('p_sProductId', $('#ddlIncomingProduct').val());
    formData.append('p_sSupplierId', $('#ddlIncomingSupplier').val());
    formData.append('p_iQuantity', $('#txtIncomingQuantity').val());
    formData.append('p_sReceivedDate', formatDate($('#txtIncomingReceivedDate').val()));
    formData.append('p_sMode', p_sMode);
    
    $.ajax({
        type: "POST",
        url: "/IncomingStockMst/IncomingStockMst/AddUpdateIncomingStock",
        data: formData,
        processData: false,
        contentType: false,
        success: function (respone) {

            if (respone != null && respone.success == true) {

                successMessage(respone.message, true);
                $('#IncomingStock_modal').modal('hide');
                FillIncomingStock();
            }
            else {
                errorMessage(respone.message, false);
            }
        },
        error: function (req, status, error) {

            errorMessage(error, status)
        }
    });
}
function btnEdit(id) {
    
    formData = new FormData();
    formData.append('p_sId', id);
    $.ajax({
        type: "POST",
        url: "/IncomingStockMst/IncomingStockMst/FillIncomingStock",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            if (result && result.incomingStockMst) {
                
                $('#IncomingStockID').val(id);
                $('#IncomingStockHeader').text('Edit Category');
                $('#ddlIncomingCategory').removeClass('is-invalid');
                $('#ddlIncomingSupplier').removeClass('is-invalid');
                $('#ddlIncomingProduct').removeClass('is-invalid');
                $('#txtIncomingQuantity').removeClass('is-invalid');
                $('#txtIncomingReceivedDate').removeClass('is-invalid');
                $('#btnIncomingStockSave').hide();
                $('#btnIncomingStockUpdate').show();
                EditIncomingStockData(result.incomingStockMst[0]);
            }
        },
        error: function (req, status, message) {
            errorMessage(message, status)
        }
    });
}
async function EditIncomingStockData(incomingStockMst) {
    $('#ddlIncomingCategory').val(incomingStockMst.categoryId);
    await ddlFillProduct();
    $('#ddlIncomingSupplier').val(incomingStockMst.supplierId);
    $('#ddlIncomingProduct').val(incomingStockMst.productId);
    $('#txtIncomingPrice').val(incomingStockMst.price);
    $('#txtIncomingDescription').val(incomingStockMst.description);
    $('#txtIncomingQuantity').val(incomingStockMst.quantity);
    $('#txtIncomingReceivedDate').val(incomingStockMst.receivedDate);
    $('#IncomingStock_modal').modal('show');
}
function btnDelete(id) {

    var formData = new FormData();
    formData.append('p_sId', id);

    Swal.fire({
        title: "Are you sure?",
        text: "You won't to Delete Record!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/IncomingStockMst/IncomingStockMst/DeleteIncomingStock",
                data: formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    if (response != null && response.success == true) {
                        successMessage(response.message, true);
                        FillIncomingStock();
                    } else {
                        Swal.fire({
                            title: "Error!",
                            text: "Failed to delete record.",
                            icon: "error"
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        title: "Error!",
                        text: "Failed to delete record.",
                        icon: "error"
                    });
                }
            });
        }
    });
}
function ddlFillProduct() {
    return new Promise((resolve, reject) => {
        
        if ($("#ddlIncomingCategory").val() != "") {
            $("#ddlIncomingProduct").empty();
            $("#ddlIncomingProduct").append($("<option></option>").val("").html("--Select Product--"));

            formData = new FormData();
            formData.append('p_sId', $("#ddlIncomingCategory").val());

            $.ajax({
                type: "POST",
                url: "/IncomingStockMst/IncomingStockMst/ddlFillProduct",
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    if (result != null) {
                        $.each(result, function (key, value) {
                            $("#ddlIncomingProduct").append($("<option></option>").val(value.value).html(value.text));
                        });
                        resolve();  // Resolve the Promise after dropdown is populated
                    } else {
                        reject("No data returned");  // Reject in case of no data
                    }
                },
                error: function (req, status, message) {
                    reject("Error: " + message);  // Reject in case of an error
                }
            });
        } else {
            $("#ddlIncomingProduct").empty();
            $("#ddlIncomingProduct").append($("<option></option>").val("").html("--Select Product--"));
            $("#txtIncomingDescription").val('');
            $("#txtIncomingPrice").val('');
            resolve();  // Resolve immediately if there's no category selected
        }
    });
}
function FillDescription() {
    
    var productId = $("#ddlIncomingProduct").val();
    var Product = dtProduct.find(function (product) {
        return product.ID === productId;
    });

    if (Product) {
        $("#txtIncomingDescription").val(Product.DESCRIPTION);
        $("#txtIncomingPrice").val(Product.PRICE);

    } else {
        $("#txtIncomingDescription").val('');
        $("#txtIncomingPrice").val('');
        
    }
}
