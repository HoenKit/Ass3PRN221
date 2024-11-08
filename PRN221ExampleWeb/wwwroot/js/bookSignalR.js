﻿$(() => {
    LoadBookData();

    var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();
    connection.start();

    connection.on("BookCreated", function () {
        LoadBookData();
    });

    $('#searchForm').on('submit', function (e) {
        e.preventDefault();
        LoadBookData($('#Search').val());
    });

    function LoadBookData(searchQuery = '') {
        var tr = '';

        $.ajax({
            url: '/Books?handler=GetBooks',
            method: 'GET',
            data: { Search: searchQuery }, 
            success: (result) => {
                console.log(result);
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
                            <td>${v.price}$</td> 
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

                $("#tableBody").html(tr); 
            },
            error: (error) => {
                console.log(error);
            }
        });
    }
});
