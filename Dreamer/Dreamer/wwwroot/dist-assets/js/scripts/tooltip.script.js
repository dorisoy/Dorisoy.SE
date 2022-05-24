"use strict";

$(document).ready(function () {
  $(function () {
    $('[data-toggle="tooltip"]').tooltip();
  });
  $('#manual').on('click', function () {
    $(this).tooltip('toggle');
  });
});