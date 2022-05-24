"use strict";

$(document).ready(function () {
  $('.basic-ladda-button').on('click', function (e) {
    var laddaBtn = e.currentTarget;
    var l = Ladda.create(laddaBtn);
    l.start();
    setTimeout(function () {
      l.stop();
    }, 3000);
  });
  $('.example-button').on('click', function (e) {
    var laddaBtn = e.currentTarget;
    var l = Ladda.create(laddaBtn);
    l.start();
    setTimeout(function () {
      l.stop();
    }, 3000);
  });
});