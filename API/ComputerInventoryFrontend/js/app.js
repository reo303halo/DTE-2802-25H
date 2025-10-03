$(document).ready(function() {
    var baseUrl =  "https://localhost:7017/api/"

    $('#loginForm').on('submit', function(event) {
        event.preventDefault();

        var username = $('#username').val();
        var password = $('#password').val();

        $.ajax({
            url: `${baseUrl}Auth/Login`,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ Username: username, Password: password }),
            success: function(response) {
                if (response.isSuccess) {

                    // Store JWT in Local Storage
                    localStorage.setItem('token', response.token)

                    $('#message').text('Login Successful!');
                    $('#loginForm').hide();
                    fetchComputers();
                } else {
                    $('#message').text('Error: ' + response.message);
                }
            },
            error: function(xhr, status, error) {
                $('#message').text('Error: ' + error);
            }
        });
    }); 

    $('#logoutButton').on('click', function() {
        localStorage.removeItem('token')
        $('#message').text('Logged out successfully.');
        $('#loginForm').show();
        $('#data').empty();
    });

    function fetchComputers() {
        var token = localStorage.getItem('token'); // Be aware of XSS

        $.ajax({
            url: `${baseUrl}Computer`,
            type: 'GET',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            success: function(response) {
                displayData(response)
            },
            error: function(xhr, status, error) {
                $('#message').text('Error (fetched data): ' + error);
            }
        });
    }

    function displayData(data) {
        var dataDiv = $('#data');
        dataDiv.empty(); // Clear previous data

        if (Array.isArray(data)) {
            var list = $('<ul></ul>');
            data.forEach(function(item) {
                var listItem = $('<li></li>').text(JSON.stringify(item));
                list.append(listItem);
            });
            dataDiv.append(list);
        } else {
            dataDiv.text('No data available.')
        }
    }
});