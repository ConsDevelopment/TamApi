

function logIn() {
	alert($("#ApiServer").val());
    source = {
        "UserName": $("#UserName").val(),
        "Password": $("#Password").val(),
        "RememberMe": $("#RememberMe").is(":checked")
    };

    $.ajax({

        type: "POST",
        url: $("#ApiServer").val() + "/api/Login",
        data: JSON.stringify(source),
        //data: "1",
        contentType: 'application/json',

        //dataType: 'json',

        success: function (data) {
        	location.href = "../Confirmation/DriverConfirmation";


        },

        error: function (error) {
            alert(error);
            jsonValue = jQuery.parseJSON(error.responseText);

        }

    });

}