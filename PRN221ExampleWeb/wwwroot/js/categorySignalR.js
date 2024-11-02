$(() => {
    LoadCategoryData();

    var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();
    connection.start();

    connection.on("CategoryCreated", function () {
        LoadCategoryData();
    });

    $('#searchForm').on('submit', function (e) {
        e.preventDefault();
        LoadCategoryData($('#Search').val());
    });

    function LoadCategoryData(searchQuery = '') {
        var tr = '';

        $.ajax({
            url: '/Categories?handler=GetCategories',
            method: 'GET',
            data: { Search: searchQuery }, 
            success: (result) => {
                console.log(result);
                tr += `
                <table class="table table-striped">
                    <thead class="thead-dark">
                        <tr>
                            <th>Category Name</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>`;

                $.each(result, (k, v) => {
                    tr += `
                        <tr>
                            <td>${v.categoryName}</td> 
                            <td>
                                <a class="btn btn-primary btn-sm" href="../Categories/Edit?Id=${v.id}">Edit</a>
                                <a class="btn btn-info btn-sm" href="../Categories/Details?Id=${v.id}">Details</a>
                                <a class="btn btn-danger btn-sm" href="../Categories/Delete?Id=${v.id}">Delete</a>
                            </td>
                        </tr>`;
                });

                tr += `</tbody>
                </table>`;

                $("#tableBody").html(tr); 
            },
            error: (error) => {
                console.log(error);
            }
        });
    }
});
