"use strict";

$(document).ready(function () {
  // Collapsable Sidebar
  var $sidebarContainer = $('[data-sidebar-container]').addClass('sidebar-container');
  var $sidebarContent = $('[data-sidebar-content]').addClass('sidebar-content');
  var $sidebar = $('[data-sidebar]').addClass('sidebar');
  $sidebarContainer.each(function (index) {
    var $container = $(this);
    var sidebarName;
    var $content;
    var $sb;
    var $toggle;
    var sidebarWidth;
    var sidebarPosition;

    function initSidebar() {
      sidebarName = $container.data('sidebar-container');
      $content = $('[data-sidebar-content="' + sidebarName + '"]');
      $sb = $('[data-sidebar="' + sidebarName + '"]');
      $toggle = $('[data-sidebar-toggle="' + sidebarName + '"]');
      sidebarWidth = $sb.outerWidth();
      sidebarPosition = $sb.data('sidebar-position'); // console.log('width:: ', $sb.outerWidth());

      if (sidebarPosition === 'right') {
        !gullUtils.isMobile() ? $content.css('margin-right', sidebarWidth) : $content.css('margin-right', 0);
        !gullUtils.isMobile() ? $sb.css('right', 0) : $sb.css('right', -sidebarWidth);
      } else {
        // console.log('width: ', sidebarWidth)
        !gullUtils.isMobile() ? $content.css('margin-left', sidebarWidth) : $content.css('margin-left', 0);
        !gullUtils.isMobile() ? $sb.css('left', 0) : $sb.css('left', -sidebarWidth);
      }
    }

    initSidebar();
    $(window).on("resize", function (event) {
      setTimeout(function () {
        initSidebar();
      }, 300);
    });
    $toggle.on('click', function (e) {
      if (sidebarPosition === 'right') {
        if ($sb.css('right') == '0px') {
          $sb.css('right', -sidebarWidth);
          !gullUtils.isMobile() ? $content.css('margin-right', 0) : null;
        } else {
          $sb.css('right', 0);
          !gullUtils.isMobile() ? $content.css('margin-right', sidebarWidth) : null;
        }
      } else {
        if ($sb.css('left') == '0px') {
          $sb.css('left', -sidebarWidth);
          !gullUtils.isMobile() ? $content.css('margin-left', 0) : null;
        } else {
          $sb.css('left', 0);
          !gullUtils.isMobile() ? $content.css('margin-left', sidebarWidth) : null;
        }
      }
    });
  });
});