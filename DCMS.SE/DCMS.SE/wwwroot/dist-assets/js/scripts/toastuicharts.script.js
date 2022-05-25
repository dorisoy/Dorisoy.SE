"use strict";

$(document).ready(function () {
  // var chart = tui.chart;
  //    basic bar chart 
  var container = document.getElementById('basicBarChart');
  var data = {
    categories: ['June', 'July', 'Aug', 'Sep', 'Oct', 'Nov'],
    series: [{
      name: 'Budget',
      data: [5000, 3000, 5000, 7000, 6000, 4000]
    }, {
      name: 'Income',
      data: [8000, 1000, 7000, 2000, 5000, 3000]
    }]
  };
  var options = {
    chart: {
      // width: 500,
      // height: 400,
      title: 'Monthly Revenue',
      format: '1,000'
    },
    yAxis: {
      title: 'Month'
    },
    xAxis: {
      title: 'Amount',
      min: 0,
      max: 9000,
      suffix: '$'
    },
    series: {
      showLabel: true
    }
  };
  var theme = {
    series: {
      colors: ['#83b14e', '#458a3f', '#295ba0', '#2a4175', '#289399', '#289399', '#617178', '#8a9a9a', '#516f7d', '#dddddd']
    }
  }; // For apply theme

  tui.chart.registerTheme('myTheme', theme);
  options.theme = 'myTheme';
  tui.chart.barChart(container, data, options); // bar chart negetive data

  var container = document.getElementById('negetiveBarChart');
  var data = {
    categories: ['May', 'June', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
    series: [{
      name: 'Budget',
      data: [4000, 5000, 3000, 5000, 7000, 6000, 4000, 1000]
    }, {
      name: 'Income',
      data: [7000, 8000, 1000, 7000, 2000, 7000, 3000, 5000]
    }, {
      name: 'Expenses',
      data: [-5000, -4000, -4000, -6000, -3000, -4000, -5000, -7000]
    }]
  };
  var options = {
    chart: {
      // width: 500,
      // height: 400,
      title: 'Monthly Revenue',
      format: '1,000'
    },
    yAxis: {
      title: 'Month'
    },
    xAxis: {
      title: 'Amount',
      min: -10000,
      max: 10000
    },
    series: {
      showLabel: false
    }
  };
  var theme = {
    series: {
      colors: ['#83b14e', '#458a3f', '#295ba0', '#2a4175', '#289399', '#289399', '#617178', '#8a9a9a', '#516f7d', '#dddddd']
    }
  }; // For apply theme

  tui.chart.registerTheme('myTheme', theme);
  options.theme = 'myTheme';
  tui.chart.barChart(container, data, options);
});