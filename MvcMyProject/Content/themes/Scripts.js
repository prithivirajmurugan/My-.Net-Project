$(document).ready(function () {
    $("#btn_cust").click(function () {
        var address = "/Customer/getpgbycity";
        var city = $("#customerCity").val();
        $.getJSON(address, { givencity: city }, function (data)
        {
            var str = JSON.stringify(data);
            var jsondata = $.parseJSON(str);
            var table = "<table class='table-hover'><tr><td>PG Name</td><td>PG Address</td><td>PG Near By Area</td><td>PG Amenities</td><td>PG Image</td></tr>";
            $.each(jsondata, function () {
                var Pgname = this["pgName"];
                var PgAddress = this["pgAddress"];
                var PgNearByArea = this["nearbyarea"];
                var PgImage = this["pgImagesAddress"];
                var PgAmenities = this["amenities"];
                table = table + "<tr><td>" + Pgname + "</td><td>" + PgAddress + "</td><td>" + PgNearByArea + "</td><td>" + PgAmenities + "<td></td>" + "<img height='100px' width='150px' src=" + PgImage + " />";
            });
            table = table + "</table>";
            $("#result").html(table);


        });


    });
   


});



