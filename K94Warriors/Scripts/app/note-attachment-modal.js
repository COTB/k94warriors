function AttachmentModel(dogNoteAttachmentId, fileName) {
    var self = this;

    self.dogNoteAttachmentId = ko.observable(dogNoteAttachmentId);
    self.fileName = ko.observable(fileName);
    self.downloadLink = ko.computed(function() {
        return '/Notes/DownloadFile/' + self.dogNoteAttachmentId();
    }, self);
}

function AttachmentModalViewModel(noteId) {
    var self = this;

    var thumbsPerFrame = 4;

    var filePicker = $('#upload form input:file')[0];

    self.noteId = ko.observable(noteId);
    self.downloadAllLink = ko.computed(function() {
        return '/Notes/DownloadAllFiles?dogNoteId=' + self.noteId();
    }, self);
    self.attachments = ko.observableArray([]);
    self.hasAttachments = ko.computed(function() {
        return self.attachments().length > 0;
    });
    self.selectedAttachment = ko.observable();

    self.uploadProgress = ko.observable(0);
    self.isUploading = ko.observable(false);
    self.filesSelected = ko.observable(false);
    self.uploadEnabled = ko.computed(function() {
        return !self.isUploading() && self.filesSelected();
    });
    self.cancelPickedFiles = function() {
        $(filePicker).closest('form')[0].reset(true);
        $(filePicker).trigger('change');
    };
    self.fileInputChanged = function() {
        var selected = filePicker.files.length > 0;
        self.filesSelected(selected);
    };

    self.numberOfFrames = ko.computed(function() {
        return self.attachments().length / thumbsPerFrame;
    }, self);

    self.frames = ko.computed(function() {
        var attachments = self.attachments();
        var frames = [];
        var count = attachments.length;
        
        for (var i = 0; i < count; i += thumbsPerFrame) {
            var frame = [];
            for (var j = i; j < i + thumbsPerFrame && j < count; j++) {
                frame.push(attachments[j]);
            }
            frames.push(frame);
        }
        return frames;
    }, self);

    self.uploadFiles = function() {
        var fd = new FormData($('#fileUploadForm')[0]);
        $.ajax({            
            url: '/Notes/UploadFiles',
            type: 'POST',
            xhr: function() {
                var xhr = $.ajaxSettings.xhr();
                if (xhr.upload) {
                    xhr.upload.addEventListener('progress',
                        function(e) {
                            if (e.lengthComputable) {
                                var percent = e.loaded / e.total;
                                self.uploadProgress(percent);
                            }
                        }, false);
                }
                return xhr;
            },
            success: function(response) {
                if (response.success) {
                    self.getAttachmentKeys();
                    toastr.success('File upload complete.');
                } else {
                    if (response.errorMessage)
                        toastr.error(response.errorMessage);
                }
            },
            error: function(response) {
                toastr.error('Error uploading files.');
            },
            data: fd,
            cache: false,
            contentType: false,
            processData: false
        });
    };

    self.deleteAttachment = function(attachment) {
        $.ajax({            
            url: '/Notes/DeleteFile/' + attachment.dogNoteAttachmentId(),
            method: 'POST',
            success: function (response) {
                if (response.success) {
                    toastr.success('Attachment deleted.');
                    self.getAttachmentKeys();
                } else {
                    if (response.errorMessage)
                        toastr.error(response.errorMessage);
                }
            },
            error: function(response) {
                toastr.error('Error deleting attachment.');
            }
        });
    };

    self.getAttachmentKeys = function() {
        $.ajax({
            url: '/Notes/AttachmentKeys',
            data: { dogNoteId: self.noteId() },
            success: function(response) {
                self.attachments([]);
                response.forEach(function(attachment) {
                    self.attachments.push(new AttachmentModel(attachment.DogNoteAttachmentID, attachment.FileName));
                });
            },
            error: function() {
                toastr.error('Error retrieving attachments for this note!');
            }
        });
    };

    self.getAttachmentKeys();
}

KoMouseover.SetupBindings(ko);