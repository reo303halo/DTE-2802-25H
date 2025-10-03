$(document).ready(function() {
    var baseUrl = 'https://localhost:7122/api'

    function fetchProtectedData() {
        $.ajax({
            url: `${baseUrl}/Todo`,
            type: 'GET',
            success: function(response) {
                displayData(response);
            },
            error: function(xhr, status, error) {
                $('#message').text('An error occurred: ' + error);
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
            dataDiv.text('No data available.');
        }
    }

    // Fetch data when the page is ready
    fetchProtectedData()
});
