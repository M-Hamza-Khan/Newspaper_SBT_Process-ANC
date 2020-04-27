



$('document').ready(function () {

    var ip = $('#currentip').text();
    var access_key = '6bc92aeeaec00461592fd1e701f03a19';

    // get the API result via jQuery.ajax
    $.ajax({
        url: 'http://api.ipstack.com/' + ip + '?access_key=' + access_key,
        dataType: 'jsonp',
        success: function (json) {
            console.log(json);

            $('#currentIp').html(json.ip);
            $('#currentcountry').html(json.country_name);
            $('#currentstate').html(json.region_name);
            $('#currentcity').html(json.city);

        }
    });

});