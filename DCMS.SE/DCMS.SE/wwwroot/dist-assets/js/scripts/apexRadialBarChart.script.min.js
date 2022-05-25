"use strict";

$(document).ready(function () {
  // Basic Radial Bar Chart
  var options = {
    chart: {
      height: 350,
      type: 'radialBar'
    },
    plotOptions: {
      radialBar: {
        hollow: {
          size: '70%'
        },
        dataLabels: {
          showOn: 'always'
        }
      }
    },
    series: [70],
    labels: ['Cricket']
  };
  var chart = new ApexCharts(document.querySelector("#simpleRadialBar"), options);
  chart.render(); // Multiple Radial Bar

  var options = {
    chart: {
      height: 350,
      type: 'radialBar'
    },
    plotOptions: {
      radialBar: {
        dataLabels: {
          name: {
            fontSize: '22px'
          },
          value: {
            fontSize: '16px'
          },
          total: {
            show: true,
            label: 'Total',
            formatter: function formatter(w) {
              // By default this function returns the average of all series. The below is just an example to show the use of custom formatter function
              return 249;
            }
          }
        },
        endingShape: 'rounded'
      }
    },
    stroke: {
      curve: 'smooth',
      lineCap: 'round'
    },
    series: [44, 55, 67, 83],
    labels: ['Apples', 'Oranges', 'Bananas', 'Berries']
  };
  var chart = new ApexCharts(document.querySelector("#multipleRadialBar"), options);
  chart.render(); // Custom Angle Circle Chart

  var options = {
    chart: {
      height: 350,
      type: 'radialBar'
    },
    plotOptions: {
      radialBar: {
        offsetY: -30,
        startAngle: 0,
        endAngle: 270,
        hollow: {
          margin: 5,
          size: '30%',
          background: 'transparent',
          image: undefined
        },
        dataLabels: {
          name: {
            show: true
          },
          value: {
            show: false
          }
        }
      }
    },
    colors: ['#1ab7ea', '#0084ff', '#39539E', '#0077B5'],
    series: [76, 67, 61, 90],
    labels: ['Vimeo', 'Messenger', 'Facebook', 'LinkedIn'],
    legend: {
      show: true,
      floating: true,
      fontSize: '16px',
      position: 'bottom',
      // offsetX: 10,
      offsetY: 10,
      labels: {
        useSeriesColors: true
      },
      markers: {
        size: 0
      },
      formatter: function formatter(seriesName, opts) {
        return seriesName + ":  " + opts.w.globals.series[opts.seriesIndex];
      },
      itemMargin: {
        horizontal: 1
      }
    },
    responsive: [{
      breakpoint: 480,
      options: {
        legend: {
          show: false
        }
      }
    }]
  };
  var chart = new ApexCharts(document.querySelector("#customAngleCircleChart"), options);
  chart.render(); // Gradiant Radial Bar

  var options = {
    chart: {
      height: 350,
      type: 'radialBar',
      toolbar: {
        show: true
      }
    },
    plotOptions: {
      radialBar: {
        startAngle: -135,
        endAngle: 225,
        hollow: {
          margin: 0,
          size: '70%',
          background: '#fff',
          image: undefined,
          imageOffsetX: 0,
          imageOffsetY: 0,
          position: 'front',
          dropShadow: {
            enabled: true,
            top: 3,
            left: 0,
            blur: 4,
            opacity: 0.24
          }
        },
        track: {
          background: '#fff',
          strokeWidth: '67%',
          margin: 0,
          // margin is in pixels
          dropShadow: {
            enabled: true,
            top: -3,
            left: 0,
            blur: 4,
            opacity: 0.35
          }
        },
        dataLabels: {
          showOn: 'always',
          name: {
            offsetY: -10,
            show: true,
            color: '#888',
            fontSize: '17px'
          },
          value: {
            formatter: function formatter(val) {
              return parseInt(val);
            },
            color: '#111',
            fontSize: '20px',
            show: true
          }
        }
      }
    },
    fill: {
      type: 'gradient',
      gradient: {
        shade: 'dark',
        type: 'horizontal',
        shadeIntensity: 0.5,
        gradientToColors: ['#ABE5A1'],
        inverseColors: true,
        opacityFrom: 1,
        opacityTo: 1,
        stops: [0, 100]
      }
    },
    series: [75],
    stroke: {
      lineCap: 'round'
    },
    labels: ['Percent']
  };
  var chart = new ApexCharts(document.querySelector("#gradientRadial"), options);
  chart.render(); // Radialbars with Image

  var options = {
    chart: {
      height: 350,
      type: 'radialBar'
    },
    plotOptions: {
      radialBar: {
        hollow: {
          margin: 15,
          size: '70%',
          image: '../../assets/images/products/watch-1.jpg',
          imageWidth: 64,
          imageHeight: 64,
          imageClipped: false
        },
        dataLabels: {
          name: {
            show: false,
            color: '#fff'
          },
          value: {
            show: true,
            color: '#333',
            offsetY: 50,
            fontSize: '22px'
          }
        }
      }
    },
    fill: {
      type: 'image',
      image: {
        src: ['../../assets/images/products/watch-2.jpg']
      }
    },
    series: [67],
    stroke: {
      lineCap: 'round'
    },
    labels: ['Volatility']
  };
  var chart = new ApexCharts(document.querySelector("#radialbarswithImage"), options);
  chart.render(); // Stroked Angular Gauge

  var options = {
    chart: {
      height: 350,
      type: 'radialBar'
    },
    plotOptions: {
      radialBar: {
        startAngle: -135,
        endAngle: 135,
        dataLabels: {
          name: {
            fontSize: '16px',
            color: undefined,
            offsetY: 120
          },
          value: {
            offsetY: 76,
            fontSize: '22px',
            color: undefined,
            formatter: function formatter(val) {
              return val + "%";
            }
          }
        }
      }
    },
    fill: {
      type: 'gradient',
      gradient: {
        shade: 'dark',
        shadeIntensity: 0.15,
        inverseColors: false,
        opacityFrom: 1,
        opacityTo: 1,
        stops: [0, 50, 65, 91]
      }
    },
    stroke: {
      dashArray: 4
    },
    series: [67],
    labels: ['Median Ratio']
  };
  var chart = new ApexCharts(document.querySelector("#strokedangularGauge"), options);
  chart.render(); // window.setInterval(function () {
  //     chart.updateSeries([Math.floor(Math.random() * (100 - 1 + 1)) + 1])
  // }, 2000)
  // Semi Circle Gauge

  var options = {
    chart: {
      type: 'radialBar'
    },
    plotOptions: {
      radialBar: {
        startAngle: -90,
        endAngle: 90,
        track: {
          background: "#e7e7e7",
          strokeWidth: '97%',
          margin: 5,
          // margin is in pixels
          shadow: {
            enabled: true,
            top: 2,
            left: 0,
            color: '#999',
            opacity: 1,
            blur: 2
          }
        },
        dataLabels: {
          name: {
            show: false
          },
          value: {
            offsetY: 15,
            fontSize: '22px'
          }
        }
      }
    },
    fill: {
      type: 'gradient',
      gradient: {
        shade: 'light',
        shadeIntensity: 0.4,
        inverseColors: false,
        opacityFrom: 1,
        opacityTo: 1,
        stops: [0, 50, 53, 91]
      }
    },
    series: [76],
    labels: ['Average Results']
  };
  var chart = new ApexCharts(document.querySelector("#semiCircleGauge"), options);
  chart.render();
});