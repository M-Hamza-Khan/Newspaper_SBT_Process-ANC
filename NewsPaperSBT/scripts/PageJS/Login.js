
//todo this method will check validation before form submission
$("#btnNext").click(function () {

    checkisuserblock();

});
//function formsubmissionvalidation() {
//    $("#btnsubmit").click(function () {
//        if (captchvalidate()) { return true; } else { return false; }
       

//    });
   

//}
//todo this method will check is user blocked for registration or not
function checkisuserblock() {
    $.ajax({
        url: "/vendor/checkisuperBlocked",
        type: "POST",
        data: '',
        dataType: "JSON",
        success: function (data) {




            if (data.MessageStatus == 1) {

                var url = "/login/Eror407";
                location.href = url;

            }
            else {


                var url = "/vendor/index";
                location.href = url;

            }
        }

    });
}
function init() {

    setalert();
}

$('document').ready(function () {
     
    init();
});

function setalert() {

    var message = $('#message').text();
    var isvalid = $('#isvalid').text();
    if (message !== "") {

        ShowMessage("", message, isvalid);
        $('#message').html('');
        $('#isvalid').html('');
    }
}


  
function captchvalidate() {
    initializeformValidation();
    if (grecaptcha.getResponse().length == 0) {
        $("#lgcaptcha").css("border", "2px solid red");
        $("#lgcaptcha-error").html('please verify  your are not a bot..');
        $("#lgcaptcha-error").css("color", "red");
        $("#lgcaptcha-error").css("font-size", "10px");

        return false;
    }


    return true;
}


function expiredReCaptcha() { 
  
    $("#tbIsCaptchaChecked").val(""); 
   }
function verifyReCaptcha() {
    //This method will execute only if Google ReCaptcha successfully validated
    $('#btnsubmit').attr('disabled', false);
    $("#tbIsCaptchaChecked").val("Success"); // making a dummy entry to hidden textbox
    $("#rfvtbIsCaptchaChecked").hide(); // hiding asp.Net requiredFieldValidator
    $("#isCaptchaChecked").hide(); // hiding custom error message
    $("#lgcaptcha").css("border", "none");
    $("#lgcaptcha-error").html('');
}


function initializeformValidation(){
    $('#frmlogin').parsley().on('field:validated', function () {
        var ok = $('.parsley-error').length === 0;
        $('.bs-callout-info').toggleClass('hidden', !ok);
        $('.bs-callout-warning').toggleClass('hidden', ok);
    })
  .on('form:submit', function () {
      return true; // Don't submit form for this demo
  });
}

// To Do this method is used for forgot password request
var url = '/login/ForgetPassword';
$('#recover-submit').on('click', function () {
    toastr.remove();
    if($('#email').val() == ''){
        toastr.error("Email must not be blank","Warning");
    }
    else {
        $(this).attr("disabled", true);
        var isValid = true;

        if ($('#email').parsley().validate() !== true) { isValid = false; }
        if (isValid) {

            $.ajax({
                url: url,
                type: "post",
                data: {
                    email: $('#email').val()
                },
                datatype: "json",
                success: function (data) {
                    if (data == true) {
                        toastr.success("Passwrod Reset Successfull check your email", "Success");
                        window.location.href = '/login/Index';
                    }
                    else {
                        $('#recover-submit').attr('disabled', false);
                        toastr.error("Email does not exist", "Warning");
                        $('#email').val('');
                    }
                }
            });
        }
    }
});