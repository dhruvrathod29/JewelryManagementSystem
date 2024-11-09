$(document).ready(function () {
    debugger;
    FillAvailableStock();
});

function FillAvailableStock() {

    // Check if the DataTable is already initialized
    if ($.fn.DataTable.isDataTable('#AvailableStock_table')) {
        $('#AvailableStock_table').DataTable().clear().destroy();
    }

    let dataTable = $('#AvailableStock_table').DataTable({
        processing: false,
        serverSide: false,
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
                data: 'incomingStockQuantity',
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
                data: 'outgoingStockQuantity',
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
                data: 'totalQuantity',
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
            
            
        ],
        order: [[1, 'desc']], // Sort by creation date by default
        pageLength: 10,
        lengthMenu: [[5, 10, 25, 50], [5, 10, 25, 50]]
    });

    formData = new FormData();
    formData.append('p_sId', '');

    $.ajax({
        type: "POST",
        url: "/AvailableStockMst/AvailableStockMst/FillAvailableStock",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            if (result != null) {
                debugger;
                dataTable.clear().rows.add(result.availableStockMst).draw();
            }
        },
        error: function (req, status, message) {
            errorMessage(message, status)
        }
    });
    $('input[data-availablestock-filter="search"]').on('keyup change clear', function () {
        const searchTerm = $(this).val();
        dataTable.search(searchTerm).draw();
    });
}