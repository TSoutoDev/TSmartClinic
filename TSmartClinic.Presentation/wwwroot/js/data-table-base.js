"use strict";
function dataTableInit(path, params) {
	var obj = {
		processing: true,
		serverSide: false,
		filter: true,
		orderMulti: true,
		pagingType: "full_numbers",
		language: languagePtBr(),
		ajax: {
			url: path,
			type: "POST",
			datatype: "json",
			data: {
				model: params
			},
			dataSrc: function (response) {				
				if (!!response && response.length <= 1000) {
					$('#alertExcessoLinhas').addClass('d-lg-none');
					return response;				
				}
				else {
					$('#alertExcessoLinhas').removeClass('d-lg-none');
					return response.slice(0, 1000);
				}
			},
			error: function (xhr, error, thrown) {
				console.log(error);
			},
		}
	};

	return obj;
}