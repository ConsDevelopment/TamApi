function Accept(id,stat) {
	alert(stat);
	source = {
		"Id": id,
		"status": stat
	};

	$.ajax({

		type: "POST",
		url: $("#ApiServer").val() + "/api/RegConfirmation",
		data: JSON.stringify(source),
		//data: "1",
		contentType: 'application/json; charset=utf-8',

		//dataType: 'json',

		success: function (data) {
			alert(data);


		},

		error: function (error) {
			alert(error);
			jsonValue = jQuery.parseJSON(error.responseText);

		}

	});
}