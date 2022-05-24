"use strict";

$(document).ready(function () {
  $('#order-datepicker').pickadate();
  $('.print-invoice').on('click', function () {
    window.print();
  });
});