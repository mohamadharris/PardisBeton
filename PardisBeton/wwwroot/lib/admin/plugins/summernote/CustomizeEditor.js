$(document).ready(function () {
    function initializeSummernote(selector, uploadUrl) {
        $(selector).summernote({
            height: 300,
            fontNames: ['Vazirtmatn', 'Tahoma'],
            toolbar: [
                ['style', ['style']],
                ['font', ['bold', 'italic', 'underline', 'clear']],
                ['fontname', ['fontname']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['table', ['table']],
                ['insert', ['link', 'picture', 'video']],
                ['view', ['fullscreen', 'codeview', 'help']],
                ['custom', ['bootstrapGrid', 'ltr', 'rtl']]
            ],
            buttons: {
                bootstrapGrid: function (context) {
                    var ui = $.summernote.ui;
                    var button = ui.button({
                        contents: '<i class="fa fa-th-large"/> Bootstrap Grid',
                        tooltip: 'Insert Bootstrap Grid',
                        click: function () {
                            $('#bootstrapGridModal').modal('show');
                        }
                    });
                    return button.render();
                },
                ltr: function (context) {
                    var ui = $.summernote.ui;
                    var button = ui.button({
                        contents: '<i class="fa fa-align-left"/> LTR',
                        tooltip: 'Left-to-Right',
                        click: function () {
                            context.invoke('editor.saveRange');
                            var selectedText = context.invoke('editor.getSelectedText');

                            if (selectedText) {
                                context.invoke('editor.formatBlock', 'p');
                                document.execCommand('insertHTML', false, `<p style="direction: ltr; text-align: left;">${selectedText}</p>`);
                            } else {
                                context.invoke('editor.insertParagraph');
                                context.invoke('editor.formatPara', 'p');
                                context.invoke('editor.pasteHTML', '<p style="direction: ltr; text-align: left;">&nbsp;</p>');
                            }

                            context.invoke('editor.restoreRange');
                        }
                    });
                    return button.render();
                },
                rtl: function (context) {
                    var ui = $.summernote.ui;
                    var button = ui.button({
                        contents: '<i class="fa fa-align-right"/> RTL',
                        tooltip: 'Right-to-Left',
                        click: function () {
                            context.invoke('editor.saveRange');
                            var selectedText = context.invoke('editor.getSelectedText');

                            if (selectedText) {
                                context.invoke('editor.formatBlock', 'p');
                                document.execCommand('insertHTML', false, `<p style="direction: rtl; text-align: right;">${selectedText}</p>`);
                            } else {
                                context.invoke('editor.insertParagraph');
                                context.invoke('editor.formatPara', 'p');
                                context.invoke('editor.pasteHTML', '<p style="direction: rtl; text-align: right;">&nbsp;</p>');
                            }

                            context.invoke('editor.restoreRange');
                        }
                    });
                    return button.render();
                }
            },
            callbacks: {
                onImageUpload: function (files) {
                    for (var i = 0; i < files.length; i++) {
                        uploadImage(files[i], selector, uploadUrl);
                    }
                }
            }
        });
    }

    function uploadImage(file, summernoteId, uploadUrl) {
        var data = new FormData();
        data.append("file", file);

        $.ajax({
            url: uploadUrl, // Use the dynamic URL passed to the function
            cache: false,
            contentType: false,
            processData: false,
            data: data,
            type: "POST",
            success: function (url) {
                $(summernoteId).summernote('insertImage', url,
                    function ($image) {
                        $image.attr('src', url);
                        $image.attr('class', 'img-fluid');
                        $image.attr('alt', 'پردیس بتن');
                    });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Image upload failed: " + textStatus + " - " + errorThrown);
            }
        });
    }

    function setupGridModalHandlers(summernoteId) {
        $('#columnCount').change(function () {
            var columnCount = $(this).val();
            var columnSizesHtml = '';
            for (var i = 1; i <= columnCount; i++) {
                columnSizesHtml += `
                    <div class="form-group">
                      <label for="columnSize${i}">Column ${i} Size (1-12):</label>
                      <input type="number" id="columnSize${i}" class="form-control" min="1" max="12" value="${12 / columnCount}">
                    </div>
                `;
            }
            $('#columnSizes').html(columnSizesHtml);
        });

        $('#insertGrid').click(function () {
            var columnCount = $('#columnCount').val();
            var gridHtml = '<div class="row">\n';
            for (var i = 1; i <= columnCount; i++) {
                var columnSize = $('#columnSize' + i).val();
                gridHtml += `<div class="col-md-${columnSize}">\n  Grid ${i} \n </div>\n`;
            }
            gridHtml += '</div>\n';

            $(summernoteId).summernote('pasteHTML', gridHtml);
            $('#bootstrapGridModal').modal('hide');
        });
    }

    // Export the functions to be used globally
    window.initializeSummernote = initializeSummernote;
    window.setupGridModalHandlers = setupGridModalHandlers;
});
