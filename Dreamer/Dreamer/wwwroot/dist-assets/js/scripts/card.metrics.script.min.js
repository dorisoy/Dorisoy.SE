"use strict";

function ownKeys(object, enumerableOnly) { var keys = Object.keys(object); if (Object.getOwnPropertySymbols) { var symbols = Object.getOwnPropertySymbols(object); if (enumerableOnly) symbols = symbols.filter(function (sym) { return Object.getOwnPropertyDescriptor(object, sym).enumerable; }); keys.push.apply(keys, symbols); } return keys; }

function _objectSpread(target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i] != null ? arguments[i] : {}; if (i % 2) { ownKeys(source, true).forEach(function (key) { _defineProperty(target, key, source[key]); }); } else if (Object.getOwnPropertyDescriptors) { Object.defineProperties(target, Object.getOwnPropertyDescriptors(source)); } else { ownKeys(source).forEach(function (key) { Object.defineProperty(target, key, Object.getOwnPropertyDescriptor(source, key)); }); } } return target; }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

$(document).ready(function () {
  var echartElem1 = document.getElementById('echart1');

  if (echartElem1) {
    var echart1 = echarts.init(echartElem1);
    echart1.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
      series: [_objectSpread({
        data: [30, 40, 20, 50, 40, 80, 90]
      }, echartOptions.smoothLine, {
        lineStyle: _objectSpread({
          color: '#4CAF50'
        }, echartOptions.lineShadow),
        itemStyle: {
          color: '#4CAF50'
        }
      })]
    }));
    $(window).on('resize', function () {
      setTimeout(function () {
        echart1.resize();
      }, 500);
    });
  }

  var echartElem2 = document.getElementById('echart2');

  if (echartElem2) {
    var echart2 = echarts.init(echartElem2);
    echart2.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
      series: [_objectSpread({
        data: [6, 4, 7, 4, 6, 4, 1]
      }, echartOptions.smoothLine, {
        lineStyle: _objectSpread({
          color: '#df0029'
        }, echartOptions.lineShadow),
        itemStyle: {
          color: '#df0029'
        }
      })]
    }));
    $(window).on('resize', function () {
      setTimeout(function () {
        echart2.resize();
      }, 500);
    });
  }

  var echartElem3 = document.getElementById('echart3');

  if (echartElem3) {
    var echart3 = echarts.init(echartElem3);
    echart3.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
      series: [_objectSpread({
        data: [2, 2, 7, 4, 6, 4, 8]
      }, echartOptions.smoothLine, {
        lineStyle: _objectSpread({
          color: '#4CAF50'
        }, echartOptions.lineShadow),
        itemStyle: {
          color: '#4CAF50'
        }
      })]
    }));
    $(window).on('resize', function () {
      setTimeout(function () {
        echart3.resize();
      }, 500);
    });
  }

  var echartElem4 = document.getElementById('echart4');

  if (echartElem4) {
    var echart4 = echarts.init(echartElem4);
    echart4.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
      series: [_objectSpread({
        data: [2, 2, 7, 4, 6, 4, 4]
      }, echartOptions.smoothLine, {
        lineStyle: _objectSpread({
          color: '#df0029'
        }, echartOptions.lineShadow),
        itemStyle: {
          color: '#df0029'
        }
      })]
    }));
    $(window).on('resize', function () {
      setTimeout(function () {
        echart4.resize();
      }, 500);
    });
  } // Pie chart


  var echartElem5 = document.getElementById('echart5');

  if (echartElem5) {
    var echart5 = echarts.init(echartElem5);
    echart5.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
      series: [{
        type: 'pie',
        itemStyle: echartOptions.pieLineStyle,
        data: [_objectSpread({
          name: 'Email Subscription',
          value: 80
        }, echartOptions.pieLabelOff, {
          itemStyle: {
            borderColor: '#4CAF50'
          }
        }), _objectSpread({
          name: 'Registration',
          value: 20
        }, echartOptions.pieLabelOff, {
          itemStyle: {
            borderColor: '#df0029'
          }
        })]
      }]
    }));
    $(window).on('resize', function () {
      setTimeout(function () {
        echart5.resize();
      }, 500);
    });
  } // Pie chart


  var echartElem6 = document.getElementById('echart6');

  if (echartElem6) {
    var echart6 = echarts.init(echartElem6);
    echart6.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
      series: [{
        type: 'pie',
        itemStyle: echartOptions.pieLineStyle,
        data: [_objectSpread({
          name: 'Running',
          value: 40
        }, echartOptions.pieLabelOff, {
          itemStyle: {
            borderColor: '#ff9800'
          }
        }), _objectSpread({
          name: 'Completed',
          value: 60
        }, echartOptions.pieLabelOff, {
          itemStyle: {
            borderColor: '#4CAF50'
          }
        })]
      }]
    }));
    $(window).on('resize', function () {
      setTimeout(function () {
        echart6.resize();
      }, 500);
    });
  } // Pie chart


  var echartElem7 = document.getElementById('echart7');

  if (echartElem7) {
    var echart7 = echarts.init(echartElem7);
    echart7.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
      series: [{
        type: 'pie',
        itemStyle: echartOptions.pieLineStyle,
        data: [_objectSpread({
          name: 'Email Subscription',
          value: 80
        }, echartOptions.pieLabelOff, {
          itemStyle: {
            borderColor: '#4CAF50'
          }
        }), _objectSpread({
          name: 'Registration',
          value: 20
        }, echartOptions.pieLabelOff, {
          itemStyle: {
            borderColor: '#df0029'
          }
        })]
      }]
    }));
    $(window).on('resize', function () {
      setTimeout(function () {
        echart7.resize();
      }, 500);
    });
  } // Pie chart


  var echartElem8 = document.getElementById('echart8');

  if (echartElem8) {
    var echart8 = echarts.init(echartElem8);
    echart8.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
      series: [{
        type: 'pie',
        itemStyle: echartOptions.pieLineStyle,
        data: [_objectSpread({
          name: 'Running',
          value: 40
        }, echartOptions.pieLabelOff, {
          itemStyle: {
            borderColor: '#ff9800'
          }
        }), _objectSpread({
          name: 'Completed',
          value: 60
        }, echartOptions.pieLabelOff, {
          itemStyle: {
            borderColor: '#4CAF50'
          }
        })]
      }]
    }));
    $(window).on('resize', function () {
      setTimeout(function () {
        echart8.resize();
      }, 500);
    });
  } // Bar chart


  var echartElem9 = document.getElementById('echart9');

  if (echartElem9) {
    var echart9 = echarts.init(echartElem9);
    echart9.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
      series: [{
        type: 'bar',
        barWidth: 6,
        itemStyle: _objectSpread({
          color: '#ff9800'
        }, echartOptions.lineShadow),
        data: [{
          name: 'Bar 1',
          value: 40
        }, {
          name: 'Bar 2',
          value: 60,
          itemStyle: {
            color: '#4CAF50'
          }
        }, {
          name: 'Bar 3',
          value: 80
        }, {
          name: 'Bar 4',
          value: 70
        }, {
          name: 'Bar 5',
          value: 60
        }, {
          name: 'Bar 6',
          value: 70
        }, {
          name: 'Bar 7',
          value: 80
        }, {
          name: 'Bar 8',
          value: 40
        }, {
          name: 'Bar 9',
          value: 70,
          itemStyle: {
            color: '#4CAF50'
          }
        }]
      }]
    }));
    $(window).on('resize', function () {
      setTimeout(function () {
        echart9.resize();
      }, 500);
    });
  } // Bar chart


  var echartElem10 = document.getElementById('echart10');

  if (echartElem10) {
    var echart10 = echarts.init(echartElem10);
    echart10.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
      series: [{
        type: 'bar',
        barWidth: 6,
        itemStyle: _objectSpread({
          color: '#ff9800'
        }, echartOptions.lineShadow),
        data: [{
          name: 'Bar 1',
          value: 40
        }, {
          name: 'Bar 2',
          value: 60,
          itemStyle: {
            color: '#4CAF50'
          }
        }, {
          name: 'Bar 3',
          value: 80
        }, {
          name: 'Bar 4',
          value: 70
        }, {
          name: 'Bar 5',
          value: 60
        }, {
          name: 'Bar 6',
          value: 70
        }, {
          name: 'Bar 7',
          value: 80
        }, {
          name: 'Bar 8',
          value: 40
        }, {
          name: 'Bar 9',
          value: 70,
          itemStyle: {
            color: '#4CAF50'
          }
        }]
      }]
    }));
    $(window).on('resize', function () {
      setTimeout(function () {
        echart10.resize();
      }, 500);
    });
  }
});