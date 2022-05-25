"use strict";

$(document).ready(function () {
  "use strict"; // https://www.jqueryscript.net/menu/Vertical-Responsive-Multi-level-Nav-Menu-with-jQuery-CSS3.html
  // $(document).ready(function() {
  //   $("body").bootstrapMaterialDesign();
  // });
  // Layout script

  var $appAdminWrap = $(".app-admin-wrap");
  var $sidebarPanel = $(".sidebar-panel");
  var $sidebarFullToggle = $(".sidebar-full-toggle");
  var $sidebarCompactToggle = $(".sidebar-compact-switch");
  var $mobileMenu = $(".mobile-menu-icon");
  var $mobileMenuCollapse = $(".switch-overlay"); // mobile-menu

  $mobileMenu.on("click", function () {
    mobileMenuClick();
  });
  $mobileMenuCollapse.on("click", function () {
    sidebarSwitchOverlay();
  });

  function mobileMenuClick() {
    // $appAdminWrap.toggleClass('sidebar-compact-onhover');
    $(".app-admin-wrap.sidebar-full .sidebar-panel").addClass('sidebar-switch-open');
  }

  function sidebarSwitchOverlay() {
    $(".app-admin-wrap.sidebar-full .sidebar-panel").removeClass('sidebar-switch-open');
  }

  $mobileMenuCollapse.on("click", function () {
    // $appAdminWrap.toggleClass('sidebar-compact-onhover');
    $(".app-admin-wrap.sidebar-full .main-content-wrap").removeClass("sidebar-full-z-index");
  });

  function openSidebarFull() {
    $appAdminWrap.removeClass("sidebar-closed");
    $appAdminWrap.addClass("sidebar-full");
  }

  function closeSidebarFull() {
    $appAdminWrap.addClass("sidebar-closed");
    $appAdminWrap.removeClass("sidebar-full");
  }

  function openSidebarCompact() {
    $appAdminWrap.addClass("sidebar-compact");
  }

  function closeSidebarCompact() {
    $appAdminWrap.removeClass("sidebar-compact");
  }

  function toggleOnHover() {
    closeSidebarCompact();
    $appAdminWrap.toggleClass('sidebar-compact-onhover');
    $(".app-admin-wrap.sidebar-full .main-content-wrap").toggleClass("sidebar-full-z-index");
  }

  $sidebarFullToggle.on("click", function () {
    $appAdminWrap.addClass("sidebar-full");
    $appAdminWrap.toggleClass("sidebar-closed");
  });
  $sidebarCompactToggle.on("click", function () {
    toggleOnHover();
  });
  $sidebarPanel.on('mouseenter', function (e) {
    if ($appAdminWrap.hasClass('sidebar-compact-onhover')) {
      closeSidebarCompact();
      openSidebarFull();
      $('ul.metismenu .item-name').show();
      $('.main-menu-title').show();
      $('.metismenu .has-arrow').removeClass('without-after-element');
    }
  }).on('mouseleave', function (e) {
    if ($appAdminWrap.hasClass('sidebar-compact-onhover')) {
      closeSidebarFull();
      openSidebarCompact();
      $('ul.metismenu .item-name').hide();
      $('.main-menu-title').hide();
      $('.metismenu .has-arrow').addClass('without-after-element');
    }
  }); // Sidebar dropdown

  var $dropdown = $(".dropdown");
  var $subMenu = $(".sub-menu");
  $dropdown.find('> a').on("click", function (e) {
    e.preventDefault();
    e.stopPropagation();
    $(this).parent('.dropdown').toggleClass("open");
  }); // Perfect scrollbar

  $(".perfect-scrollbar, [data-perfect-scrollbar]").each(function (index) {
    var $el = $(this);
    var ps = new PerfectScrollbar(this, {
      suppressScrollX: $el.data("suppress-scroll-x"),
      suppressScrollY: $el.data("suppress-scroll-y")
    });
  });
});