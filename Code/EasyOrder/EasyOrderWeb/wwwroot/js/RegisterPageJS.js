
function validateForm() {
    $.ajax({
        url: "/api/user/register",
        method: "POST",
        data: JSON.stringify({
            UserName: $("#nickname").val(),
            Password: $("#password").val(),
            FullName: $("#fullname").val(),
            CI: $("#ci").val(),
            PhoneNumber: $("#phone").val()
        }),
        dataType: 'json',
        contentType: "application/json",
        async: false,
        success: function (result, status, jqXHR) {
            if (result.allowed) {
                targetUrl.value = "/Index";
            }
            else {
                targetUrl.value = "/Error";

            }
        },
        error(jqXHR, textStatus, errorThrown) {
            targetUrl.value = "/Error";
        }
    });
}

//function validateCI() {
//    $.ajax({
//        url: "/api/user/validateci",
//        method: "POST",
//        data: JSON.stringify({
//            CI: $("#ci").val()
//        }),
//        dataType: 'json',
//        contentType: "application/json",
//        async: false,
//        success: function (result, status, jqXHR) {
//            if (result.allowed) {
//                validateForm();
//                targetUrl.value = "/RegisterPage2";
//            }
//            else {
//                targetUrl.value = "/Error";

//            }
//        },
//        error(jqXHR, textStatus, errorThrown) {
//            targetUrl.value = "/Error";
//        }
//    });
//}