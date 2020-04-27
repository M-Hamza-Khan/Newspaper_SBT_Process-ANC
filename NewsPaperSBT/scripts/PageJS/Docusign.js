
$('#btnsubmit').click(function () {

    var ischeck = isusercheckonterms();
    var versionid = $('#versionid').text();

    if (ischeck == false) {
        return false;
    }
    else {
        $("#btnsubmit").attr("disabled", true);
        $.ajax({
            type: "POST",
            url: "/Docusign/TermsAncConditions",
            data:{
                Name: $('#txtName').val(),
                versionid: versionid
            },
            dataType: 'JSON',
            success: function (result) {
                console.log(result);
                ShowMessage(result.MessageTitle, result.Message, result.MessageStatus);
                if (result.MessageStatus == 0) {
                    setTimeout(function () {

                        var url = "/Finance/Index";
                        window.location.href = url;
                    }, 2000);
                }
            }
        });
    }
});



$('#chkterms').on('click', function () {
    if ($(this).is(':checked')) {
        $('#txtName').attr("disabled", false);
    }
    else {
        $('#txtName').attr("disabled", true);

    }
});

//edit by eu

//check is user  checked on tersm and condition
function isusercheckonterms() {
    debugger

    if ($('#chkterms').is(":checked")) {
        return true
    } else {

        $("#btnsubmit").attr("disabled", false);
        ShowMessage("kindly accept term and conditions", "", 1)
        return false;
    }
    return false;


}
$('#btndownload').click(function () {
    var version = $('#versionid').text();
    var url = "/vendor/DownLoadFile?versionid=" + version;
    location.href = url;

})
$('#btnprintview').click(function () {
    var version = $('#versionid').text();
    var url = "/vendor/printview?versionid=" + version;
    window.open( url);

})
printview

function init() {
    document.getElementById('chkterms').checked = false;
    debugger
    getcurrentlyvisibleDocument();
}

$(document).ready(init());


function getcurrentlyvisibleDocument() {
    $.ajax({
        url: "/vendor/getvisbledocument",
        type: "POST",

        dataType: "JSON",
        success: function (data) {
            debugger
            var fileName = data.fileName;
            var versionid = data.versionid;
            var parameter = data.fileName + data.versionid + ".pdf";
            $('#versionid').html(versionid);
            loadPDF(parameter);
        }
    });
}