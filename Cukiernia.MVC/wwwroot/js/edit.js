$('#loadPartialActionLink').on('click', function (e) {
    e.preventDefault();
    $.ajax({
        type: "GET",
        url: 'Baking/CreateSubProductsList/{encodedName}',
        success: function (data, textStatus, jqXHR) {
            $('#AJAXContainer').html(data);
        }
    });
});