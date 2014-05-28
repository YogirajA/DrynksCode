$('#editBtnForVenue').click(function (event) {
    event.preventDefault();
    var $form = $('#EditVenueForm'),
    url = $form.attr('action');
    var data = $form.serialize();
    $.post(url, data, function () {
        showMessage('<span>Everything saved Successfully!</span>', 'alert-success');
        setTimeout(function () {
            $("#resultDiv").hide(); 
        }, 5000);
        return false;
    }).error(function() {
        showMessage('<span>Something went wrong!</span>', 'alert-error');
    });
});

function showMessage(message, cssClass) {
    $("html, body").animate({ scrollTop: 0 }, "slow");
    var $resultDiv = $('#resultDiv');
    $resultDiv.show();
    var $messageDiv = $resultDiv.find('#messageDiv');
    $messageDiv.html(message);
    $messageDiv.addClass(cssClass);
}