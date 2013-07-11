$(document).ready(function() {
	$('a#fancybox').fancybox({
		'type': 'image',
		'titlePosition': 'inside'
	});

	$('#search-query').autocomplete({
		source: '/AutoComplete/',
		minLength: 3,
		select: function(event, ui) {
			//assign value back to the form element
			if (ui.item) {
				$(event.target).val(ui.item.value);
			}
			//submit the form
			$(event.target.form).submit();
		}
	});

	var dynStories = $('#dynStories');
	$('#addStory').live('click', function() {
		$('<p><input class="inputStory" type="text" name="stories" value="" placeholder="Geschichte" /> <a href="#" class="remStory" style="margin-left: 10px;"><img src="/Content/cross.png" alt="Entfernen" title="Entfernen" /></a> <a href="#" class="insStory"><img src="/Content/add.png" alt="Einfügen" title="Einfügen" /></a></p>').appendTo(dynStories);
		return false;
	});

	$('.insStory').live('click', function() {
		var o = $(this).parents('p');
		$('<p><input class="inputStory" type="text" name="stories" value="" placeholder="Geschichte" /> <a href="#" class="remStory" style="margin-left: 10px;"><img src="/Content/cross.png" alt="Entfernen" title="Entfernen" /></a> <a href="#" class="insStory"><img src="/Content/add.png" alt="Einfügen" title="Einfügen" /></a></p>').insertBefore(o);
		return false;
	});

	$('.remStory').live('click', function() {
		$(this).parents('p').remove();
		return false;
	});

	$('.delimg').live('click', function() {
		data = { 'id': $('#id').val() };

		$.post('/book/removeimage/', data, function(data) {
			var ret = data['success'];
			if (ret != undefined) {
				if (ret === "true")
					$('.imgdisp').remove();
			}
		}).fail(function() {
			$('#message-box').dialog('open');
		});
	});
	
	$('#message-box').dialog({
		autoOpen: false,
		resizable: false,
		draggable: false,
		modal: true,
		title: 'Fehler',
		buttons: {
			Ok: function() {
				$(this).dialog('close');
			}
		}
	});
});