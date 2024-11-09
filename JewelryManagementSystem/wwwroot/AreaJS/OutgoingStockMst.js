var dtProduct = dtProduct;

$(document).ready(function () {

    FillOutgoingStock();
});

function FillOutgoingStock() {

    // Check if the DataTable is already initialized
    if ($.fn.DataTable.isDataTable('#OutgoingStock_table')) {
        $('#OutgoingStock_table').DataTable().clear().destroy();
    }

    let dataTable = $('#OutgoingStock_table').DataTable({
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
                            <div class="ms-10">
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
                data: 'customerName',
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
                data: 'soldDate',
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
        url: "/OutgoingStockMst/OutgoingStockMst/FillOutgoingStock",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            if (result != null) {

                dataTable.clear().rows.add(result.outgoingStockMst).draw();
            }
        },
        error: function (req, status, message) {
            errorMessage(message, status)
        }
    });
    $('input[data-outgoingstock-filter="search"]').on('keyup change clear', function () {
        const searchTerm = $(this).val();
        dataTable.search(searchTerm).draw();
    });
}
function btnNewOutgoingStock() {
    $("#OutgoingStockHeader").text("Add Outgoing Stock");
    $("#ddlOutgoingCategory").val('');
    $('#ddlOutgoingCategory').removeClass('is-invalid');
    $("#ddlOutgoingProduct").empty();
    $("#ddlOutgoingProduct").append($("<option></option>").val("").html("--Select Product--"));
    $('#ddlOutgoingProduct').removeClass('is-invalid');
    $("#txtOutgoingDescription").val('');
    $('#txtOutgoingDescription').removeClass('is-invalid');
    $("#txtOutgoingPrice").val('');
    $('#txtOutgoingPrice').removeClass('is-invalid');
    $("#ddlOutgoingCustomer").val('');
    $('#ddlOutgoingCustomer').removeClass('is-invalid');
    $("#txtOutgoingQuantity").val('');
    $('#txtOutgoingQuantity').removeClass('is-invalid');
    $("#txtOutgoingSoldDate").val('');
    $('#txtOutgoingSoldDate').removeClass('is-invalid');
    $('#btnOutgoingStockSave').show();
    $('#btnOutgoingStockUpdate').hide();
    $('#OutgoingStock_modal').modal('show');
}
function btnOutgoingStockSave(p_sMode) {

    if (!$('#ddlOutgoingCategory').val()) {
        toastr.error('Category is required.', '', { timeOut: 5000 });
        $('#ddlOutgoingCategory').addClass('is-invalid');
        $('#ddlOutgoingCategory').focus();
        return;
    }
    else {
        $('#ddlOutgoingCategory').removeClass('is-invalid');
    }

    if (!$('#ddlOutgoingCustomer').val()) {
        toastr.error('Customer is required.', '', { timeOut: 5000 });
        $('#ddlOutgoingCustomer').addClass('is-invalid');
        $('#ddlOutgoingCustomer').focus();
        return;
    }
    else {
        $('#ddlOutgoingCustomer').removeClass('is-invalid');
    }

    if (!$('#ddlOutgoingProduct').val()) {
        toastr.error('Product is required.', '', { timeOut: 5000 });
        $('#ddlOutgoingProduct').addClass('is-invalid');
        $('#ddlOutgoingProduct').focus();
        return;
    }
    else {
        $('#ddlOutgoingProduct').removeClass('is-invalid');
    }

    if (!$('#txtOutgoingQuantity').val()) {
        toastr.error('Quantity is required.', '', { timeOut: 5000 });
        $('#txtOutgoingQuantity').addClass('is-invalid');
        $('#txtOutgoingQuantity').focus();
        return;
    }
    else {
        $('#txtOutgoingQuantity').removeClass('is-invalid');
    }

    if (!$('#txtOutgoingSoldDate').val()) {
        toastr.error('Sold Date is required.', '', { timeOut: 5000 });
        $('#txtOutgoingSoldDate').addClass('is-invalid');
        $('#txtOutgoingSoldDate').focus();
        return;
    }
    else {
        $('#txtIncomingReceivedDate').removeClass('is-invalid');
    }

    formData = new FormData();
    formData.append('p_sId', p_sMode == "INSERT" ? "" : $('#OutgoingStockID').val());
    formData.append('p_sProductId', $('#ddlOutgoingProduct').val());
    formData.append('p_sCustomerId', $('#ddlOutgoingCustomer').val());
    formData.append('p_iQuantity', $('#txtOutgoingQuantity').val());
    formData.append('p_sSoldDate', formatDate($('#txtOutgoingSoldDate').val()));
    formData.append('p_sMode', p_sMode);

    $.ajax({
        type: "POST",
        url: "/OutgoingStockMst/OutgoingStockMst/AddUpdateOutgoingStock",
        data: formData,
        processData: false,
        contentType: false,
        success: function (respone) {

            if (respone != null && respone.success == true) {

                successMessage(respone.message, true);
                $('#OutgoingStock_modal').modal('hide');
                FillOutgoingStock();
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
        url: "/OutgoingStockMst/OutgoingStockMst/FillOutgoingStock",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            if (result && result.outgoingStockMst) {
                debugger;
                $('#OutgoingStockID').val(id);
                $('#OutgoingStockHeader').text('Edit Outgoing Stock');
                $('#ddlOutgoingCategory').removeClass('is-invalid');
                $('#ddlOutgoingCustomer').removeClass('is-invalid');
                $('#ddlOutgoingProduct').removeClass('is-invalid');
                $('#txtOutgoingQuantity').removeClass('is-invalid');
                $('#txtOutgoingSoldDate').removeClass('is-invalid');
                $('#btnOutgoingStockSave').hide();
                $('#btnOutgoingStockUpdate').show();
                EditOutgoingStockData(result.outgoingStockMst[0]);
            }
        },
        error: function (req, status, message) {
            errorMessage(message, status)
        }
    });
}
async function EditOutgoingStockData(outgoingStockMst) {
    $('#ddlOutgoingCategory').val(outgoingStockMst.categoryId);
    await ddlFillOutgoingProduct();
    $('#ddlOutgoingCustomer').val(outgoingStockMst.customerId);
    $('#ddlOutgoingProduct').val(outgoingStockMst.productId);
    $('#txtOutgoingPrice').val(outgoingStockMst.price);
    $('#txtOutgoingDescription').val(outgoingStockMst.description);
    $('#txtOutgoingQuantity').val(outgoingStockMst.quantity);
    $('#txtOutgoingSoldDate').val(outgoingStockMst.soldDate);
    $('#OutgoingStock_modal').modal('show');
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
                url: "/OutgoingStockMst/OutgoingStockMst/DeleteOutgoingStock",
                data: formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    if (response != null && response.success == true) {
                        successMessage(response.message, true);
                        FillOutgoingStock();
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
function ddlFillOutgoingProduct() {
    return new Promise((resolve, reject) => {
        debugger;
        if ($("#ddlOutgoingCategory").val() != "") {
            $("#ddlOutgoingProduct").empty();
            $("#ddlOutgoingProduct").append($("<option></option>").val("").html("--Select Product--"));

            formData = new FormData();
            formData.append('p_sId', $("#ddlOutgoingCategory").val());

            $.ajax({
                type: "POST",
                url: "/OutgoingStockMst/OutgoingStockMst/ddlFillProduct",
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    if (result != null) {
                        debugger;
                        $.each(result, function (key, value) {
                            $("#ddlOutgoingProduct").append($("<option></option>").val(value.value).html(value.text));
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
            $("#ddlOutgoingProduct").empty();
            $("#ddlOutgoingProduct").append($("<option></option>").val("").html("--Select Product--"));
            $("#txtOutgoingDescription").val('');
            $("#txtOutgoingPrice").val('');
            resolve();  // Resolve immediately if there's no category selected
        }
    });
}
function FillOutgoingStockDescription() {

    var productId = $("#ddlOutgoingProduct").val();
    var Product = dtProduct.find(function (product) {
        return product.ID === productId;
    });

    if (Product) {
        $("#txtOutgoingDescription").val(Product.DESCRIPTION);
        $("#txtOutgoingPrice").val(Product.PRICE);

    } else {
        $("#txtOutgoingDescription").val('');
        $("#txtOutgoingPrice").val('');

    }
}
