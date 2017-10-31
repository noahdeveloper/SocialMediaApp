$(function () {
    // When your form is submitted
    $("form").submit(function (e) {
        // Prevent the default behavior
        e.preventDefault();

        // Serialize your form
        var formData = new FormData($(this)[0]);

        // Make your POST
        $.ajax({
            type: 'POST',
            url: 'TestUpload2/Settings',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (view) {
                alert("success");
            },
            complete: function () {
                alert("complete");
            }
        });
    });
})