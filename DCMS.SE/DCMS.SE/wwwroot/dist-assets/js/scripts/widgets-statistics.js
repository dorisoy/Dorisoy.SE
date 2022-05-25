"use strict";

$(document).ready(function () {
  // var spark1 = {
  //     chart: {
  //         type: 'bar',
  //         height: 160,
  //         sparkline: {
  //             enabled: true
  //         },
  //     },
  //     stroke: {
  //         curve: 'curve'
  //     },
  //     fill: {
  //         opacity: 1,
  //     },
  //     series: [{
  //         data: [47, 45, 54, 38, 56, 24, 65, 31, 37, 39, 62, 51, 35, 41, 35, 27, 93, 53, 61, 27, 54, 43, 19, 46],
  //     }],
  //     yaxis: {
  //         min: 0
  //     },
  //     colors: ['#639'],
  // }
  // var spark1 = new ApexCharts(document.querySelector("#spark1"), spark1);
  // spark1.render();
  // basic area
  var bAOptions = {
    chart: {
      height: 100,
      type: 'area',
      zoom: {
        enabled: false
      },
      sparkline: {
        enabled: true
      },
      toolbar: {
        show: false,
        tools: {
          download: false
        }
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'straight',
      width: 1
    },
    series: [{
      name: 'income 1',
      data: [31, 40, 28, 51, 42, 109, 100]
    }, {
      name: 'income 2',
      data: [11, 32, 45, 32, 34, 52, 41]
    }],
    colors: ['#6ed3cf', '#4caf50']
  };
  var chart = new ApexCharts(document.querySelector("#basicArea-chart"), bAOptions);
  chart.render(); // end basic area
  // basic area 2

  var bAOptions = {
    chart: {
      height: 100,
      type: 'area',
      zoom: {
        enabled: false
      },
      sparkline: {
        enabled: true
      },
      toolbar: {
        show: false,
        tools: {
          download: false
        }
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'smooth',
      width: 1
    },
    series: [{
      name: 'approve 1',
      data: [90, 40, 28, 51, 42, 109, 100]
    }, {
      name: 'approve 2',
      data: [11, 90, 45, 32, 34, 52, 41]
    }],
    colors: ['#6ed3cf', '#9068be']
  };
  var chart = new ApexCharts(document.querySelector("#basicArea-chart2"), bAOptions);
  chart.render(); // end basic area 2
  // basic area 3

  var bAOptions = {
    chart: {
      height: 100,
      type: 'area',
      zoom: {
        enabled: false
      },
      sparkline: {
        enabled: true
      },
      toolbar: {
        show: false,
        tools: {
          download: false
        }
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'smooth',
      width: 1
    },
    series: [{
      name: 'transaction 1',
      data: [11, 40, 28, 51, 42, 109, 100]
    }, {
      name: 'transaction 2',
      data: [100, 40, 45, 32, 34, 0, 41]
    }],
    colors: ['#fae596', '#3fb0ac']
  };
  var chart = new ApexCharts(document.querySelector("#basicArea-chart3"), bAOptions);
  chart.render(); // end basic area 3
  // basic area 4  not working

  var bAOptions = {
    chart: {
      height: 100,
      type: 'area',
      zoom: {
        enabled: false
      },
      sparkline: {
        enabled: true
      },
      toolbar: {
        show: false,
        tools: {
          download: false
        }
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'straight',
      width: 1
    },
    series: [{
      name: 'sales 1',
      data: [31, 40, 28, 51, 42, 109, 100]
    }, {
      name: 'sales 2',
      data: [11, 32, 90, 32, 34, 52, 41]
    }],
    colors: ['#f44336', '#e05038']
  };
  var chart = new ApexCharts(document.querySelector("#basicArea-chart4"), bAOptions);
  chart.render(); // end basic area 4
  // basic area 5

  var bAOptions = {
    chart: {
      height: 100,
      type: 'area',
      zoom: {
        enabled: false
      },
      sparkline: {
        enabled: true
      },
      toolbar: {
        show: false,
        tools: {
          download: false
        }
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'straight',
      width: 1
    },
    series: [{
      name: 'approve 1',
      data: [31, 40, 28, 51, 42, 109, 100]
    }],
    colors: ['#639', '#e97d23']
  };
  var chart = new ApexCharts(document.querySelector("#basicArea-chart5"), bAOptions);
  chart.render(); // end of basic area5
  // basic area 6

  var bAOptions = {
    chart: {
      height: 100,
      type: 'area',
      zoom: {
        enabled: false
      },
      sparkline: {
        enabled: true
      },
      toolbar: {
        show: false,
        tools: {
          download: false
        }
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'straight',
      width: 1
    },
    series: [{
      name: 'Comments',
      data: [60, 20, 28, 51, 15, 40, 50]
    }],
    colors: ['#98dafc']
  };
  var chart = new ApexCharts(document.querySelector("#basicArea-chart6"), bAOptions);
  chart.render(); // end of basic area6
  // basic area 7

  var bAOptions = {
    chart: {
      height: 100,
      type: 'area',
      zoom: {
        enabled: false
      },
      sparkline: {
        enabled: true
      },
      toolbar: {
        show: false,
        tools: {
          download: false
        }
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'straight',
      width: 1
    },
    series: [{
      name: 'Contribution',
      data: [30, 20, 90, 2, 15, 40, 50]
    }],
    colors: ['#bcd5d1']
  };
  var chart = new ApexCharts(document.querySelector("#basicArea-chart7"), bAOptions);
  chart.render(); // end of basic area7
  // basic area 8

  var bAOptions = {
    chart: {
      height: 100,
      type: 'area',
      zoom: {
        enabled: false
      },
      sparkline: {
        enabled: true
      },
      toolbar: {
        show: false,
        tools: {
          download: false
        }
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'straight',
      width: 1
    },
    series: [{
      name: 'Watch',
      data: [65, 90, 10, 45, 15, 75, 50]
    }],
    colors: ['#6534ff']
  };
  var chart = new ApexCharts(document.querySelector("#basicArea-chart8"), bAOptions);
  chart.render(); // end of basic area8
  // people---basic area 9

  var options6 = {
    chart: {
      type: 'bar',
      width: '70%',
      height: 150,
      sparkline: {
        enabled: true
      }
    },
    plotOptions: {
      bar: {
        columnWidth: '60%',
        endingShape: 'rounded'
      }
    },
    colors: ['#08e5e8'],
    series: [{
      data: [12, 14, 2, 47, 42, 15, 47, 75, 65, 19, 14, 30, 50]
    }],
    labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13],
    xaxis: {
      crosshairs: {
        width: 1
      }
    },
    tooltip: {
      fixed: {
        enabled: false
      },
      x: {
        show: false
      },
      y: {
        title: {
          formatter: function formatter(seriesName) {
            return '';
          }
        }
      },
      marker: {
        show: false
      }
    }
  };
  new ApexCharts(document.querySelector("#chart6"), options6).render(); // Multiple Radial Bar--use 10

  var options = {
    chart: {
      height: 150,
      width: '60%',
      type: 'radialBar'
    },
    plotOptions: {
      radialBar: {
        startAngle: -90,
        endAngle: 90,
        track: {
          background: "#e7e7e7",
          strokeWidth: '100%',
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
            offsetY: 1,
            fontSize: '10px'
          }
        }
      }
    },
    stroke: {
      curve: 'smooth',
      lineCap: 'round'
    },
    lebels: {
      show: false
    },
    series: [75],
    // labels: ['Apples', 'Oranges', 'Bananas', 'Berries'],
    labels: ['Average Results']
  };
  var chart = new ApexCharts(document.querySelector("#multipleRadialBar"), options);
  chart.render(); // views---basic area 9

  var options6 = {
    chart: {
      type: 'bar',
      width: '70%',
      height: 150,
      sparkline: {
        enabled: true
      }
    },
    plotOptions: {
      bar: {
        columnWidth: '60%'
      }
    },
    colors: ['#e05038'],
    series: [{
      data: [12, 14, 2, 47, -42, -15, 47, 75, 65, 19, -14, 30, 50]
    }],
    labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13],
    xaxis: {
      crosshairs: {
        width: 1
      }
    },
    tooltip: {
      fixed: {
        enabled: false
      },
      x: {
        show: false
      },
      y: {
        title: {
          formatter: function formatter(seriesName) {
            return '';
          }
        }
      },
      marker: {
        show: false
      }
    }
  };
  new ApexCharts(document.querySelector("#chart6-2"), options6).render(); //growth

  var options1 = {
    chart: {
      type: 'line',
      width: '100%',
      height: 150,
      sparkline: {
        enabled: true
      }
    },
    series: [{
      data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54]
    }],
    tooltip: {
      fixed: {
        enabled: false
      },
      x: {
        show: false
      },
      y: {
        title: {
          formatter: function formatter(seriesName) {
            return '';
          }
        }
      },
      marker: {
        show: false
      }
    }
  };
  new ApexCharts(document.querySelector("#chart1"), options1).render(); // chart no 13

  var options6 = {
    chart: {
      type: 'bar',
      width: '70%',
      height: 100,
      sparkline: {
        enabled: true
      }
    },
    plotOptions: {
      bar: {
        columnWidth: '30%'
      }
    },
    colors: ['#c43235'],
    series: [{
      data: [5, 15, 25, 35, 55, 75, 100, 75, 55, 35, 25, 15, 5]
    }],
    labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13],
    xaxis: {
      crosshairs: {
        width: 1
      }
    },
    tooltip: {
      fixed: {
        enabled: false
      },
      x: {
        show: false
      },
      y: {
        title: {
          formatter: function formatter(seriesName) {
            return '';
          }
        }
      },
      marker: {
        show: false
      }
    }
  };
  new ApexCharts(document.querySelector("#chart13"), options6).render(); // chart no 14

  var options6 = {
    chart: {
      type: 'bar',
      width: '70%',
      height: 100,
      sparkline: {
        enabled: true
      }
    },
    plotOptions: {
      bar: {
        columnWidth: '30%'
      }
    },
    colors: ['#6534ff'],
    series: [{
      data: [5, 15, 15, 35, 35, 75, 90, 75, 35, 35, 15, 15, 5]
    }],
    labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13],
    xaxis: {
      crosshairs: {
        width: 1
      }
    },
    tooltip: {
      fixed: {
        enabled: false
      },
      x: {
        show: false
      },
      y: {
        title: {
          formatter: function formatter(seriesName) {
            return '';
          }
        }
      },
      marker: {
        show: false
      }
    }
  };
  new ApexCharts(document.querySelector("#chart14"), options6).render(); // chart no 15

  var options6 = {
    chart: {
      type: 'bar',
      width: '70%',
      height: 100,
      sparkline: {
        enabled: true
      }
    },
    plotOptions: {
      bar: {
        columnWidth: '30%'
      }
    },
    colors: ['#e6af4b'],
    series: [{
      data: [5, 15, 25, 35, 25, 15, 5, 10, 20, 30, 45, 65, 45, 30, 20, 10]
    }],
    labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16],
    xaxis: {
      crosshairs: {
        width: 1
      }
    },
    tooltip: {
      fixed: {
        enabled: false
      },
      x: {
        show: false
      },
      y: {
        title: {
          formatter: function formatter(seriesName) {
            return '';
          }
        }
      },
      marker: {
        show: false
      }
    }
  };
  new ApexCharts(document.querySelector("#chart15"), options6).render(); // chart no 16

  var options6 = {
    chart: {
      type: 'bar',
      width: '70%',
      height: 100,
      sparkline: {
        enabled: true
      }
    },
    plotOptions: {
      bar: {
        columnWidth: '30%'
      }
    },
    colors: ['#259b24'],
    series: [{
      data: [100, 85, 75, 55, 35, 25, 15, 5, 15, 25, 35, 55, 75, 85, 90, 100]
    }],
    labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16],
    xaxis: {
      crosshairs: {
        width: 1
      }
    },
    tooltip: {
      fixed: {
        enabled: false
      },
      x: {
        show: false
      },
      y: {
        title: {
          formatter: function formatter(seriesName) {
            return '';
          }
        }
      },
      marker: {
        show: false
      }
    }
  };
  new ApexCharts(document.querySelector("#chart16"), options6).render(); // simple radialBar chart no 17
  // Gradiant Radial Bar

  var options = {
    chart: {
      height: 250,
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
            offsetY: 1,
            show: true,
            color: '#888',
            fontSize: '15px'
          },
          value: {
            formatter: function formatter(val) {
              return parseInt(val);
            },
            color: '#111',
            fontSize: '12px',
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
    labels: ['Choice']
  };
  var chart = new ApexCharts(document.querySelector("#gradientRadial"), options);
  chart.render(); // simple radial bar chart no 18

  var options = {
    chart: {
      height: 250,
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
            offsetY: 1,
            show: true,
            color: '#888',
            fontSize: '15px'
          },
          value: {
            formatter: function formatter(val) {
              return parseInt(val);
            },
            color: '#111',
            fontSize: '12px',
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
        gradientToColors: ['#e6af4b'],
        inverseColors: true,
        opacityFrom: 1,
        opacityTo: 1,
        stops: [0, 100]
      }
    },
    series: [55],
    stroke: {
      lineCap: 'round'
    },
    labels: ['Pending']
  };
  var chart = new ApexCharts(document.querySelector("#gradientRadial2"), options);
  chart.render(); // simple radial bar chart no 19

  var options = {
    chart: {
      height: 250,
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
            offsetY: 1,
            show: true,
            color: '#888',
            fontSize: '15px'
          },
          value: {
            formatter: function formatter(val) {
              return parseInt(val);
            },
            color: '#111',
            fontSize: '12px',
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
        gradientToColors: ['#e05038'],
        inverseColors: true,
        opacityFrom: 1,
        opacityTo: 1,
        stops: [0, 100]
      }
    },
    series: [12],
    stroke: {
      lineCap: 'round'
    },
    labels: ['Profit']
  };
  var chart = new ApexCharts(document.querySelector("#gradientRadial3"), options);
  chart.render(); // simple radia bar chart no 20

  var options = {
    chart: {
      height: 250,
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
            offsetY: 1,
            show: true,
            color: '#888',
            fontSize: '15px'
          },
          value: {
            formatter: function formatter(val) {
              return parseInt(val);
            },
            color: '#111',
            fontSize: '12px',
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
        gradientToColors: ['#c0dfd9'],
        inverseColors: true,
        opacityFrom: 1,
        opacityTo: 1,
        stops: [0, 100]
      }
    },
    series: [100],
    stroke: {
      lineCap: 'round'
    },
    labels: ['Revenue']
  };
  var chart = new ApexCharts(document.querySelector("#gradientRadial4"), options);
  chart.render(); // basic area chart no 21

  var bAOptions = {
    chart: {
      height: 100,
      type: 'area',
      zoom: {
        enabled: false
      },
      sparkline: {
        enabled: true
      },
      toolbar: {
        show: false,
        tools: {
          download: false
        }
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'smooth',
      width: 1
    },
    series: [{
      name: 'Orders',
      data: [10, 20, 28, 51, 15, 40, 20]
    }],
    colors: ['#e62739']
  };
  var chart = new ApexCharts(document.querySelector("#basicArea-chart21"), bAOptions);
  chart.render(); // basic area chart no 22

  var bAOptions = {
    chart: {
      height: 100,
      type: 'area',
      zoom: {
        enabled: false
      },
      sparkline: {
        enabled: true
      },
      toolbar: {
        show: false,
        tools: {
          download: false
        }
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'smooth',
      width: 1
    },
    series: [{
      name: 'Marketing',
      data: [50, 20, 0, 51, 15, 30, 70]
    }],
    colors: ['#ffeb3b']
  };
  var chart = new ApexCharts(document.querySelector("#basicArea-chart22"), bAOptions);
  chart.render(); // basic area chart no 23

  var bAOptions = {
    chart: {
      height: 100,
      type: 'area',
      zoom: {
        enabled: false
      },
      sparkline: {
        enabled: true
      },
      toolbar: {
        show: false,
        tools: {
          download: false
        }
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'smooth',
      width: 1
    },
    series: [{
      name: 'Earning',
      data: [100, 20, 20, 51, 35, 10, 70]
    }],
    colors: ['#4caf50']
  };
  var chart = new ApexCharts(document.querySelector("#basicArea-chart23"), bAOptions);
  chart.render();
});