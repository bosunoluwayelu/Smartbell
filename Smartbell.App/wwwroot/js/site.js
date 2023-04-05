$(function () {
    $('#loaderbody').addClass('hide'); // hide div at document ready

    $(document).bind('ajaxStart', function () {
        $('#loaderbody').removeClass('hide');
    }).bind('ajaxStop', function () {
        $('#loaderbody').addClass('hide');
    })
})

showInPopup = (url, title) => {
    try {
        $.ajax({
            type: 'GET',
            url: url,
            success: function (response) {
                $('#form-modal .modal-body').html(response);
                $('#form-modal .modal-title').html(title);
                $('#form-modal').modal('show');
            }
        })
    }
    catch (e) {
        console.log(e)
    }
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.isvalid) {
                    $('#view-all').html(response.html);
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    $.notify('submitted successfully', { globalPosition: 'top center', className: 'success'});
                }
                else {
                    $('#form-modal .modal-body').html(response.html);
                }
            },
            error: function (e) {
                console.log(e)
            }
        })
    }
    catch (e) {
        console.log(e)
    }

    return false;
}

jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (response) {
                    $('#view-all').html(response.html);
                    $.notify('submitted successfully', { globalPosition: 'top center', className: 'success' });
                },
                error: function (e) {
                    console.log(e);
                }
            })
        }
        catch (e) {
            console.log(e)
        }
    }

    return false;
}