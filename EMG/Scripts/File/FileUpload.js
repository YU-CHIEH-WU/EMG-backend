$(function () {
    var bar = $('.progress-bar');
    var percent = $('.progress-bar');
    var status = $('#status');
    $('form').ajaxForm({
        beforeSend: function () {
            $(".progress").show(500);
            var percentVal = '0%';
            bar.width(percentVal)
            percent.html(percentVal);
        },
        uploadProgress: function (event, position, total, percentComplete) {
            var percentVal = percentComplete + '%';
            bar.width(percentVal)
            percent.html(percentVal);
        },
        success: function () {
            var percentVal = '100%';
            bar.width(percentVal)
            percent.html(percentVal);
        },
        complete: function (xhr) {
            window.setTimeout(function ResetForm() {
                $("#FileUpload").find("#files").val("");
                $(".progress").hide(500);
            }, 1000)
            status.html(xhr.responseText);
        },
    });
});