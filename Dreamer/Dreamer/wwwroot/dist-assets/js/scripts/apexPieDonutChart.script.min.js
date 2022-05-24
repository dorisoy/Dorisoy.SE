"use strict";

$(document).ready(function () {
  // simple Pie
  var options = {
    chart: {
      width: '100%',
      type: 'pie'
    },
    labels: ['Team A', 'Team B', 'Team C', 'Team D', 'Team E'],
    series: [44, 55, 13, 43, 22],
    legend: {
      position: 'bottom'
    },
    responsive: [{
      breakpoint: 480,
      options: {
        chart: {
          width: 300
        },
        legend: {
          position: 'bottom',
          offsetY: 40
        }
      }
    }]
  };
  var chart = new ApexCharts(document.querySelector("#simplePie"), options);
  chart.render(); // simple donut

  var options = {
    chart: {
      type: 'donut',
      width: '100%'
    },
    series: [44, 55, 41, 17, 15],
    legend: {
      position: 'bottom'
    },
    responsive: [{
      breakpoint: 480,
      options: {
        chart: {
          width: 310
        },
        legend: {
          position: 'bottom'
        }
      }
    }]
  };
  var chart = new ApexCharts(document.querySelector("#simpleDonut"), options);
  chart.render(); // monochromePie

  var options = {
    chart: {
      width: '100%',
      type: 'pie' // width: 450

    },
    series: [25, 15, 44, 55, 41, 17],
    labels: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
    theme: {
      monochrome: {
        enabled: true
      }
    },
    //  title: {
    //      text: ""
    //  },
    legend: {
      position: 'bottom'
    },
    responsive: [{
      breakpoint: 480,
      options: {
        chart: {
          width: 310
        },
        legend: {
          position: 'bottom'
        }
      }
    }]
  };
  var chart = new ApexCharts(document.querySelector("#monochromePie"), options);
  chart.render(); // gradient Donut

  var options = {
    chart: {
      width: '100%',
      type: 'donut'
    },
    dataLabels: {
      enabled: false
    },
    series: [44, 55, 41, 17, 15],
    fill: {
      type: 'gradient'
    },
    legend: {
      formatter: function formatter(val, opts) {
        return val + " - " + opts.w.globals.series[opts.seriesIndex];
      },
      position: 'bottom'
    },
    responsive: [{
      breakpoint: 480,
      options: {
        chart: {
          width: 310
        },
        legend: {
          position: 'bottom'
        }
      }
    }]
  };
  var chart = new ApexCharts(document.querySelector("#gradientDonut"), options);
  chart.render();
  var paper = chart.paper(); // donut with Pattern

  var options = {
    chart: {
      width: '100%',
      type: 'donut',
      dropShadow: {
        enabled: true,
        color: '#111',
        top: -1,
        left: 3,
        blur: 3,
        opacity: 0.2
      }
    },
    stroke: {
      width: 0
    },
    series: [44, 55, 41, 17, 15],
    labels: ["Comedy", "Action", "SciFi", "Drama", "Horror"],
    dataLabels: {
      dropShadow: {
        blur: 3,
        opacity: 0.8
      }
    },
    fill: {
      type: 'pattern',
      opacity: 1,
      pattern: {
        enabled: true,
        style: ['verticalLines', 'squares', 'horizontalLines', 'circles', 'slantedLines']
      }
    },
    states: {
      hover: {
        enabled: false
      }
    },
    theme: {
      palette: 'palette2'
    },
    //   title: {
    //       text: ""
    //   },
    legend: {
      position: 'bottom'
    },
    responsive: [{
      breakpoint: 480,
      options: {
        chart: {
          width: 310
        },
        legend: {
          position: 'bottom'
        }
      }
    }]
  };
  var chart = new ApexCharts(document.querySelector("#donutwithPattern"), options);
  chart.render(); // pie With Image

  var options = {
    chart: {
      width: '100%',
      type: 'pie'
    },
    colors: ['#93C3EE', '#E5C6A0', '#669DB5', '#94A74A'],
    series: [44, 33, 54, 45],
    fill: {
      type: 'image',
      opacity: 0.85,
      image: {
        src: ['../../dist-assets/images/products/headphone-1.jpg', '../../dist-assets/images/products/iphone-1.jpg', '../../dist-assets/images/products/speaker-2.jpg', '../../dist-assets/images/products/watch-1.jpg'],
        width: 25,
        imagedHeight: 25
      }
    },
    stroke: {
      width: 4
    },
    dataLabels: {
      enabled: false
    },
    legend: {
      position: 'bottom'
    },
    responsive: [{
      breakpoint: 480,
      options: {
        chart: {
          width: 310
        },
        legend: {
          position: 'bottom'
        }
      }
    }]
  };
  var chart = new ApexCharts(document.querySelector("#piewithImage"), options);
  chart.render();
});