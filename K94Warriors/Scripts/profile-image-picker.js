function ImageModel(blobKey, mimeType, index) {
    var self = this;

    // Private
    var baseUrl = '/Dog/ImageForBlobKey';
    var params = function (height, width) {
        return '?blobKey=' + self.blobKey() + '&mimeType=' + self.mimeType() +
            (height ? '&height=' + height : "") + (width ? '&width=' + width : "");
    };

    // Exported properties
    self.blobKey = ko.observable(blobKey);
    self.mimeType = ko.observable(mimeType);
    self.index = ko.observable(index);

    // Computed properties
    self.thumbnailUrl = ko.computed(function () {
        return baseUrl + params(250, 250);
    });
    self.customSizeUrl = ko.computed(function (height, width) {
        return baseUrl + params(height, width);
    });
    self.fullSizeUrl = ko.computed(function () {
        return baseUrl + params();
    });
}

function ThumbnailModalViewModel(profileId) {
    var self = this;

    var imagesPerFrame = 4;

    var doFileUpload = function (form, url, progressHandler, success, error) {

    };

    var filePicker = $('#upload form input:file')[0];

    // Exported properties
    self.profileId = ko.observable(profileId);
    self.images = ko.observableArray([]);
    self.selectedImage = ko.observable();

    self.uploadProgress = ko.observable(0);
    self.isUploading = ko.observable(false);
    self.filesSelected = ko.observable(false);
    self.uploadEnabled = ko.computed(function () {
        return !self.isUploading() && self.filesSelected();
    });
    self.cancelPickedFiles = function () {
        $(filePicker).closest('form')[0].reset(true);
        $(filePicker).trigger('change');
    };
    self.fileInputChanged = function () {
        var selected = filePicker.files.length > 0;
        self.filesSelected(selected);
    };

    self.numberOfFrames = ko.computed(function () {
        return self.images().length / imagesPerFrame;
    }, self);

    self.frames = ko.computed(function () {
        var images = self.images();
        var frames = [];
        var count = self.images().length;

        for (var i = 0; i < count; i += imagesPerFrame) {
            var frame = [];
            for (var j = i; j < i + imagesPerFrame && j < count; j++) {
                frame.push(images[j]);
            }
            frames.push(frame);
        }
        return frames;
    }, self);

    // Exported methods
    self.uploadFiles = function () {
        //var input = $(filePicker);
        //input.ajaxfileupload({
        //    action: '/Dog/UploadDogImages',
        //    params: { dogProfileId: $('#upload form input:hidden') },
        //    onComplete: function(response) {
        //        alert(JSON.stringify(response));
        //    },
        //    onStart: function() {
                
        //    },
        //    onCancel: function() {
                
        //    }
        //});

        var formData = new FormData(document.getElementById('imageUploadForm'));
        //var formData = new FormData();
        //formData.append('dogProfileId', $('#dogProfileId').val());
        //var fi = document.getElementById('imageFileUpload');
        //var files = [];
        //for (var i = 0; i < fi.files.length; i++) 
        //    files.push(fi.files[i]);
        //formData.append('files', files);
        $.ajax({
            url: '/Dog/UploadDogImages',
            type: 'POST',
            xhr: function () {
                var myXhr = $.ajaxSettings.xhr();
                if (myXhr.upload) {
                    myXhr.upload.addEventListener('progress',
                        function (e) { // progress
                            if (e.lengthComputable) {
                                var percentComplete = e.loaded / e.total;
                                self.uploadProgress(percentComplete);
                            }
                        }, false);
                }
                return myXhr;
            },
            //Ajax events
            success: function (result) { // success
                if (result.success) {
                    self.getImageKeys();
                    toastr.success('File upload complete.');
                } else {
                    toastr.error(result.message);
                }
            },
            error: function () { // error
                toastr.error('Error uploading files.');
            },
            // Form data
            data: formData,
            //Options to tell jQuery not to process data or worry about content-type.
            cache: false,
            contentType: false,
            processData: false
        });
    };

    self.deleteImage = function (image) {
        console.log('Delete: ' + image.blobKey());
        $.ajax({
            url: '/Dog/DeleteImage',
            method: 'POST',
            data: { blobKey: image.blobKey() },
            success: function (result) {
                if (result.success) {
                    toastr.success('Image deleted.');
                    self.getImageKeys();
                } else {
                    toastr.error(result.message);
                }
            },
            error: function () {
                toastr.error('Error deleting image.');
            }
        });
    };

    self.getImageKeys = function () {
        $.ajax({
            url: '/Dog/ImageKeysForDogProfile',
            data: { dogProfileId: self.profileId() },
            success: function (result) {
                self.images([]);
                result.forEach(function (item) {
                    self.images.push(new ImageModel(item.BlobKey, item.MimeType, item.Index));
                });
            },
            error: function () {
                alert('Error retrieving image data for dog!');
            }
        });
    };

    self.getImageKeys();
}

function setupCustomBindings() {
    ko.bindingHandlers.hoverTargetId = {};
    ko.bindingHandlers.hoverVisible = {
        init: function (element, valueAccessor, allBindingsAccessor) {

            function showOrHideElement(show) {
                var canShow = ko.utils.unwrapObservable(valueAccessor());
                $(element).toggle(show && canShow);
            }

            var hideElement = showOrHideElement.bind(null, false);
            var showElement = showOrHideElement.bind(null, true);
            var $hoverTarget = $('#' + ko.utils.unwrapObservable(allBindingsAccessor().hoverTargetId));
            ko.utils.registerEventHandler($hoverTarget, 'mouseover', showElement);
            ko.utils.registerEventHandler($hoverTarget, 'mouseout', hideElement);
            hideElement();
        }
    };
}

setupCustomBindings();