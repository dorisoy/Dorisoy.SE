"use strict";

$(document).ready(function () {
  // basic Area chart
  var bAOptions = {
    chart: {
      height: 350,
      type: 'area',
      zoom: {
        enabled: false
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
      curve: 'straight'
    },
    series: [{
      name: "STOCK ABC",
      data: series.monthDataSeries1.prices
    }],
    // title: {
    //     text: '',
    //     align: 'left'
    // },
    // subtitle: {
    //     text: 'Price Movements',
    //     align: 'left'
    // },
    labels: series.monthDataSeries1.dates,
    xaxis: {
      type: 'datetime'
    },
    yaxis: {
      opposite: true
    },
    legend: {
      horizontalAlign: 'left'
    }
  };
  var chart = new ApexCharts(document.querySelector("#basicArea-chart"), bAOptions);
  chart.render(); // sline area 

  var SAoptions = {
    chart: {
      height: 350,
      type: 'area',
      toolbar: {
        show: false
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'smooth'
    },
    series: [{
      name: 'series1',
      data: [31, 40, 28, 51, 42, 109, 100]
    }, {
      name: 'series2',
      data: [11, 32, 45, 32, 34, 52, 41]
    }],
    xaxis: {
      type: 'datetime',
      categories: ["2018-09-19T00:00:00", "2018-09-19T01:30:00", "2018-09-19T02:30:00", "2018-09-19T03:30:00", "2018-09-19T04:30:00", "2018-09-19T05:30:00", "2018-09-19T06:30:00"]
    },
    tooltip: {
      x: {
        format: 'dd/MM/yy HH:mm'
      }
    }
  };
  var chart = new ApexCharts(document.querySelector("#SplineArea"), SAoptions);
  chart.render(); // datetime X-axis

  var options = {
    annotations: {
      yaxis: [{
        y: 30,
        borderColor: '#999',
        label: {
          show: true,
          text: 'Support',
          style: {
            color: "#fff",
            background: '#00E396'
          }
        }
      }],
      xaxis: [{
        x: new Date('14 Nov 2012').getTime(),
        borderColor: '#999',
        yAxisIndex: 0,
        label: {
          show: true,
          text: 'Rally',
          style: {
            color: "#fff",
            background: '#775DD0'
          }
        }
      }]
    },
    chart: {
      type: 'area',
      height: 350
    },
    title: {
      text: 'Datetime X-Axis',
      align: 'left'
    },
    dataLabels: {
      enabled: false
    },
    series: [{
      data: dateSeries1
    }],
    markers: {
      size: 0,
      style: 'hollow'
    },
    xaxis: {
      type: 'datetime',
      min: new Date('01 Mar 2012').getTime(),
      tickAmount: 6
    },
    tooltip: {
      x: {
        format: 'dd MMM yyyy'
      }
    },
    fill: {
      type: 'gradient',
      gradient: {
        shadeIntensity: 1,
        opacityFrom: 0.7,
        opacityTo: 0.9,
        stops: [0, 100]
      }
    }
  };
  var chart = new ApexCharts(document.querySelector("#timeline-chart"), options);
  chart.render();

  var resetCssClasses = function resetCssClasses(activeEl) {
    var els = document.querySelectorAll("button");
    Array.prototype.forEach.call(els, function (el) {
      el.classList.remove('active');
    });
    activeEl.target.classList.add('active');
  }; // document.querySelector("#one_month").addEventListener('click', function(e) {
  //     resetCssClasses(e)
  //     chart.updateOptions({
  //         xaxis: {
  //             min: new Date('28 Jan 2013').getTime(),
  //             max: new Date('27 Feb 2013').getTime(),
  //         }
  //     })
  // })
  // document.querySelector("#six_months").addEventListener('click', function(e) {
  //     resetCssClasses(e)
  //     chart.updateOptions({
  //         xaxis: {
  //             min: new Date('27 Sep 2012').getTime(),
  //             max: new Date('27 Feb 2013').getTime(),
  //         }
  //     })
  // })
  // document.querySelector("#one_year").addEventListener('click', function(e) {
  //     resetCssClasses(e)
  //     chart.updateOptions({
  //         xaxis: {
  //             min: new Date('27 Feb 2012').getTime(),
  //             max: new Date('27 Feb 2013').getTime(),
  //         }
  //     })
  // })
  // document.querySelector("#ytd").addEventListener('click', function(e) {
  //     resetCssClasses(e)
  //     chart.updateOptions({
  //         xaxis: {
  //             min: new Date('01 Jan 2013').getTime(),
  //             max: new Date('27 Feb 2013').getTime(),
  //         }
  //     })
  // })
  // document.querySelector("#all").addEventListener('click', function(e) {
  //     resetCssClasses(e)
  //     chart.updateOptions({
  //         xaxis: {
  //             min: undefined,
  //             max: undefined,
  //         }
  //     })
  // })
  // document.querySelector("#ytd").addEventListener('click', function() {
  // });
  // negetive Area Chart


  var ngAoptions = {
    chart: {
      height: 350,
      type: 'area',
      // zoom: {
      //     enabled: false
      // }
      toolbar: {
        show: false
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'straight'
    },
    series: [{
      name: 'north',
      data: [{
        x: 1996,
        y: 322
      }, {
        x: 1997,
        y: 324
      }, {
        x: 1998,
        y: 329
      }, {
        x: 1999,
        y: 342
      }, {
        x: 2000,
        y: 348
      }, {
        x: 2001,
        y: 334
      }, {
        x: 2002,
        y: 325
      }, {
        x: 2003,
        y: 316
      }, {
        x: 2004,
        y: 318
      }, {
        x: 2005,
        y: 330
      }, {
        x: 2006,
        y: 355
      }, {
        x: 2007,
        y: 366
      }, {
        x: 2008,
        y: 337
      }, {
        x: 2009,
        y: 352
      }, {
        x: 2010,
        y: 377
      }, {
        x: 2011,
        y: 383
      }, {
        x: 2012,
        y: 344
      }, {
        x: 2013,
        y: 366
      }, {
        x: 2014,
        y: 389
      }, {
        x: 2015,
        y: 334
      }]
    }, {
      name: 'south',
      data: [{
        x: 1996,
        y: 162
      }, {
        x: 1997,
        y: 90
      }, {
        x: 1998,
        y: 50
      }, {
        x: 1999,
        y: 77
      }, {
        x: 2000,
        y: 35
      }, {
        x: 2001,
        y: -45
      }, {
        x: 2002,
        y: -88
      }, {
        x: 2003,
        y: -120
      }, {
        x: 2004,
        y: -156
      }, {
        x: 2005,
        y: -123
      }, {
        x: 2006,
        y: -88
      }, {
        x: 2007,
        y: -66
      }, {
        x: 2008,
        y: -45
      }, {
        x: 2009,
        y: -29
      }, {
        x: 2010,
        y: -45
      }, {
        x: 2011,
        y: -88
      }, {
        x: 2012,
        y: -132
      }, {
        x: 2013,
        y: -146
      }, {
        x: 2014,
        y: -169
      }, {
        x: 2015,
        y: -184
      }]
    }],
    xaxis: {
      type: 'datetime',
      axisBorder: {
        show: false
      },
      axisTicks: {
        show: false
      }
    },
    yaxis: {
      tickAmount: 4,
      floating: false,
      labels: {
        style: {
          color: '#8e8da4'
        },
        offsetY: -7,
        offsetX: 0
      },
      axisBorder: {
        show: false
      },
      axisTicks: {
        show: false
      }
    },
    fill: {
      type: ['gradient', 'solid'],
      opacity: 1
    },
    tooltip: {
      x: {
        format: "yyyy"
      },
      fixed: {
        enabled: false,
        position: 'topRight'
      }
    },
    grid: {
      yaxis: {
        lines: {
          offsetX: -30
        }
      },
      padding: {
        left: 20
      }
    }
  };
  var chart = new ApexCharts(document.querySelector("#negetiveArea"), ngAoptions);
  chart.render(); // Stacked Area Chart

  var Stackedoptions = {
    chart: {
      height: 350,
      type: 'area',
      stacked: true,
      events: {
        selection: function selection(chart, e) {
          console.log(new Date(e.xaxis.min));
        }
      },
      toolbar: {
        show: false
      }
    },
    colors: ['#008FFB', '#00E396', '#CED4DC'],
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'smooth'
    },
    series: [{
      name: 'South',
      data: generateDayWiseTimeSeries(new Date('11 Feb 2017 GMT').getTime(), 20, {
        min: 10,
        max: 60
      })
    }, {
      name: 'North',
      data: generateDayWiseTimeSeries(new Date('11 Feb 2017 GMT').getTime(), 20, {
        min: 10,
        max: 20
      })
    }, {
      name: 'Central',
      data: generateDayWiseTimeSeries(new Date('11 Feb 2017 GMT').getTime(), 20, {
        min: 10,
        max: 15
      })
    }],
    fill: {
      type: 'gradient',
      gradient: {
        opacityFrom: 0.6,
        opacityTo: 0.8
      }
    },
    legend: {
      position: 'top',
      horizontalAlign: 'left'
    },
    xaxis: {
      type: 'datetime'
    }
  };
  var chart = new ApexCharts(document.querySelector("#stackedAreaChart"), Stackedoptions);
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
  } // missing or null values area chart


  var nullOptions = {
    chart: {
      height: 350,
      type: 'area',
      // animations: {
      //     enabled: false
      // },
      zoom: {
        enabled: false
      },
      animations: {
        enabled: true,
        easing: 'easeinout',
        speed: 800,
        animateGradually: {
          enabled: true,
          delay: 150
        },
        dynamicAnimation: {
          enabled: true,
          speed: 350
        }
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: 'straight'
    },
    series: [{
      name: 'Network',
      data: [{
        x: 'Dec 23 2017',
        y: null
      }, {
        x: 'Dec 24 2017',
        y: 44
      }, {
        x: 'Dec 25 2017',
        y: 31
      }, {
        x: 'Dec 26 2017',
        y: 38
      }, {
        x: 'Dec 27 2017',
        y: null
      }, {
        x: 'Dec 28 2017',
        y: 32
      }, {
        x: 'Dec 29 2017',
        y: 55
      }, {
        x: 'Dec 30 2017',
        y: 51
      }, {
        x: 'Dec 31 2017',
        y: 67
      }, {
        x: 'Jan 01 2018',
        y: 22
      }, {
        x: 'Jan 02 2018',
        y: 34
      }, {
        x: 'Jan 03 2018',
        y: null
      }, {
        x: 'Jan 04 2018',
        y: null
      }, {
        x: 'Jan 05 2018',
        y: 11
      }, {
        x: 'Jan 06 2018',
        y: 4
      }, {
        x: 'Jan 07 2018',
        y: 15
      }, {
        x: 'Jan 08 2018',
        y: null
      }, {
        x: 'Jan 09 2018',
        y: 9
      }, {
        x: 'Jan 10 2018',
        y: 34
      }, {
        x: 'Jan 11 2018',
        y: null
      }, {
        x: 'Jan 12 2018',
        y: null
      }, {
        x: 'Jan 13 2018',
        y: 13
      }, {
        x: 'Jan 14 2018',
        y: null
      }]
    }],
    fill: {
      opacity: 0.8,
      type: 'pattern',
      pattern: {
        enabled: true,
        style: ['verticalLines', 'horizontalLines'],
        width: 5,
        depth: 6
      }
    },
    markers: {
      size: 5,
      hover: {
        size: 9
      }
    },
    tooltip: {
      intersect: true,
      shared: false
    },
    theme: {
      palette: 'palette1'
    },
    xaxis: {
      type: 'datetime'
    },
    yaxis: {
      title: {
        text: 'Bytes Received'
      }
    }
  };
  var chart = new ApexCharts(document.querySelector("#nullAreaChart"), nullOptions);
  chart.render();
});