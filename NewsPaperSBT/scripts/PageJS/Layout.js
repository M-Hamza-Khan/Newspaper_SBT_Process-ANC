
//todo this method will closo current session /logout 
function SeesionLogout() {

    $.ajax({
        type: "POST",
        url: "/Login/Logout",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        //data: JSON.stringify(record),
        success: function (result) {

            window.location.href = "/Login/Index";
        }
    });



}



//todo set Toastr Setting
function setToastrSetting() {
    toastr.options = {
        "closeButton": true,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "800",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
}

//todo initialization
function init() {
    setToastrSetting();
}

//todo this Method will get some parameters and display on page with current setting / 
function ShowMessage(title, description, status) {
    debugger
    if (status==1) {

        toastr.error(description, title);
    } else {

        toastr.success(description, title);

    }

}
//todo on document ready
$('document').ready(function () {
    debugger
    var username = $('#username').val();

    var string = username;
    if (string.indexOf(" ") > -1) {
        // Contains a space
        var username = $('#username').val();
        var firstname = username.charAt(0);
        var lastname = username.substr(username.indexOf(' ') + 1).charAt(0);
        var intials = firstname + lastname;
        var profileImage = $('#profileImage').text(intials);

    } else {
        var username = $('#username').val();
        var firstname = username.charAt(0);
        var intials = firstname;
        var profileImage = $('#profileImage').text(intials);
    }
   

    init();
});
