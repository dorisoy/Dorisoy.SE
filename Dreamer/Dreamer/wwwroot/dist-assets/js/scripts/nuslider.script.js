"use strict";

$(document).ready(function () {
  // handle
  var handlesSlider = document.getElementById('slider-handles');
  noUiSlider.create(handlesSlider, {
    start: [4000, 8000],
    range: {
      'min': [2000],
      'max': [10000]
    }
  }); // more than two slider

  var handlesSlider4 = document.getElementById('slider-handles4');
  noUiSlider.create(handlesSlider4, {
    start: [4000, 8000, 12000, 16000],
    connect: [false, true, true, false, true],
    range: {
      'min': [2000],
      'max': [20000]
    }
  }); // range slider

  var rangeSlider = document.getElementById('slider-range');
  noUiSlider.create(rangeSlider, {
    start: [4000],
    range: {
      'min': [2000],
      'max': [10000]
    }
  }); // show value of range

  var rangeSliderValueElement = document.getElementById('slider-range-value');
  rangeSlider.noUiSlider.on('update', function (values, handle) {
    rangeSliderValueElement.innerHTML = values[handle];
  }); // step and snaping

  var stepSlider = document.getElementById('slider-step');
  noUiSlider.create(stepSlider, {
    start: [4000],
    step: 1000,
    range: {
      'min': [2000],
      'max': [10000]
    }
  }); // non lenear slider

  var nonLinearSlider = document.getElementById('slider-non-linear');
  noUiSlider.create(nonLinearSlider, {
    start: [4000],
    range: {
      'min': [2000],
      '30%': [4000],
      '70%': [8000],
      'max': [10000]
    }
  }); //value

  var nonLinearSliderValueElement = document.getElementById('slider-non-linear-value'); // Show the value for the *last* moved handle.

  nonLinearSlider.noUiSlider.on('update', function (values, handle) {
    nonLinearSliderValueElement.innerHTML = values[handle];
  }); // stepping in non lenear slider

  var nonLinearStepSlider = document.getElementById('slider-non-linear-step');
  noUiSlider.create(nonLinearStepSlider, {
    start: [500, 4000],
    range: {
      'min': [0],
      '10%': [500, 500],
      '50%': [4000, 1000],
      'max': [10000]
    }
  }); //value

  var nonLinearStepSliderValueElement = document.getElementById('slider-non-linear-step-value');
  nonLinearStepSlider.noUiSlider.on('update', function (values, handle) {
    nonLinearStepSliderValueElement.innerHTML = values[handle];
  }); // tap

  var tapSlider = document.getElementById('tap');
  noUiSlider.create(tapSlider, {
    start: 40,
    behaviour: 'tap',
    connect: [false, true],
    range: {
      'min': 20,
      'max': 80
    }
  }); // drag

  var dragSlider = document.getElementById('drag');
  noUiSlider.create(dragSlider, {
    start: [40, 60],
    behaviour: 'drag',
    connect: true,
    range: {
      'min': 20,
      'max': 80
    }
  }); // fixed-drag

  var dragFixedSlider = document.getElementById('drag-fixed');
  noUiSlider.create(dragFixedSlider, {
    start: [40, 60],
    behaviour: 'drag-fixed',
    connect: true,
    range: {
      'min': 20,
      'max': 80
    }
  }); // snap

  var snapSlider = document.getElementById('snap');
  noUiSlider.create(snapSlider, {
    start: 40,
    behaviour: 'snap',
    connect: [true, false],
    range: {
      'min': 20,
      'max': 80
    }
  }); // hover

  var hoverSlider = document.getElementById('hover');
  var field = document.getElementById('hover-val');
  noUiSlider.create(hoverSlider, {
    start: 20,
    behaviour: 'hover-snap',
    direction: 'rtl',
    range: {
      'min': 0,
      'max': 10
    }
  });
  hoverSlider.noUiSlider.on('hover', function (value) {
    field.innerHTML = value;
  }); // combined

  var dragTapSlider = document.getElementById('combined');
  noUiSlider.create(dragTapSlider, {
    start: [40, 60],
    behaviour: 'drag-tap',
    connect: true,
    range: {
      'min': 20,
      'max': 80
    }
  }); // scale/pipe demos

  var range_all_sliders = {
    'min': [0],
    '10%': [500, 500],
    '50%': [4000, 1000],
    'max': [10000]
  }; // pipe range left to right

  var pipsRange = document.getElementById('pips-range');
  noUiSlider.create(pipsRange, {
    range: range_all_sliders,
    start: 0,
    pips: {
      mode: 'range',
      density: 3
    }
  }); // right to left

  var pipsRangeRtl = document.getElementById('pips-range-rtl');
  noUiSlider.create(pipsRangeRtl, {
    range: range_all_sliders,
    start: 0,
    direction: 'rtl',
    pips: {
      mode: 'range',
      density: 3
    }
  }); // positions

  var pipsPositions = document.getElementById('pips-positions');
  noUiSlider.create(pipsPositions, {
    range: range_all_sliders,
    start: 0,
    pips: {
      mode: 'positions',
      values: [0, 25, 50, 75, 100],
      density: 4
    }
  }); // stepped positions

  var positionsStepped = document.getElementById('pips-positions-stepped');
  noUiSlider.create(positionsStepped, {
    range: range_all_sliders,
    start: 0,
    pips: {
      mode: 'positions',
      values: [0, 25, 50, 75, 100],
      density: 4,
      stepped: true
    }
  }); // count

  var pipsCount = document.getElementById('pips-count');
  noUiSlider.create(pipsCount, {
    range: range_all_sliders,
    start: 0,
    pips: {
      mode: 'count',
      values: 6,
      density: 4
    }
  }); // stepped count

  var pipsCountStepped = document.getElementById('pips-count-stepped');
  noUiSlider.create(pipsCountStepped, {
    range: range_all_sliders,
    start: 0,
    pips: {
      mode: 'count',
      values: 6,
      density: 4,
      stepped: true
    }
  }); // values

  var pipsValues = document.getElementById('pips-values');
  noUiSlider.create(pipsValues, {
    range: range_all_sliders,
    start: 0,
    pips: {
      mode: 'values',
      values: [50, 552, 2251, 3200, 5000, 7080, 9000],
      density: 4
    }
  }); // stepped values

  var pipsValuesStepped = document.getElementById('pips-values-stepped');
  noUiSlider.create(pipsValuesStepped, {
    range: range_all_sliders,
    start: 0,
    pips: {
      mode: 'values',
      values: [50, 552, 4651, 4952, 5000, 7080, 9000],
      density: 4,
      stepped: true
    }
  }); // primary color slider

  var primary = document.getElementById('primary-color-slider');
  noUiSlider.create(primary, {
    start: [20, 40],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // success color slider

  var success = document.getElementById('success-color-slider');
  noUiSlider.create(success, {
    start: [20, 40],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // info color slider

  var info = document.getElementById('info-color-slider');
  noUiSlider.create(info, {
    start: [20, 40],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // warning color slider

  var warning = document.getElementById('warning-color-slider');
  noUiSlider.create(warning, {
    start: [20, 40],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // danger color slider

  var danger = document.getElementById('danger-color-slider');
  noUiSlider.create(danger, {
    start: [20, 40],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // custom color slider

  var custom = document.getElementById('custom-color-slider');
  noUiSlider.create(custom, {
    start: [20, 40],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // sizing
  // custom color slider

  var extra_large = document.getElementById('extra-large-slider');
  noUiSlider.create(extra_large, {
    start: [20, 40],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // custom color slider

  var large_slider = document.getElementById('large-slider');
  noUiSlider.create(large_slider, {
    start: [20, 50],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // defualt-slider 

  var default_slider = document.getElementById('default-slider');
  noUiSlider.create(default_slider, {
    start: [20, 60],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // small-slider 

  var small_slider = document.getElementById('small-slider');
  noUiSlider.create(small_slider, {
    start: [20, 60],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // extra-small-slider 

  var extra_small_slider = document.getElementById('extra-small-slider');
  noUiSlider.create(extra_small_slider, {
    start: [20, 60],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // filed circle
  // custom color slider

  var extra_large_filled_circle = document.getElementById('extra-large-slider-circle-filled');
  noUiSlider.create(extra_large_filled_circle, {
    start: [20, 40],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // custom color slider

  var large_slider_circle_filled = document.getElementById('large-slider-circle-filled');
  noUiSlider.create(large_slider_circle_filled, {
    start: [20, 50],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // defualt-slider 

  var default_slider_circle_filled = document.getElementById('default-slider-circle-filled');
  noUiSlider.create(default_slider_circle_filled, {
    start: [20, 60],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // small-slider 

  var small_slider_circle_filled = document.getElementById('small-slider-circle-filled');
  noUiSlider.create(small_slider_circle_filled, {
    start: [20, 60],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // extra-small-slider 

  var extra_small_slider_circle_filled = document.getElementById('extra-small-slider-circle-filled');
  noUiSlider.create(extra_small_slider_circle_filled, {
    start: [20, 60],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // square default
  // extra large

  var extra_large_square_default = document.getElementById('extra-large-slider-square-default');
  noUiSlider.create(extra_large_square_default, {
    start: [20, 40],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // large

  var large_slider_square_default = document.getElementById('large-slider-square-default');
  noUiSlider.create(large_slider_square_default, {
    start: [20, 50],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // defualt-slider 

  var default_slider_square_default = document.getElementById('default-slider-square-default');
  noUiSlider.create(default_slider_square_default, {
    start: [20, 60],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // small-slider 

  var small_slider_square_default = document.getElementById('small-slider-square-default');
  noUiSlider.create(small_slider_square_default, {
    start: [20, 60],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // extra-small-slider 

  var extra_small_slider_square_default = document.getElementById('extra-small-slider-square-default');
  noUiSlider.create(extra_small_slider_square_default, {
    start: [20, 60],
    connect: true,
    range: {
      'min': 10,
      'max': 80
    }
  }); // vertical

  var verticalSlider1 = document.getElementById('slider-vertical-1');
  noUiSlider.create(verticalSlider1, {
    start: 10,
    orientation: 'vertical',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider2 = document.getElementById('slider-vertical-2');
  noUiSlider.create(verticalSlider2, {
    start: 20,
    orientation: 'vertical',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider3 = document.getElementById('slider-vertical-3');
  noUiSlider.create(verticalSlider3, {
    start: 30,
    orientation: 'vertical',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider4 = document.getElementById('slider-vertical-4');
  noUiSlider.create(verticalSlider4, {
    start: 40,
    orientation: 'vertical',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider5 = document.getElementById('slider-vertical-5');
  noUiSlider.create(verticalSlider5, {
    start: 50,
    orientation: 'vertical',
    range: {
      'min': 0,
      'max': 100
    }
  }); // connect to lower

  var verticalSlider_connectlower1 = document.getElementById('slider-vertical-connect-lower-1');
  noUiSlider.create(verticalSlider_connectlower1, {
    start: 0,
    connect: [true, false],
    orientation: 'vertical',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider_connectlower2 = document.getElementById('slider-vertical-connect-lower-2');
  noUiSlider.create(verticalSlider_connectlower2, {
    start: 10,
    connect: [true, false],
    orientation: 'vertical',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider_connectlower3 = document.getElementById('slider-vertical-connect-lower-3');
  noUiSlider.create(verticalSlider_connectlower3, {
    start: 20,
    connect: [true, false],
    orientation: 'vertical',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider_connectlower4 = document.getElementById('slider-vertical-connect-lower-4');
  noUiSlider.create(verticalSlider_connectlower4, {
    start: 30,
    connect: [true, false],
    orientation: 'vertical',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider_connectlower5 = document.getElementById('slider-vertical-connect-lower-5');
  noUiSlider.create(verticalSlider_connectlower5, {
    start: 40,
    connect: [true, false],
    orientation: 'vertical',
    range: {
      'min': 0,
      'max': 100
    }
  }); // connect upper

  var verticalSlider_connectupper1 = document.getElementById('slider-vertical-connect-upper-1');
  noUiSlider.create(verticalSlider_connectupper1, {
    start: 10,
    connect: [true, false],
    direction: 'rtl',
    orientation: 'vertical',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider_connectupper2 = document.getElementById('slider-vertical-connect-upper-2');
  noUiSlider.create(verticalSlider_connectupper2, {
    start: 30,
    connect: [true, false],
    direction: 'rtl',
    orientation: 'vertical',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider_connectupper3 = document.getElementById('slider-vertical-connect-upper-3');
  noUiSlider.create(verticalSlider_connectupper3, {
    start: 50,
    connect: [true, false],
    direction: 'rtl',
    orientation: 'vertical',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider_connectupper4 = document.getElementById('slider-vertical-connect-upper-4');
  noUiSlider.create(verticalSlider_connectupper4, {
    start: 70,
    connect: [true, false],
    direction: 'rtl',
    orientation: 'vertical',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider_connectupper5 = document.getElementById('slider-vertical-connect-upper-5');
  noUiSlider.create(verticalSlider_connectupper5, {
    start: 90,
    connect: [true, false],
    orientation: 'vertical',
    direction: 'rtl',
    range: {
      'min': 0,
      'max': 100
    }
  }); // tooltips

  var verticalSlider_tooltip1 = document.getElementById('slider-vertical-tooltip-1');
  noUiSlider.create(verticalSlider_tooltip1, {
    start: 10,
    connect: [true, false],
    orientation: 'vertical',
    tooltips: true,
    direction: 'rtl',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider_tooltip2 = document.getElementById('slider-vertical-tooltip-2');
  noUiSlider.create(verticalSlider_tooltip2, {
    start: 20,
    connect: [true, false],
    orientation: 'vertical',
    tooltips: true,
    direction: 'rtl',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider_tooltip3 = document.getElementById('slider-vertical-tooltip-3');
  noUiSlider.create(verticalSlider_tooltip3, {
    start: 40,
    connect: [true, false],
    orientation: 'vertical',
    tooltips: true,
    direction: 'rtl',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider_tooltip4 = document.getElementById('slider-vertical-tooltip-4');
  noUiSlider.create(verticalSlider_tooltip4, {
    start: 60,
    connect: [true, false],
    orientation: 'vertical',
    tooltips: true,
    direction: 'rtl',
    range: {
      'min': 0,
      'max': 100
    }
  });
  var verticalSlider_tooltip5 = document.getElementById('slider-vertical-tooltip-5');
  noUiSlider.create(verticalSlider_tooltip5, {
    start: 70,
    connect: [true, false],
    orientation: 'vertical',
    tooltips: true,
    direction: 'rtl',
    range: {
      'min': 0,
      'max': 100
    }
  });
});