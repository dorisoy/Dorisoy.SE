"use strict";

$(document).ready(function () {
  Dropzone.options.singleFileUpload = {
    paramName: "file",
    // The name that will be used to transfer the file
    maxFiles: 1,
    init: function init() {
      this.on("maxfilesexceeded", function (file) {
        this.removeAllFiles();
        this.addFile(file);
      });
    }
  }; // multiple files

  Dropzone.options.multipleFileUpload = {
    paramName: "file",
    // The name that will be used to transfer the file
    maxFilesize: 1,
    // MB
    clickable: true
  }; // button slect

  new Dropzone(document.body, {
    // Make the whole body a dropzone
    url: "#",
    // Set the url
    previewsContainer: "#button-select-upload",
    // Define the container to display the previews
    clickable: "#button-select" // Define the element that should be used as click trigger to select files.

  }); // file limit

  Dropzone.options.fileLimitUpload = {
    paramName: "file",
    // The name that will be used to transfer the file
    maxFilesize: 1,
    // MB
    maxFiles: 4,
    maxThumbnailFilesize: 1 // MB

  }; // accepted files

  Dropzone.options.fileAcceptedUpload = {
    paramName: "file",
    // The name that will be used to transfer the file
    maxFilesize: 1,
    // MB
    acceptedFiles: 'image/*'
  }; // remove thumbnail

  Dropzone.options.fileRemoveThumbnailUpload = {
    paramName: "file",
    // The name that will be used to transfer the file
    maxFilesize: 1,
    // MB
    addRemoveLinks: true,
    dictRemoveFile: " Trash"
  };
});