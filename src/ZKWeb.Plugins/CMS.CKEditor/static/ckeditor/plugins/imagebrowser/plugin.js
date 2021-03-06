// Author: 303248153@github
// License: MIT License
// This plugin is for integration with zkweb ckeditor and image browser plugin
//

(function () {
	CKEDITOR.plugins.add("imagebrowser", {
		requires: "filebrowser",

		init: function (editor) {
			// set image browse url if image upload category is specificed
			var imageBrowserUrl = editor.config.imageBrowserUrl;
			if (imageBrowserUrl) {
				editor.config.filebrowserImageBrowseUrl = imageBrowserUrl;
				editor.config.filebrowserImageUploadUrl = imageBrowserUrl + "/upload";
			}
		}
	});

	var overrideDialogDefinition = function (editor, elements) {
		for (var i = 0; i < elements.length; ++i) {
			var element = elements[i];
			element.elements && overrideDialogDefinition(editor, element.elements);
			// modify quick upload button onclick event
			// use the result from ajax response
			if (element.type === "fileButton" && element.filebrowser &&
				element.filebrowser.url === editor.config.filebrowserImageUploadUrl) {
				var onClickPrev = element.onClick;
				element.onClick = function (event) {
					// call previous handler
					if (onClickPrev && onClickPrev.call(this, event) === false) {
						return false;
					}
					// find form element (require jquery)
					var target = element["for"];
					var dialog = event.sender.getDialog();
					var formContainer = dialog.getContentElement(target[0], target[1]);
					var $formFrame = $(document.getElementById(formContainer.domId)).find("iframe");
					var $form = $($formFrame[0].contentDocument).find("form");
					// submit form by ajax
					$form.ajaxSubmit(function (data) {
						$.handleAjaxResult(data);
						CKEDITOR.tools.callFunction(editor._.filebrowserFn, data.path);
					});
					return false;
				};
			}
			// handle file browser selected event
			// use the result from event posted by child window
			if (element.type === "button" && element.filebrowser &&
				element.filebrowser.url === editor.config.filebrowserImageBrowseUrl) {
				var eventName = "selected.imageBrowser";
				$(document).off(eventName).on(eventName, function (e, path) {
					CKEDITOR.tools.callFunction(editor._.filebrowserFn, path);
				});
			}
		}
	};

	CKEDITOR.on("dialogDefinition", function (e) {
		var definition = e.data.definition;
		var editor = e.editor;
		for (var i = 0; i < definition.contents.length; ++i) {
			var element = definition.contents[i];
			element && element.elements && overrideDialogDefinition(editor, element.elements);
		}
	});
})();
