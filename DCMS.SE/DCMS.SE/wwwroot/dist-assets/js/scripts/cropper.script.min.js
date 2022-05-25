"use strict";

$(document).ready(function () {
  var $image = $('.cropper-main-img');
  var $dataX = $('.cropper-main-dataX');
  var $dataY = $('.cropper-main-dataY');
  var $dataHeight = $('.cropper-main-dataHeight');
  var $dataWidth = $('.cropper-main-dataWidth');
  var $dataRotate = $('.cropper-main-dataRotate');
  var $dataScaleX = $('.cropper-main-dataScaleX');
  var $dataScaleY = $('.cropper-main-dataScaleY');
  var options = {
    viewMode: 1,
    aspectRatio: 16 / 9,
    preview: '.img-preview',
    crop: function crop(e) {
      $dataX.val(Math.round(e.x));
      $dataY.val(Math.round(e.y));
      $dataHeight.val(Math.round(e.height));
      $dataWidth.val(Math.round(e.width));
      $dataRotate.val(e.rotate);
      $dataScaleX.val(e.scaleX);
      $dataScaleY.val(e.scaleY);
    }
  };
  $image.cropper(options);
});