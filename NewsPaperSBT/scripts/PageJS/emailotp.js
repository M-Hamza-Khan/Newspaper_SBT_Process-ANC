$('document').ready(function () {
    debugger
    var msg = $('#message').text();
    var msgdesc = $('#messagedesc').text();
    if (msg.length > 1) {
        if (msgdesc == "1 ") {
            ShowMessage(msg, '', 1);

            setTimeout(function () {
                var url = "/login/Index";
                location.href = url;
            }, 3000);

        } else {

            ShowMessage(msg, msgdesc, 1);
            $('#message').html('');
            $('#messagedesc').html('');

        }
     
    }
});
//this methdo for form validate
function formvalidate() {
    var otp = $('#txtotp').val();
    if (otp == "") {
        $("#txtotp").css("border", "1px solid #ee5f5b");
        return false;
    }
    else {
        return true;
    }
}
//this method for resend otp
function ResendOtp() {
    $('#btnresend').attr('disabled', true);

    $.ajax({
        type: "POST",
        url: "/login/ResendOTP",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: '{}',
        success: function (result) {
            ShowMessage(result.MessageTitle, result.Message, result.MessageStatus);
            $('#btnresend').attr('disabled', false);

        }
   

    });
    $('#btnresend').attr('disabled', false);
}

//this event for resend verfication code 
$('#btnresend').click(function () {

    ResendOtp();

});
    function clearborder() {

        $('#txtotp').css("border-color", "#cccccc");

    }
