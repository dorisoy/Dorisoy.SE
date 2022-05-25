"use strict";

$(document).ready(function () {
  // Toastr types
  $("#toast-success").on("click", function () {
    toastr.success("toastr is a Javascript library for non-blocking notifications. jQuery is required!", "Miracle Max Says", {
      timeOut: "50000"
    });
  }), $("#toast-info").on("click", function () {
    toastr.info("We do have the Kapua suite available.", "Turtle Bay Resort");
  }), $("#toast-warning").on("click", function () {
    toastr.warning("My name is Inigo Montoya. You killed my father, prepare to die!");
  }), $("#toast-error").on("click", function () {
    toastr.error("toastr is a Javascript library for non-blocking notifications. jQuery is required.", "Inconceivable!");
  }), // top position
  $("#toast-position-top-left").on("click", function () {
    toastr.info("toastr is a Javascript library for non-blocking notifications. jQuery is required.", "Top Left!", {
      positionClass: "toast-top-left",
      containerId: "toast-top-left",
      timeOut: "50000"
    });
  }), $("#toast-position-top-center").on("click", function () {
    toastr.info("toastr is a Javascript library for non-blocking notifications. jQuery is required.", "Top Center!", {
      positionClass: "toast-top-center",
      containerId: "toast-top-center"
    });
  }), $("#toast-position-top-right").on("click", function () {
    toastr.info("toastr is a Javascript library for non-blocking notifications. jQuery is required.", "Top Right!", {
      positionClass: "toast-top-right",
      containerId: "toast-top-right"
    });
  }), $("#toast-position-top-full").on("click", function () {
    toastr.info("toastr is a Javascript library for non-blocking notifications. jQuery is required.", "Top Full Width!", {
      positionClass: "toast-top-full-width",
      containerId: "toast-top-full-width"
    });
  }), // toast bottom position
  $("#toast-position-bottom-left").on("click", function () {
    toastr.info("toastr is a Javascript library for non-blocking notifications. jQuery is required.", "Bottom Left!", {
      positionClass: "toast-bottom-left",
      containerId: "toast-bottom-left"
    });
  }), $("#toast-position-bottom-center").on("click", function () {
    toastr.info("toastr is a Javascript library for non-blocking notifications. jQuery is required.", "Bottom Center!", {
      positionClass: "toast-bottom-center",
      containerId: "toast-bottom-center"
    });
  }), $("#toast-position-bottom-right").on("click", function () {
    toastr.info("toastr is a Javascript library for non-blocking notifications. jQuery is required.", "Bottom Right!", {
      positionClass: "toast-bottom-right",
      containerId: "toast-bottom-right"
    });
  }), $("#toast-position-bottom-full").on("click", function () {
    toastr.info("toastr is a Javascript library for non-blocking notifications. jQuery is required.", "Bottom Full Width!", {
      positionClass: "toast-bottom-full-width",
      containerId: "toast-bottom-full-width"
    }); // options
  }), $("#toast-text-notification").on("click", function () {
    toastr.info("toastr is a Javascript library for non-blocking notifications. jQuery is required!", "Miracle Max Says");
  }), $("#toast-close-button").on("click", function () {
    toastr.success("toastr is a Javascript library for non-blocking notifications. jQuery is required!", "With Close Button", {
      closeButton: !0
    });
  }), $("#toast-progress-bar").on("click", function () {
    toastr.warning("toastr is a Javascript library for non-blocking notifications. jQuery is required!", "Progress Bar", {
      progressBar: !0
    });
  }), $("#toast-clear-btn").on("click", function () {
    toastr.error('Clear itself?<br /><br /><button type="button" class="btn btn-secondary clear">Yes</button>', "Clear Toast Button");
  }), // show and remove toaser
  $("#toast-show-remove").on("click", function () {
    toastr.info("toastr is a Javascript library for non-blocking notifications. jQuery is required!", "Miracle Max Says");
  }), $("#toast-remove").on("click", function () {
    toastr.remove();
  }), $("#toast-show-clear").on("click", function () {
    toastr.info("toastr is a Javascript library for non-blocking notifications. jQuery is required!", "Miracle Max Says");
  }), $("#toast-clear").on("click", function () {
    toastr.clear();
  }), // Duration
  $("#toast-fast-duration").on("click", function () {
    toastr.success("toastr is a Javascript library for non-blocking notifications. jQuery is required!", "Fast Duration", {
      showDuration: 500
    });
  }), $("#toast-slow-duration").on("click", function () {
    toastr.warning("toastr is a Javascript library for non-blocking notifications. jQuery is required!", "Slow Duration", {
      hideDuration: 3000
    });
  }), $("#toast-timeout").on("click", function () {
    toastr.error("toastr is a Javascript library for non-blocking notifications. jQuery is required.", "Timeout!", {
      timeOut: 6000
    });
  }), $("#toast-sticky").on("click", function () {
    toastr.info("toastr is a Javascript library for non-blocking notifications. jQuery is required.", "Sticky!", {
      timeOut: 0
    });
  }), // slideup/fadein/Out
  $("#toast-slide").on("click", function () {
    toastr.success("toastr is a Javascript library for non-blocking notifications. jQuery is required.", "Slide Down / Slide Up!", {
      showMethod: "slideDown",
      hideMethod: "slideUp",
      timeOut: 2e3
    });
  }), $("#toast-fade").on("click", function () {
    toastr.success("toastr is a Javascript library for non-blocking notifications. jQuery is required.", "Slide Down / Slide Up!", {
      showMethod: "fadeIn",
      hideMethod: "fadeOut",
      timeOut: 2e3
    });
  });
});