$('document').ready(function () {


    window.ParsleyValidator.addValidator('fileextension', function (value, requirement) {
            var fileExtension = value.split('.').pop();

            return fileExtension === requirement;
        }, 32).addMessage('en', 'fileextension', 'The extension doesn\'t match the required');

    
      




    $("#termsrchinput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tblbdyterms tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
    bindterms();



});


function validatefile() {
    debugger
    var isValid=true;
    if ($('#file').parsley().validate() !== true) isValid = false;
    if (!$('#file').val()) {
        isValid = false
    }
    return isValid;
}

    function bindterms() {
        var html = null;
        $.ajax({
            type: "POST",
            url: "/Admin/getterms",
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: '{}',
            success: function (result) {
                $('.loader').css("display", "none");
                $("#tblterms").dataTable().fnDestroy();
                var json = JSON.parse(result);
                //var Id = json[index].Id;
                var editor = $('#tblterms').DataTable({
                    data: json,
                    "order": [[0, "desc"]],
                    columns: [
                    { "data": "versionid" },
                  {
                      "data": null,
                      "render": function (result, type, row) {
                          var filename = result.fileName +''+ result.versionid;
                          return filename;

                      }
                  },
                    {
                        "data": null,
                        "render": function (result, type, row) {
                            return '  <a href="/admin/DownLoadFile?id='+result.versionid+'" ><i class="fa fa-eye"></i></a>';
                            debugger
                        }
                    },
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
                            var accounttypeid = $('#accounttypeid').text();
                            debugger
                            if (accounttypeid == 2) {
                                if (result.isActive == true) {
                                    return '  <a href="#" onclick="ActiveTerms(' + result.versionid + ',false)"><i class="fa fa-times"></i></a>'
                                }
                                else {
                                    return '  <a href="#" onclick="ActiveTerms(' + result.versionid + ',true)"><i class="fa fa-check"></i></a>'
                                }
                            }
                            else {
                                if (result.isActive == true) {
                                    return '  <a href="#" onclick="ActiveTerms('+result.versionid+',false)"><i class="fa fa-times"></i></a>'
                                }
                                else {
                                    return '  <a href="#" onclick="ActiveTerms('+result.versionid+',true)"><i class="fa fa-check"></i></a>'
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


    function ActiveTerms(versionid, isactive) {

        $.ajax({
            url: "/Admin/UpdateTerms",
            type: "POST",
            data: {
                versionid: versionid,
                isactive: isactive
            },
            dataType: "JSON",
            success: function (data) {
                debugger
                if (data.MessageType == "success") {
                    bindterms();
                    toastr.success(data.MessageTitle, "success");

                }
                else {
                    toastr.error(data.MessageTitle, "Warning");
                }
            }
        });



    }