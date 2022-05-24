"use strict";

$(document).ready(function () {
  // interval
  $('#carouselExampleInterval').carousel({
    interval: 2000
  }); // pause

  $('#carouselExamplePause').carousel({
    pause: "hover"
  }); // wrap

  $('#carouselExampleWrap').carousel({
    wrap: false
  }); // keyboard

  $('#carouselExampleKeyboard').carousel({
    keyboard: true
  });
});