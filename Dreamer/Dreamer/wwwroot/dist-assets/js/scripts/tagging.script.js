"use strict";

$(document).ready(function () {
  $(".tagBox").tagging();
  $(".forbidden-words").tagging({
    "forbidden-words": ["shit", "bloody"]
  });
});