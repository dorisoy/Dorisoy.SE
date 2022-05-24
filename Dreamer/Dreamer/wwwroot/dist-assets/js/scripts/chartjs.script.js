"use strict";

$(document).ready(function () {
  // basic line chart
  var ctx = document.getElementById('LineChart').getContext('2d');
  var chart = new Chart(ctx, {
    // The type of chart we want to create
    type: 'line',
    // The data for our dataset
    data: {
      labels: ["January", "February", "March", "April", "May", "June", "July"],
      datasets: [{
        label: "My First dataset",
        backgroundColor: '#FF6384',
        borderColor: '#f47c96',
        data: [0, 10, 5, 20, 15, 20, 30]
      }]
    },
    // Configuration options go here
    options: {}
  }); // line chart 2 

  var ctx = document.getElementById("LineChart2");
  var lineChart = new Chart(ctx, {
    type: 'line',
    data: {
      labels: ["January", "February", "March", "April", "May", "June", "July"],
      datasets: [{
        label: "Sales",
        backgroundColor: "#36A2EB",
        borderColor: "#0292f4",
        pointBorderColor: "#0292f4",
        pointBackgroundColor: "#0292f4",
        pointHoverBackgroundColor: "#fff",
        pointHoverBorderColor: "#0292f4",
        pointBorderWidth: 1,
        data: [31, 74, 6, 39, 20, 85, 7]
      }, {
        label: "ravenue",
        backgroundColor: "#4BC0C0",
        borderColor: "#05c4c4",
        pointBorderColor: "#05c4c4",
        pointBackgroundColor: "#05c4c4",
        pointHoverBackgroundColor: "#fff",
        pointHoverBorderColor: "rgba(151,187,205,1)",
        pointBorderWidth: 1,
        data: [82, 23, 66, 9, 99, 4, 2]
      }]
    }
  }); // bar chart 

  var ctx = document.getElementById("BarChart");
  var myChart = new Chart(ctx, {
    type: 'bar',
    data: {
      labels: ["Red", "Blue", "Yellow", "Green", "Purple", "Orange", 'olive', 'Teal', 'Magenta'],
      datasets: [{
        label: '# of Votes',
        data: [12, 19, 3, 5, 2, 3, 10, 14, 9],
        backgroundColor: ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(128, 128, 0, 0.2)', 'rgb(0, 128, 128,0.2)', 'rgb(255, 0, 255,0.2)'],
        borderColor: ['rgba(255,99,132,1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)', 'rgba(128, 128, 0, 1)', 'rgb(0, 128, 128,1)', 'rgb(255, 0, 255,1)'],
        borderWidth: 1
      }]
    },
    options: {
      scales: {
        yAxes: [{
          ticks: {
            beginAtZero: true
          }
        }]
      }
    }
  }); // multiple bar chart 

  var ctx = document.getElementById("BarChart2");
  var mybarChart = new Chart(ctx, {
    type: 'bar',
    data: {
      labels: ["January", "February", "March", "April", "May", "June", "July"],
      datasets: [{
        label: '# of Votes',
        backgroundColor: "#4BC0C0",
        data: [51, 30, 40, 28, 92, 50, 45]
      }, {
        label: '# of Votes',
        backgroundColor: "#36A2EB",
        data: [41, 56, 25, 48, 72, 34, 12]
      }]
    },
    options: {
      scales: {
        yAxes: [{
          ticks: {
            beginAtZero: true
          }
        }]
      }
    }
  }); // mix chart 

  var ctx = document.getElementById("MixChart");
  var myMixChart = new Chart(ctx, {
    type: 'bar',
    data: {
      labels: ["January", "February", "March", "April", "May", "June", "July", 'august'],
      datasets: [{
        label: '# of Votes',
        backgroundColor: "#4BC0C0",
        data: [51, 30, 40, 28, 92, 50, 45]
      }, {
        label: '# of Votes',
        backgroundColor: "#36A2EB",
        data: [41, 56, 25, 48, 72, 34, 12]
      }, {
        label: 'Line Dataset',
        backgroundColor: 'rgba(255, 99, 132, 0.2)',
        // borderColor: '#f47c96',
        data: [0, 50, 60, 50, 90, 70, 95, 50],
        // Changes this dataset to become a line
        type: 'line'
      }]
    },
    options: {
      scales: {
        yAxes: [{
          ticks: {
            beginAtZero: true
          }
        }]
      }
    }
  }); // Horizontal bar chart 

  var ctx = document.getElementById("HorizontalBarChart");
  var mybarChart = new Chart(ctx, {
    type: 'horizontalBar',
    data: {
      labels: ["January", "February", "March", "April", "May", "June", "July"],
      datasets: [{
        label: '# of Votes',
        backgroundColor: "#4BC0C0",
        data: [51, 30, 40, 28, 92, 50, 45]
      }, {
        label: '# of Votes',
        backgroundColor: "#36A2EB",
        data: [41, 56, 25, 48, 72, 34, 12]
      }]
    },
    options: {
      scales: {
        yAxes: [{
          ticks: {
            beginAtZero: true
          }
        }]
      }
    }
  }); // radar chart

  var ctx = document.getElementById("RadarChart");
  var data = {
    labels: ["Eating", "Drinking", "Sleeping", "Designing", "Coding", "Cycling", "Running"],
    datasets: [{
      label: "My First dataset",
      backgroundColor: "rgba(102, 51, 153, 0.5)",
      borderColor: "#639",
      pointBorderColor: "#825da7",
      pointBackgroundColor: "#825da7",
      pointHoverBackgroundColor: "#fff",
      pointHoverBorderColor: "rgba(220,220,220,1)",
      data: [65, 59, 90, 81, 56, 55, 40]
    }, {
      label: "My Second dataset",
      backgroundColor: "rgba(177, 125, 230, 0.7)",
      borderColor: "#639",
      pointColor: "#639",
      pointStrokeColor: "#fff",
      pointHighlightFill: "#fff",
      pointHighlightStroke: "rgba(151,187,205,1)",
      data: [28, 48, 40, 19, 96, 27, 100]
    }]
  };
  var RadarChart = new Chart(ctx, {
    type: 'radar',
    data: data
  }); // dhougnut chart

  var ctx = document.getElementById("DoughnutChart");
  var data = {
    labels: ["Mozila", "IE", "Google Chrome", " Edge", "Safari"],
    datasets: [{
      data: [120, 50, 140, 180, 100],
      backgroundColor: ["#455C73", "#9B59B6", "#BDC3C7", "#26B99A", "#3498DB"],
      hoverBackgroundColor: ["#34495E", "#B370CF", "#CFD4D8", "#36CAAB", "#49A9EA"]
    }]
  };
  var DoughnutChart = new Chart(ctx, {
    type: 'doughnut',
    tooltipFillColor: "rgba(51, 51, 51, 0.55)",
    data: data
  }); // Pie chart

  var ctx = document.getElementById("PieChart");
  var data = {
    labels: ["Mozila", "IE", "Google Chrome", " Edge", "Safari"],
    datasets: [{
      data: [120, 50, 140, 180, 100],
      backgroundColor: ["#455C73", "#9B59B6", "#BDC3C7", "#26B99A", "#3498DB"],
      hoverBackgroundColor: ["#34495E", "#B370CF", "#CFD4D8", "#36CAAB", "#49A9EA"]
    }]
  };
  var PieChart = new Chart(ctx, {
    type: 'pie',
    tooltipFillColor: "rgba(51, 51, 51, 0.55)",
    data: data
  }); // Pie chart

  var ctx = document.getElementById("PolarChart");
  var data = {
    labels: ["Mozila", "IE", "Google Chrome", " Edge", "Safari"],
    datasets: [{
      data: [120, 50, 140, 180, 100],
      backgroundColor: ["#455C73", "#9B59B6", "#BDC3C7", "#26B99A", "#3498DB"],
      hoverBackgroundColor: ["#34495E", "#B370CF", "#CFD4D8", "#36CAAB", "#49A9EA"]
    }]
  };
  var PolarChart = new Chart(ctx, {
    type: 'polarArea',
    tooltipFillColor: "rgba(51, 51, 51, 0.55)",
    data: data
  });
});