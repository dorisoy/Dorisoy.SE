"use strict";

$(document).ready(function () {
  // basic Line Chart
  var options = {
    chart: {
      height: 350,
      type: 'line',
      zoom: {
        enabled: false
      },
      toolbar: {
        show: true
      }
    },
    tooltip: {
      enabled: true,
      shared: true,
      followCursor: false,
      intersect: false,
      inverseOrder: false,
      custom: undefined,
      fillSeriesColor: false,
      theme: false
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'smooth'
    },
    series: [{
      name: "Desktops",
      data: [10, 41, 35, 51, 49, 62, 69, 91, 148]
    }],
    grid: {
      row: {
        colors: ['#f3f3f3', 'transparent'],
        // takes an array which will be repeated on columns
        opacity: 0.5
      }
    },
    xaxis: {
      categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep']
    }
  };
  var chart = new ApexCharts(document.querySelector("#basicLine-chart"), options);
  chart.render(); // line chart with Data Label

  var options = {
    chart: {
      height: 350,
      type: 'line',
      shadow: {
        enabled: true,
        color: '#000',
        top: 18,
        left: 7,
        blur: 10,
        opacity: 1
      },
      toolbar: {
        show: false
      },
      animations: {
        enabled: true,
        easing: 'linear',
        speed: 500,
        animateGradually: {
          enabled: true,
          delay: 150
        },
        dynamicAnimation: {
          enabled: true,
          speed: 550
        }
      }
    },
    colors: ['#77B6EA', '#545454'],
    dataLabels: {
      enabled: true
    },
    stroke: {
      curve: 'smooth'
    },
    series: [{
      name: "High - 2013",
      data: [28, 29, 33, 36, 32, 32, 33]
    }, {
      name: "Low - 2013",
      data: [12, 11, 14, 18, 17, 13, 13]
    }],
    grid: {
      borderColor: '#e7e7e7',
      row: {
        colors: ['#f3f3f3', 'transparent'],
        // takes an array which will be repeated on columns
        opacity: 0.5
      }
    },
    markers: {
      size: 6
    },
    xaxis: {
      categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'],
      title: {
        text: 'Month'
      }
    },
    yaxis: {
      title: {
        text: 'Temperature'
      },
      min: 5,
      max: 40
    },
    legend: {
      position: 'top',
      horizontalAlign: 'right',
      floating: true,
      offsetY: -25,
      offsetX: -5
    }
  };
  var chart = new ApexCharts(document.querySelector("#lineChartWIthDataLabel"), options);
  chart.render(); // Zoomable timeseries line chart

  var ts2 = 1484418600000;
  var dates = [];
  var spikes = [5, -5, 3, -3, 8, -8];

  for (var i = 0; i < 120; i++) {
    ts2 = ts2 + 86400000;
    var innerArr = [ts2, dataSeries[1][i].value];
    dates.push(innerArr);
  }

  var options = {
    chart: {
      type: 'area',
      stacked: false,
      height: 350,
      zoom: {
        type: 'x',
        enabled: true
      },
      toolbar: {
        autoSelected: 'zoom'
      }
    },
    dataLabels: {
      enabled: false
    },
    series: [{
      name: 'XYZ MOTORS',
      data: dates
    }],
    markers: {
      size: 0
    },
    fill: {
      type: 'gradient',
      gradient: {
        shadeIntensity: 1,
        inverseColors: false,
        opacityFrom: 0.5,
        opacityTo: 0,
        stops: [0, 90, 100]
      }
    },
    yaxis: {
      min: 20000000,
      max: 250000000,
      labels: {
        formatter: function formatter(val) {
          return (val / 1000000).toFixed(0);
        }
      },
      title: {
        text: 'Price'
      }
    },
    xaxis: {
      type: 'datetime'
    },
    tooltip: {
      shared: false,
      y: {
        formatter: function formatter(val) {
          return (val / 1000000).toFixed(0);
        }
      }
    }
  };
  var chart = new ApexCharts(document.querySelector("#zoomableLine-chart"), options);
  chart.render(); // gradient line chart

  var options = {
    chart: {
      height: 350,
      type: 'line',
      dropShadow: {
        enabled: true,
        top: 3,
        left: 3,
        blur: 1,
        opacity: 0.2
      }
    },
    stroke: {
      width: 7,
      curve: 'smooth'
    },
    series: [{
      name: 'Likes',
      data: [4, 3, 10, 9, 29, 19, 22, 9, 12, 7, 19, 5, 13, 9, 17, 2, 7, 5]
    }],
    xaxis: {
      type: 'datetime',
      categories: ['1/11/2000', '2/11/2000', '3/11/2000', '4/11/2000', '5/11/2000', '6/11/2000', '7/11/2000', '8/11/2000', '9/11/2000', '10/11/2000', '11/11/2000', '12/11/2000', '1/11/2001', '2/11/2001', '3/11/2001', '4/11/2001', '5/11/2001', '6/11/2001']
    },
    fill: {
      type: 'gradient',
      gradient: {
        shade: 'dark',
        gradientToColors: ['#FDD835'],
        shadeIntensity: 1,
        type: 'horizontal',
        opacityFrom: 1,
        opacityTo: 1,
        stops: [0, 100, 100, 100]
      }
    },
    markers: {
      size: 4,
      opacity: 0.9,
      colors: ["#FFA41B"],
      strokeColor: "#fff",
      strokeWidth: 2,
      hover: {
        size: 7
      }
    },
    yaxis: {
      min: -10,
      max: 40,
      title: {
        text: 'Engagement'
      }
    }
  };
  var chart = new ApexCharts(document.querySelector("#gradientLineChart"), options);
  chart.render(); // Real time Line chart

  /*
  // this function will generate output in this format
  // data = [
      [timestamp, 23],
      [timestamp, 33],
      [timestamp, 12]
      ...
  ]
  */

  var lastDate = 0;
  var data = [];

  function getDayWiseTimeSeries(baseval, count, yrange) {
    var i = 0;

    while (i < count) {
      var x = baseval;
      var y = Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min;
      data.push({
        x: x,
        y: y
      });
      lastDate = baseval;
      baseval += 86400000;
      i++;
    }
  }

  getDayWiseTimeSeries(new Date('11 Feb 2017 GMT').getTime(), 10, {
    min: 10,
    max: 90
  });

  function getNewSeries(baseval, yrange) {
    var newDate = baseval + 86400000;
    lastDate = newDate;
    data.push({
      x: newDate,
      y: Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min
    });
  }

  function resetData() {
    data = data.slice(data.length - 10, data.length);
  }

  var options = {
    chart: {
      height: 350,
      type: 'line',
      animations: {
        enabled: true,
        easing: 'linear',
        dynamicAnimation: {
          speed: 2000
        }
      },
      toolbar: {
        show: false
      },
      zoom: {
        enabled: false
      },
      dropShadow: {
        enabled: true,
        top: 3,
        left: 3,
        blur: 1,
        opacity: 0.2
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'smooth'
    },
    series: [{
      data: data
    }],
    fill: {
      type: 'gradient',
      gradient: {
        shade: 'dark',
        gradientToColors: ['#FDD835'],
        shadeIntensity: 1,
        type: 'horizontal',
        opacityFrom: 1,
        opacityTo: 1,
        stops: [0, 100, 100, 100]
      }
    },
    markers: {
      size: 0
    },
    xaxis: {
      type: 'datetime',
      range: 777600000
    },
    yaxis: {
      max: 100
    },
    legend: {
      show: false
    }
  };
  var RealTimechart = new ApexCharts(document.querySelector("#realTimeLine-chart"), options);
  RealTimechart.render();
  var dataPointsLength = 10;
  window.setInterval(function () {
    getNewSeries(lastDate, {
      min: 10,
      max: 90
    });
    RealTimechart.updateSeries([{
      data: data
    }]);
  }, 2000); // every 60 seconds, we reset the data 

  window.setInterval(function () {
    resetData();
    RealTimechart.updateSeries([{
      data: data
    }], false, true);
  }, 60000); // Dashed Line Chart

  var options = {
    chart: {
      height: 350,
      type: 'line',
      zoom: {
        enabled: false
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      width: [5, 7, 5],
      curve: 'smooth',
      dashArray: [0, 8, 5]
    },
    series: [{
      name: "Session Duration",
      data: [45, 52, 38, 24, 33, 26, 21, 20, 6, 8, 15, 10]
    }, {
      name: "Page Views",
      data: [35, 41, 62, 42, 13, 18, 29, 37, 36, 51, 32, 35]
    }, {
      name: 'Total Visits',
      data: [87, 57, 74, 99, 75, 38, 62, 47, 82, 56, 45, 47]
    }],
    markers: {
      size: 0,
      hover: {
        sizeOffset: 6
      }
    },
    xaxis: {
      categories: ['01 Jan', '02 Jan', '03 Jan', '04 Jan', '05 Jan', '06 Jan', '07 Jan', '08 Jan', '09 Jan', '10 Jan', '11 Jan', '12 Jan']
    },
    tooltip: {
      y: [{
        title: {
          formatter: function formatter(val) {
            return val + " (mins)";
          }
        }
      }, {
        title: {
          formatter: function formatter(val) {
            return val + " per session";
          }
        }
      }, {
        title: {
          formatter: function formatter(val) {
            return val;
          }
        }
      }]
    },
    grid: {
      borderColor: '#f1f1f1'
    }
  };
  var chart = new ApexCharts(document.querySelector("#dashedLineChart"), options);
  chart.render(); // brush chart

  var data = generateDayWiseTimeSeries(new Date('11 Feb 2017').getTime(), 185, {
    min: 30,
    max: 90
  });
  var optionsline2 = {
    chart: {
      id: 'chart2',
      type: 'line',
      height: 230,
      toolbar: {
        autoSelected: 'pan',
        show: false
      }
    },
    colors: ['#546E7A'],
    stroke: {
      width: 3
    },
    dataLabels: {
      enabled: false
    },
    fill: {
      opacity: 1
    },
    markers: {
      size: 0
    },
    series: [{
      data: data
    }],
    xaxis: {
      type: 'datetime'
    }
  };
  var chartline2 = new ApexCharts(document.querySelector("#chart-line2"), optionsline2);
  chartline2.render();
  var options = {
    chart: {
      id: 'chart1',
      height: 130,
      type: 'area',
      brush: {
        target: 'chart2',
        enabled: true
      },
      selection: {
        enabled: true,
        xaxis: {
          min: new Date('19 Jun 2017').getTime(),
          max: new Date('14 Aug 2017').getTime()
        }
      }
    },
    colors: ['#008FFB'],
    series: [{
      data: data
    }],
    fill: {
      type: 'gradient',
      gradient: {
        opacityFrom: 0.91,
        opacityTo: 0.1
      }
    },
    xaxis: {
      type: 'datetime',
      tooltip: {
        enabled: false
      }
    },
    yaxis: {
      tickAmount: 2
    }
  };
  var chart = new ApexCharts(document.querySelector("#brushLine-chart"), options);
  chart.render();
  /*
    // this function will generate output in this format
    // data = [
        [timestamp, 23],
        [timestamp, 33],
        [timestamp, 12]
        ...
    ]
  */

  function generateDayWiseTimeSeries(baseval, count, yrange) {
    var i = 0;
    var series = [];

    while (i < count) {
      var x = baseval;
      var y = Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min;
      series.push([x, y]);
      baseval += 86400000;
      i++;
    }

    return series;
  }
});