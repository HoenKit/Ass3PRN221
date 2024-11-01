$(() => {
    LoadBookData();

    var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build(); // Update to your Hub path
    connection.start();

    connection.on("BookCreated", function () {
        LoadBookData();
        location.href = '/Books'; // Redirect to the Books index page
    });

    function LoadBookData() {
        var tr = '';

        $.ajax({
            url: '/Books?handler=GetBooks',
            method: 'GET',
            success: (result) => {
                console.log(result)
                tr += `
                <table class="table table-striped">
                    <thead class="thead-dark">
                        <tr>
                            <th>Book Name</th>
                            <th>Price</th>
                            <th>Category</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>`;

                $.each(result, (k, v) => {
                    tr += `
                        <tr>
                            <td>${v.bookName}</td> 
                            <td>${v.price.toFixed(2)}$</td> <!-- Format price to two decimal places -->
                            <td>${v.category.categoryName}</td>
                            <td>
                                <a class="btn btn-primary btn-sm" href="../Books/Edit?Id=${v.id}">Edit</a>
                                <a class="btn btn-info btn-sm" href="../Books/Details?Id=${v.id}">Details</a>
                                <a class="btn btn-danger btn-sm" href="../Books/Delete?Id=${v.id}">Delete</a>
                            </td>
                        </tr>`;
                });

                tr += `</tbody>
                </table>`;

                $("#tableBody").html(tr); // Update the table body with new rows
            },
            error: (error) => {
                console.log(error);
            }
        });
    }
});