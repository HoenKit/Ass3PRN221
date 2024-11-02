$(() => {
    LoadUserData();

    var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();
    connection.start();

    connection.on("UserCreated", function () {
        LoadUserData();
    });

    $('#searchForm').on('submit', function (e) {
        e.preventDefault();
        LoadUserData($('#Search').val());
    });

    function LoadUserData(searchQuery = '') {
        var tr = '';

        $.ajax({
            url: '/Users?handler=GetUsers',
            method: 'GET',
            data: { Search: searchQuery }, 
            success: (result) => {
                console.log(result);
                tr += `
                <table class="table table-striped">
                    <thead class="thead-dark">
                        <tr>
                            <th>User Name</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>`;

                $.each(result, (k, v) => {
                    tr += `
                        <tr>
                            <td>${v.userName}</td> 
                            <td>
                                <a class="btn btn-primary btn-sm" href="../Users/Edit?Id=${v.id}">Edit</a>
                                <a class="btn btn-info btn-sm" href="../Users/Details?Id=${v.id}">Details</a>
                                <a class="btn btn-danger btn-sm" href="../Users/Delete?Id=${v.id}">Delete</a>
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
