
$("#txtcontactname").on('keyup', function () {
    CheckValid("txtcontactname");
});
$("#txtvendorname").on('keyup', function () {
    CheckValid("txtvendorname");
});
$("#txtstreet").on('keyup', function () {
    CheckValid("txtstreet");
});
$("#txtcity").on('keyup', function () {
    CheckValid("txtcity");
});
$("#txtstate").on('keyup', function () {
    CheckValid("txtstate");
});
$("#txtzipcode").on('keyup', function () {
    CheckValid("txtzipcode");
});

$("#txtemail2").on('keyup', function () {
    CheckValid("txtemail2");
});
$("#txtphone").on('keyup', function () {
    CheckValid("txtphone");
});


function CheckValid(id) {
    $('#' + id).css("border-color", "#cccccc");
}
    function Validation() {
        var isValid = true;
        if ($("#txtcontactname").val() == "") {
            isValid = false;
            $("#txtcontactname").css("border", "1px solid #ee5f5b");
        }
        if ($("#txtvendorname").val() == "") {
            isValid = false;
            $("#txtvendorname").css("border", "1px solid #ee5f5b");
        }
        if ($("#txtstreet").val() == "") {
            isValid = false;
            $("#txtstreet").css("border", "1px solid #ee5f5b");
        }
        if ($("#txtcity").val() == "") {
            isValid = false;
            $("#txtcity").css("border", "1px solid #ee5f5b");
        }
        if ($("#txtphone").val() == "") {
            isValid = false;
            $("#txtphone").css("border", "1px solid #ee5f5b");
        }
        if ($("#txtzipcode").val() == "") {
            isValid = false;
            $("#txtzipcode").css("border", "1px solid #ee5f5b");
        }
        if ($("#txtstate").val() == "") {
            isValid = false;
            $("#txtstate").css("border", "1px solid #ee5f5b");
        }
        if ($("#txtemail2").val() == "") {
            isValid = false;
            $("#txtemail2").css("border", "1px solid #ee5f5b");
        }
        else if ($("#txtphone").val().indexOf("_") > 0) {
            isValid = false;
            $("#txtphone").css("border", "1px solid #ee5f5b");
        }
      
        return isValid;
    }

    function clearText() {
        $("#txtphone").val("");
        $("#txtcontactname").val("");
        $("#txtaddress").val("");
        $("#txtemail2").val("");
    }


    $("#btnNext").click(function () {
        if (Validation() != false) {
            initializeProcess();
        }
});

    function AddVendor(data){
        $('#btnNext').attr('disabled', true);
        //var UserId = 0;
        var Contactname = $("#txtcontactname").val();
        var Vendorname = $("#txtvendorname").val();
        var Email = $("#txtemail").val();
        var City = $("#txtcity").val();
        var Street = $("#txtstreet").val();
        var State = $("#txtstreet").val();
        var zipcode = $("#txtzipcode").val();
        var Phone = $("#txtphone").val();

       
        var Model = {
            Vendor :{
                'Contactname': Contactname,
                'VendorName': Vendorname,
                'Street': Street,
                'City': City,
                'State': State,
                'Zipcode': zipcode,
                'Email': Email,
                'Phone': Phone,
                'IsActive': false,                     
            },

            userdetails: {
                user: {
                  
                    'Fullname': Contactname,
                           'Email': Email,
                           'Roll': 3,
                           'isActive': false,
                },
                userregion: {
     
                'Regionid':0,
                 'IP':data.ip,
                 'Country':data.country_name,
                  'State':data.region_name,
                   'City':data.city,
                }

            }

        };
        debugger
            
            $.ajax({
                type: "POST",
                url: "/Vendor/AddVendor",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(Model),
                success: function (result) {
                    if (result != "Failed") {
                        //toastr.success("User Submit Successfully", "Success");
                        clearText();
                    }
                    else {
                        $('#btnNext').attr('disabled', false);
                    }
                }
            });
        }
      
    

async function doAjaxGet(ajaxurl) {
   const result = await $.ajax({
        url: ajaxurl,
       type: 'GET',
    });
  return result;
}

function initializeProcess(){
    var access_key = '6bc92aeeaec00461592fd1e701f03a19';

    var ip= '27.255.5.75';
    doAjaxGet( 'http://api.ipstack.com/' + ip + '?access_key=' + access_key)
      .then(json => {
          AddVendor(json);

      })
}


//function UpdateUser(data) {

//    //toastr.remove();
//    $('#btnsubmit2').attr('disabled', true);
//    var location = window.location;
//    console.log(  data.ip);
//    var Userid = 0;
//    var name = $("#txtname").val();
//    var Email = $("#txtemail").val();
   

//    var user = {

//        'Userid': Userid,
//        'Fullname': name,
//        'FirstsName': '',
//        'LastName': '',
//        'Email': Email,
//        'Password': '',
//        'Roll': 1,
//        'isActive': false,
//    }

//    var userregion = {

//        'Userid':Userid,
//        'Regionid':0,
//        'IP':data.ip,
//        'Country':data.country_name,
//        'State':data.region_name,
//        'City':data.city,
//    } 

//    var model = {

//        "User": user,

//        "userregion": userregion
//    }


//    $.ajax({
//        type: "POST",
//        url: "/User/AddUser",
//        contentType: "application/json; charset=utf-8",
//        dataType: 'json',
//        data: JSON.stringify(model),
//        success: function (result) {

//            if (result) {
//                //  toastr.success("User Submit Successfully", "Success");
//                //window.location = result.url;
//                debugger
//                console.log(result.Userid);

//                window.location.href = "/Vendor/Index";
//            }
//            else {
//                // toastr.error("Something went wrong", "Warning");
//                $('#btnsubmit2').attr('disabled', false);
//                $(".ghost").click(function () {
//                    $(".block").toggleClass("left-panel-active");
//                });
//            }
//        }       });    
 


//}