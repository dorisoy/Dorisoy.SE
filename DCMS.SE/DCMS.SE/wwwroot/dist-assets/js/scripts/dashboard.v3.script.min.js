"use strict";

function ownKeys(object, enumerableOnly) { var keys = Object.keys(object); if (Object.getOwnPropertySymbols) { var symbols = Object.getOwnPropertySymbols(object); if (enumerableOnly) symbols = symbols.filter(function (sym) { return Object.getOwnPropertyDescriptor(object, sym).enumerable; }); keys.push.apply(keys, symbols); } return keys; }

function _objectSpread(target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i] != null ? arguments[i] : {}; if (i % 2) { ownKeys(source, true).forEach(function (key) { _defineProperty(target, key, source[key]); }); } else if (Object.getOwnPropertyDescriptors) { Object.defineProperties(target, Object.getOwnPropertyDescriptors(source)); } else { ownKeys(source).forEach(function (key) { Object.defineProperty(target, key, Object.getOwnPropertyDescriptor(source, key)); }); } } return target; }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

$(document).ready(function () {
  var echartElem1 = document.getElementById('echart1');

  if (echartElem1) {
    var echart1 = echarts.init(echartElem1);
    echart1.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
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
        echart1.resize();
      }, 500);
    });
  }

  var echartElem2 = document.getElementById('echart2');

  if (echartElem2) {
    var echart2 = echarts.init(echartElem2);
    echart2.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
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
        echart2.resize();
      }, 500);
    });
  }

  var echartElem3 = document.getElementById('echart3');

  if (echartElem3) {
    var echart3 = echarts.init(echartElem3);
    echart3.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
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
        echart3.resize();
      }, 500);
    });
  }

  var echartElem4 = document.getElementById('echart4');

  if (echartElem4) {
    var echart4 = echarts.init(echartElem4);
    echart4.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
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
        echart4.resize();
      }, 500);
    });
  }
});