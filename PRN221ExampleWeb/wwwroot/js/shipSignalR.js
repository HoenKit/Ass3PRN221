$(() => {
    LoadShipData();

    var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();
    connection.start();

    connection.on("ShipCreated", function () {
        LoadShipData();
    });

    $('#searchForm').on('submit', function (e) {
        e.preventDefault();
        LoadShipData($('#Search').val());
    });

    function LoadShipData(searchQuery = '') {
        var tr = '';

        $.ajax({
            url: '/Ships?handler=GetShips',
            method: 'GET',
            data: { Search: searchQuery }, 
            success: (result) => {
                console.log(result);
                tr += `
                <table class="table table-striped">
                    <thead class="thead-dark">
                        <tr>
                            <th>Book</th>
                            <th>Date Order</th>
                            <th>Date Ship</th>
                            <th>User Order</th>
                            <th>User Ship</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>`;

                $.each(result, (k, v) => {
                    tr += `
                        <tr>
                            <td>${v.book.bookName}</td>
                            <td>${v.dateOrder}</td> 
                            <td>${v.dateShip}</td> 
                            <td>${v.userOrder.userName}</td> 
                            <td>${v.userShip.userName}</td> 
                            <td>
                                <a class="btn btn-primary btn-sm" href="../Ships/Edit?Id=${v.id}">Edit</a>
                                <a class="btn btn-info btn-sm" href="../Ships/Details?Id=${v.id}">Details</a>
                                <a class="btn btn-danger btn-sm" href="../Ships/Delete?Id=${v.id}">Delete</a>
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
