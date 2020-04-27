

$(document).on("click", "#btnupdaste", function (event) {
 
    var isValid = true;
    if ($('#txxtname').parsley().validate() !== true) isValid = false;

    if (isValid) {
        $('#btnupdaste').attr('disabled', true);
        debugger
        var userid = $('#useridupdate').text();
        var email = $('#txtemailup').val();
        var name = $('#txxtname').val();
        var accounttypeid = $('#optpupdate option:selected').val();
        var user = {
            userid: userid,
            fullname: name,
            email: email,
            AccountTypeid: accounttypeid
        };

        $.ajax({
            type: "POST",
            url: "/Admin/updateuser",
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: JSON.stringify(user),
            success: function (result) {
                debugger
                ShowMessage(result.MessageTitle, result.Message, result.MessageStatus);
                if (result.MessageStatus == 0) {

                    BindSystemUser();
                    $('#btnupdaste').attr('disabled', false);

                    $("#editsystemuser").modal("hide");
                }
            }
        });
    }
    $('#btnupdaste').attr('disabled', false);

});
//bindAllusers
function BindAllUsers() {
    var isfinance = false;
    var daat = "";
    var html = null;
    $.ajax({
        type: "POST",
        url: "/Admin/GetAllUsers",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: '{}',
        success: function (result) {
            var json = JSON.parse(result);
            debugger
            isfinance = true;
            $('.loader').css("display", "none");
            $("#tbldemo").dataTable().fnDestroy();
            //var Id = json[index].Id;
            var editor = $('#tbldemo').DataTable({



                data: json,
                "order": [[0, "desc"]],
                columns: [
                { "data": "Userid" },
                { "data": "Name" },
                { "data": "Phone" },
                { "data": "useremail" },
                { "data": "Role" },
                { "data": "DocumentSigned" },
                { "data": "Version" },

               {
                   "data": null,
                   "render": function (result, type, row) {

                       //if (result.isActive == true) {
                       //    return ' <a href="javascript:;" data-toggle="modal"  data-target="#editmodal"><i  class="fa fa-pencil" onclick="Edit(' + result.Userid + ')"></i></a> <a href="#" onclick="Active(' + result.Userid + ',0)"><i class="fa fa-times"></i></a>'
                       //}
                       //else {
                       //    return ' <a href="javascript:;" data-toggle="modal" data-target="#editmodal"><i class="fa fa-pencil"  onclick="Edit(' + result.Userid + ')"></i></a> <a href="#" onclick="Active(' + result.Userid + ',1)"><i class="fa fa-eye"></i></a>'
                       //}
                       if (result.isActive == true) {
                           return ' <a href="javascript:;" data-toggle="modal"  data-target="#editmodal"><i  class="fa fa-pencil" onclick="Edit(' + result.Userid + ')"></i></a> <a onclick="Active(' + result.Userid + ',' + result.isActive + ')" style="cursor:pointer;"><i class="fa fa-times"></i></a>'
                       }
                       else {
                           return ' <a href="javascript:;" data-toggle="modal" data-target="#editmodal"><i class="fa fa-pencil"  onclick="Edit(' + result.Userid + ')"></i></a> <a onclick="Active(' + result.Userid + ',' + result.isActive + ')" style="cursor:pointer;"><i class="fa fa-check"></i></a>'
                       }
                       // return '<a href="javascript:;" ><i class="fa fa-pencil" aria-hidden="true" onlclick="Edit(' + result.Userid + ')" ></i></a><a href="#"><i class="fa fa-times"></i></a>'

                       /// '<a href="javascript:;" data-toggle="modal" ><i class="fa fa-times" onclick = "GetId(' + result.Userid + ')"></i></a>'
                       ////'<a href="javascript:;" data-toggle="modal" data-target="#addmodal"><i class="fa fa-check"></i></a>';


                   }
               },

                ]

            });
        }
    });


}

//function isValidEmail(emailText) {
//    var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
//    return pattern.test(emailText);
//};
//checkPwd = function () {
//    var str = $('#txtpassword').val();
//    if (str.length < 6) {
//        toastr.error("Password too_short", "Warning");
//        return false
//    } else if (str.length > 50) {
//        toastr.error("Password too_long", "Warning");
//        return false
//    } else if (str.search(/\d/) == -1) {
//        toastr.error("Password no_num", "Warning");
//        return false
//    } else if (str.search(/[a-zA-Z]/) == -1) {
//        toastr.error("Password no_letter", "Warning");
//        return false;
//    } else if (str.search(/[^a-zA-Z0-9\!\@\#\$\%\^\&\*\(\)\_\+\.\,\;\:]/) != -1) {
//        toastr.error("Password bad_char", "Warning");
//        return false
//    }

//    return true;
//}

//this method add new user
$(document).on("click", "#btnsaveuser", function (event) {
    debugger

    var isvalid = true;
    if ($('#txtpassword2').parsley().validate() !== true) isvalid = false;
    if ($('#txtconfirmpassword2').parsley().validate() !== true) isvalid = false;
    if ($('#txtname').parsley().validate() !== true) isvalid = false;
    if ($('#txtemail').parsley().validate() !== true) isvalid = false;

    if (isvalid) {

        $('#btnsaveuser').attr('disabled', true);
        //if (validationAdduser()) {
        var roles = $("#optproles option:selected").val();
        var email = $('#txtemail').val();
        var name = $('#txtname').val();
        var password = $('#txtpassword2').val();
        var Model = {

            Accounttypeid: roles,
            email: email,
            fullname: name,
            password: password
        }
        $.ajax({
            type: "POST",
            url: "/Admin/Adduser",
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: JSON.stringify(Model),
            success: function (result) {
                ShowMessage(result.MessageTitle, result.Message, result.MessageStatus);
                if (result.MessageStatus == 0) {
                    $('#txtemail').val('');
                    $('#txtname').val('');
                    $('#txtpassword2').val('');
                    $('#txtconfirmpassword2').val('');
                    $('#addnewuser').modal('hide');
                    $('#btnsaveuser').attr('disabled', false);

                    BindSystemUser();

                }

            }
        });
        $('#btnsaveuser').attr('disabled', false);

        //}
    }
});
//this mehtod for changepassword
function ChangePassword() {
    var isValid = true;
    if ($('#txtpassword').parsley().validate() !== true) isValid = false;
    if ($('#password2').parsley().validate() !== true) isValid = false;
    if (isValid) {
        $('#btnpasswordsave').attr('disabled', true);

        var password = $('#txtpassword').val();
        var email = $('#txtemail').val();

        $.post("/login/setPassword", {
            "email": email,
            "password": password,
        }, function (data) {

            ShowMessage(data.MessageTitle, data.Message, data.MessageStatus);
            $('#txtpassword').val('');
            $('#password2').val('');
            $('#btnpasswordsave').attr('disabled', false);

            $('#changepassword').modal('hide');
        });

    }
}
//todo this method is for set custom search bar
function setSearchbar() {
    $("#myInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tbldemobody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#inputsystemuser").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tblsystemuserbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
}

//todo click event for systm user tab
$('#Systemuser-tab').on('click', function () {
    BindSystemUser();
});

function BindSystemUser() {

    var html = null;
    $.ajax({
        type: "POST",
        url: "/Admin/Getsystemusers",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: '{}',
        success: function (result) {
            $('.loader').css("display", "none");
            $("#tblsystemuser").dataTable().fnDestroy();
            var json = JSON.parse(result);
            //var Id = json[index].Id;
            var editor = $('#tblsystemuser').DataTable({
                data: json,
                "order": [[0, "desc"]],
                columns: [
                { "data": "Userid" },
                { "data": "Name" },
                { "data": "email" },
                { "data": "Role" },
                {
                    "data": null,
                    "render": function (result, type, row) {
                        //if (result.isActive == true) {
                        //    return ' <a href="javascript:;" data-toggle="modal"  data-target="#editmodal"><i  class="fa fa-pencil" onclick="Edit(' + result.Userid + ')"></i></a> <a onclick="Activesystemuser(' + result.Userid + ',' + result.isActive + ')" style="cursor:pointer;"><i class="fa fa-times"></i></a>'
                        //}
                        //else {
                        //    return ' <a href="javascript:;" data-toggle="modal" data-target="#editmodal"><i class="fa fa-pencil"  onclick="Edit(' + result.Userid + ')"></i></a> <a onclick="Activesystemuser(' + result.Userid + ',' + result.isActive + ')" style="cursor:pointer;"><i class="fa fa-eye"></i></a>'
                        //}
                    
                        // return '<a href="javascript:;" ><i class="fa fa-pencil" aria-hidden="true" onlclick="Edit(' + result.Userid + ')" ></i></a><a href="#"><i class="fa fa-times"></i></a>'
                   var accounttypeid=     $('#accounttypeid').text();
                   if (accounttypeid == 2) {
                    if (result.isActive == true) {
                        return ' <a href="#d" data-toggle="modal" ><i class="fa fa-pencil" data-toggle="modal" href="#editsystemuser" onclick="loaddataforsystemuseredit(' + result.Userid + ')"  data-target="#editsystemuser"></i></a> <a href="#" onclick="Activesystemuser(' + result.Userid + ',' + result.isActive + ')"><i class="fa fa-times"></i></a>'
                    }
                    else {
                        return ' <a href="javascript:;" data-toggle="modal" data-target="#editsystemuser"><i class="fa fa-pencil" onclick="loaddataforsystemuseredit(' + result.Userid + ')"></i></a> <a href="#" onclick="Activesystemuser(' + result.Userid + ',' + result.isActive + ')"><i class="fa fa-check"></i></a>'
                    }
                   }
                   else {
                    if (result.isActive == true) {
                        return ' <a href="#d" data-toggle="modal" ><i class="fa fa-pencil" data-toggle="modal" href="#editsystemuser" onclick="loaddataforsystemuseredit(' + result.Userid + ')"  data-target="#editsystemuser"></i></a> <a href="#" onclick="Activesystemuser(' + result.Userid + ',' + result.isActive + ')"><i class="fa fa-times"></i></a>'
                    }
                    else {
                        return ' <a href="javascript:;" data-toggle="modal" data-target="#editsystemuser"><i class="fa fa-pencil" onclick="loaddataforsystemuseredit(' + result.Userid + ')"></i></a> <a href="#" onclick="Activesystemuser(' + result.Userid + ',' + result.isActive + ')"><i class="fa fa-check"></i></a>'
                    }

                   }// return '<a href="javascript:;" ><i class="fa fa-pencil" aria-hidden="true" onlclick="Edit(' + result.Userid + ')" ></i></a><a href="#"><i class="fa fa-times"></i></a>'

                        /// '<a href="javascript:;" data-toggle="modal" ><i class="fa fa-times" onclick = "GetId(' + result.Userid + ')"></i></a>'
                        ////'<a href="javascript:;" data-toggle="modal" data-target="#addmodal"><i class="fa fa-check"></i></a>';


                    }
                },

                ]

            });
        }
    });
}

//this fucntion for edit user set record on model 
function loaddataforsystemuseredit(userid) {
   debugger
    $.ajax({
        type: "POST",
        url: "/Admin/Getsystemusersbyid?userid=" + userid,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data:{},
        success: function (result) {
         
            var a = result[0].email;
            $('#txtemailup').val(result[0].email);
            $('#txxtname').val(result[0].Name);
      

            $("#optpupdate").val(result[0].AccountTypeid).change();;
            $('#useridupdate').html(result[0].Userid);
        }
    });

}
//todo update user status
function Active(Userid, isActive) {

    $.ajax({
        url: "/Admin/BandUserToLogin",
        type: "POST",
        data: {
            Userid: Userid,
            isActive: isActive
        },
        dataType: "JSON",
        success: function (data) {
            if (data == "Success") {
                ShowMessage("user Data updated Successfully.", "", 0);
                BindAllUsers();
            }
            else {
                ShowMessage(data, "", 1);

            }
        }
    });



}
function Activesystemuser(Userid, isActive) {

    $.ajax({
        url: "/Admin/BandUserToLogin",
        type: "POST",
        data: {
            Userid: Userid,
            isActive: isActive
        },
        dataType: "JSON",
        success: function (data) {
            if (data == "Success") {
                ShowMessage("user Data updated Successfully.", "", 0);
                BindSystemUser();
            }
            else {
                ShowMessage(data, "", 1);

            }
        }
    });



}
//todo this method is for check password comparision

function checkPasswordcomparision() {


    if ($('#txtpassword').val() != "") {
        if ($('#txtpassword').val() == $('#txtconfirmpassword').val()) {

            $('#txtconfirmpassword').css('border', '1px solid grey');

        }
        else {
            $('#txtpassword').css('border', '1px solid grey');
            $('#txtconfirmpassword').css('border', '1px solid Red');

        }
    }

}



$('document').ready(function () {

    setSearchbar();
    BindAllUsers();

});

//todo this method is for form vendor edit
function Edit(id) {
    debugger
    //if you want to pass an Id parameter
    var url = "/Admin/Editvendor?userid=" + id;
    window.location.href = url;


}
//todo this method is for form vendor update
function updateVendor() {
    debugger
    var city = $('#txtcity').val();
    var street = $('#txtstreet').val();
    var state = $('#txtstate').val();
    var zipcode = $('#txtzipcode').val();
    var email = $('#txtemail').val();
    var vendorname = $('#txtvendorname').val();
    var phone = $('#txtphone').val();
    var contactname = $('#txtcontactname').val();
    var vendorid = $('#vendorid').val();
    var vendor = {
        vendorname: vendorname,
        contactname: contactname,
        city: city, street: street,
        state: state, zipcode: zipcode,
        phone: phone, vendorid: vendorid
    };

    $.ajax({
        type: "POST",
        url: "/Admin/UpdateVendor",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(vendor),
        success: function (result) {
            debugger
            var data = jQuery.parseJSON(result)
            ShowMessage(data.MessageTitle, data.Message, data.MessageStatus);
            if (data.MessageStatus == 0) {
                setTimeout(function () {
                    var url = "/Admin/Index";
                    location.href = url;
                }, 3000);

            }

        }
    });
}


//tod btn click for update vendor details
$('#btnupdate').click(function () {
    $('#btnupdate').attr('disabled', true);
    if (validateupdateform()) {
        updateVendor();
    }


});

//this method for  input validation
function validateupdateform() {
    debugger

    // Validate all input fields.
    var isValid = true;
  
    if ($('#txtvendorname').parsley().validate() !== true) isValid = false;
    if ($('#txtcontactname').parsley().validate() !== true) isValid = false;
    if ($('#txtemail').parsley().validate() !== true) isValid = false;
    if ($('#txtphone').parsley().validate() !== true) isValid = false;
    if ($('#txtstreet').parsley().validate() !== true) isValid = false;
    if ($('#txtcity').parsley().validate() !== true) isValid = false;
    if ($('#txtstate').parsley().validate() !== true) isValid = false;
    if ($('#txtzipcode').parsley().validate() !== true) isValid = false;
   
    if (isValid) {
        return true;
    }
    else {
        $('#btnupdate').attr('disabled', false);

        false;
    }


    //}
    function OpenAddUserModal() {
        ResetValue();
        $("#addnewuser").modal("show");
    }
    function ResetValue() {
        $('#txtpassword').val("");
        $('#txtname').val("");
        $("#txtemail").val("");
    }
    function isValidEmail(emailText) {
        var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
        return pattern.test(emailText);
    };
    function checkPassword(str) {
        // at least one number, one lowercase and one uppercase letter
        // at least six characters that are letters, numbers or the underscore
        var re = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])\w{6,}$/;
        return re.test(str);
    }




  

    function checkchangePasswordpopup() {
        debugger
        var isvalid = true;
        if ($('txtpassword').val() == "") {
            $("#txtpassword").css("border", "1px solid red");
            isvalid = false;
        }
        if ($('txtconfirmpassword').val() == "") {
            $("#txtpassword").css("border", "1px solid red");
            isvalid = false;
        }

        if ($('#txtpassword').val() != "") {
            $('#txtpassword').css('border', '1px solid grey');
            if ($('#txtpassword').val() == $('#txtconfirmpassword').val()) {

                $('#txtconfirmpassword').css('border', '1px solid grey');

            }
            else {
                $('#txtpassword').css('border', 'none');
                $('#txtconfirmpassword').css('border', '1px solid Red');
                isvalid = false;

            }
        }
        return isvalid;
    }
}


//parseley validators for password

//has uppercase
window.Parsley.addValidator('uppercase', {
    requirementType: 'number',
    validateString: function (value, requirement) {
        var uppercases = value.match(/[A-Z]/g) || [];
        return uppercases.length >= requirement;
    },
    messages: {
        en: 'Your password must contain at least (%s) uppercase letter.'
    }
});

//has lowercase
window.Parsley.addValidator('lowercase', {
    requirementType: 'number',
    validateString: function (value, requirement) {
        var lowecases = value.match(/[a-z]/g) || [];
        return lowecases.length >= requirement;
    },
    messages: {
        en: 'Your password must contain at least (%s) lowercase letter.'
    }
});

//has number
window.Parsley.addValidator('number', {
    requirementType: 'number',
    validateString: function (value, requirement) {
        var numbers = value.match(/[0-9]/g) || [];
        return numbers.length >= requirement;
    },
    messages: {
        en: 'Your password must contain at least (%s) number.'
    }
});

//has special char
window.Parsley.addValidator('special', {
    requirementType: 'number',
    validateString: function (value, requirement) {
        var specials = value.match(/[^a-zA-Z0-9]/g) || [];
        return specials.length >= requirement;
    },
    messages: {
        en: 'Your password must contain at least (%s) special characters.'
    }
});
//this mehtod for systme user change password
function changeSystemUserPassword() {
    debugger
    var isValid = true;

    if ($('#txtpassword3').parsley().validate() !== true) isValid = false;
    if ($('#password3').parsley().validate() !== true) isValid = false;
    if (isValid) {
        $('#btnpasswordsave').attr('disabled', true);

        var password = $('#txtpassword3').val();
        var email = $('#txtemailup').val();

        $.post("/login/setPassword", {
            "email": email,
            "password": password,
        }, function (data) {

            ShowMessage(data.MessageTitle, data.Message, data.MessageStatus);
            $('#txtpassword3').val('');
            $('#password3').val('');
            $('#btnpasswordsave').attr('disabled', false);

            $('#changepasswordsystemuser').modal('hide');
        });

    }
}


$("#frmupload").submit(function (event) {
  debugger
  var fileurl2= document.getElementById("#fileInput").files[0].path
  var fileurl = $('#fileInput').val();
    $.ajax({
        type: "POST",
        url: "/Admin/termsandconditions",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: { fileurl: fileurl },
        success: function (result) {
            debugger
            var data = jQuery.parseJSON(result)
            ShowMessage(data.MessageTitle, data.Message, data.MessageStatus);
            if (data.MessageStatus == 0) {
                setTimeout(function () {
                    var url = "/Admin/Index";
                    location.href = url;
                }, 3000);

            }

        }
    });
});