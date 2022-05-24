"use strict";

$(document).ready(function () {
  $('.menu-toggle').on('click', function (e) {
    e.preventDefault();
    $('.header-topnav').toggleClass('open');
  });
});