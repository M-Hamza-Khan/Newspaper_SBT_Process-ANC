
// method for clear text

function clearText() {
    $("#txtphone").val("");
    $("#txtcontactname").val("");
    $("#txtaddress").val("");
    $("#txtemail").val("");
    $("#txtzipcode").val("");
    $("#txtstreet").val("");
    $("#txtcity").val("");
    $("#txtstate").val("");
    $("#txtUpc").val("");
    $("#txtvendorName").val("");
    $('#btnNext').attr('disabled', false);
    

}

//back to login page
function CancelPage(){
    debugger
    if(confirm("Are You Sure You Want To Cancel This Page?")){
        window.location.href='/login/index';
    }
    else{
        return false;
    }

}


function captchvalidate() {
    if (grecaptcha.getResponse().length == 0) {
        $("#lgcaptcha").css("border", "2px solid red");
        $("#lgcaptcha-error").html('please verify  your are not a bot..');
        $("#lgcaptcha-error").css("color", "red");
        $("#lgcaptcha-error").css("font-size", "10px");
        $('#btnNext').attr('disabled', true);


        return false;
    }
    return true;
}

//method for Button click 

$("#btnNext").click(function () {
    captchvalidate();

    if(initializeformValidation()){
        if( CaptchValidation()){
            $('.loader').show();
            CheckUpcVerifcationCode();
               
            
            }
        
        } });

function CleaEmailHighlight(){
    
    $('#txtemail').css('border-color',' white');
}

// method in this we send vendor registration , user details to controller through ajax call
function AddVendor(data){
    $('#btnNext').attr('disabled', true);
    //var UserId = 0;
    var captchaRes =grecaptcha.getResponse();
    var Contactname = $("#txtcontactname").val();
    var Vendorname = $("#txtvendorname").val();
    var Email = $("#txtemail").val();
    var City = $("#txtcity").val();
    var Street = $("#txtstreet").val();
    var Street1 = $("#txtstreet1").val();
    var State = $("#txtstate").val();
    var zipcode = $("#txtzipcode").val();
    var Phone = $("#txtphone").val();
    var upccode = $("#txtUpc").val();
    var roleid=3;
    var Model = {
        Vendor :{
            'Contactname': Contactname,
            'VendorName': Vendorname,
            'Street': Street+' '+ Street1,
            'City': City,
            'State': State,'upccode': upccode,
            'CaptchaResponse':captchaRes,
            'Zipcode': zipcode,
            'Email': Email,
            'Phone': Phone,
            'IsActive': true,   
            'DocumentSigned':"Document Not Signed",
        },

        userdetails: {
            user: {
                  
                'Fullname': Contactname,
                'Email': Email,
                'AccountTypeid':roleid,
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
            
    $.ajax({
        type: "POST",
        url: "/Vendor/AddVendor",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(Model),
        success: function (result) {
           
            var data = JSON.parse(result);
            grecaptcha.reset();
            $('.loader').hide();

            ShowMessage(data.MessageTitle, data.Message, data.MessageStatus);
            if (data.MessageStatus == 0) {

                setTimeout(function () {
                    var url = "/login/staticMessage";
                    window.location.href = url;
                }, 3000);

            }
            else{
                $('.loader').hide();

                $('#txtemail').css("border", "1px solid red");

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

// method for getting ip details
function initializeProcess(){
    var access_key = '6bc92aeeaec00461592fd1e701f03a19';
    var ip= $('#returnip').text();
    doAjaxGet( 'http://api.ipstack.com/' + ip + '?access_key=' + access_key)
      .then(json => {
          AddVendor(json);

      })
}


function CaptchValidation() {
    if (grecaptcha.getResponse().length == 0) {
        alert('Please click the reCAPTCHA checkbox');
        return false;
    }
    return true;
}

//This method will execute only if Google ReCaptcha expired
function expiredReCaptcha() { 
  
    $("#tbIsCaptchaChecked").val(""); 
}

function verifyReCaptcha() {
    //This method will execute only if Google ReCaptcha successfully validated
    $('#btnNext').attr('disabled', false);
    $("#tbIsCaptchaChecked").val("Success"); // making a dummy entry to hidden textbox
    $("#rfvtbIsCaptchaChecked").hide(); // hiding asp.Net requiredFieldValidator
    $("#isCaptchaChecked").hide(); // hiding custom error message
    $("#lgcaptcha").css("border", "none");
    $("#lgcaptcha-error").html('');

}










    













//todo this method will check valid upc then trigger further process / save vendor
function CheckUpcVerifcationCode() {
 
    var attempt = sessionStorage.getItem("UpcAtemp");
    var TotalAttempt = parseInt(attempt) + 1;

    sessionStorage.setItem("UpcAtemp", TotalAttempt);
    var code = $('#txtUpc').val();
 

            $.ajax({
                url: "/Vendor/UPCVerification",
                type: "POST",
                data: {
                    Atempt: TotalAttempt,
                    Code: code
                },
                dataType: "JSON",
                success: function (data) {
                 
                   
                    if(TotalAttempt>3){ 
                        var url = "/login/Eror407";
                        location.href = url;
                    }

                    if(data.MessageStatus==1){
                        $('.loader').hide();

                        ShowMessage(data.MessageTitle,data.Message,data.MessageStatus);
                        grecaptcha.reset();

                        $('#btnNext').attr('disabled', false);

                        return false;
                    }
                    else{

                        initializeProcess();
                      
                    
                    }
                }

            });
            return true;
}

function setAttemptcount(count) {
    var attemp = parseInt(sessionStorage.getItem("UpcAtemp")) + count;
    //document.getElementById("result").innerHTML = attemp;
    return attemp;
}

function initializeformValidation(){
    event.preventDefault();
    
    // Validate all input fields.
    var isValid = true;
    $('input').each( function() {
        if ($(this).parsley().validate() !== true) isValid = false;
    });
    if (isValid) {
        return true
    }
    else {
        grecaptcha.reset();

        false;
    }
  
    
}

function init() {
    var IsNull = '@Session["UpcAtemp"]' != null;  
    if (IsNull)
    {
        sessionStorage.setItem("UpcAtemp", 0);
    } 
    
}

