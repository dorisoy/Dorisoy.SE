"use strict";

$(document).ready(function () {
  var $appAdminWrap = $(".app-admin-wrap");
  var $html = $('html');
  var $customizer = $('.customizer');
  var $sidebarColor = $('.sidebar-colors a.color'); // Change sidebar color

  $sidebarColor.on('click', function (e) {
    e.preventDefault();
    $appAdminWrap.removeClass(function (index, className) {
      return (className.match(/(^|\s)sidebar-\S+/g) || []).join(' ');
    });
    $appAdminWrap.addClass($(this).data('sidebar-class'));
    $sidebarColor.removeClass('active');
    $(this).addClass('active');
  }); // Change Direction RTL/LTR

  $('#rtl-checkbox').change(function () {
    if (this.checked) {
      $html.attr('dir', 'rtl');
    } else {
      $html.attr('dir', 'ltr');
    }
  });
  var $themeLink = $('#gull-theme');
  initTheme('gull-theme');

  function initTheme(storageKey) {
    if (!localStorage) {
      return;
    }

    var fileUrl = localStorage.getItem(storageKey);

    if (fileUrl) {
      $themeLink.attr('href', fileUrl);
    }
  }

  $('.bootstrap-colors .color').on('click', function (e) {
    e.preventDefault();
    var color = $(this).attr('title');
    console.log(color);
    var fileUrl = '/assets/styles/css/themes/' + color + '.min.css';

    if (localStorage) {
      gullUtils.changeCssLink('gull-theme', fileUrl);
    } else {
      $themeLink.attr('href', fileUrl);
    }
  }); // Toggle customizer

  $('.handle').on('click', function (e) {
    $customizer.toggleClass('open');
  });
});