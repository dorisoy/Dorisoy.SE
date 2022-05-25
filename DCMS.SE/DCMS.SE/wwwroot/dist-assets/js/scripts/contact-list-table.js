"use strict";

$(document).ready(function () {
  "use strict"; // https://www.jqueryscript.net/menu/Vertical-Responsive-Multi-level-Nav-Menu-with-jQuery-CSS3.html
  // $(document).ready(function() {
  //   $("body").bootstrapMaterialDesign();
  // });
  // Layout script

  var $appAdminWrap = $(".app-admin-wrap");
  var $mobileIcon = $(".ul-contact-mobile-icon");
  var $childOpenMenu = $(".ul-contact-left-side");
  var $childCloseMenu = $(".contact-close-mobile-icon");
  $mobileIcon.on("click", function () {
    $childOpenMenu.addClass('contact-open');
  });
  $childCloseMenu.on("click", function () {
    $childOpenMenu.removeClass('contact-open');
  });
});