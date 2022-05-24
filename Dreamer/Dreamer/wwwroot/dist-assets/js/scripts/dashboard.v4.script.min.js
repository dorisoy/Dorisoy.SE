"use strict";

function ownKeys(object, enumerableOnly) { var keys = Object.keys(object); if (Object.getOwnPropertySymbols) { var symbols = Object.getOwnPropertySymbols(object); if (enumerableOnly) symbols = symbols.filter(function (sym) { return Object.getOwnPropertyDescriptor(object, sym).enumerable; }); keys.push.apply(keys, symbols); } return keys; }

function _objectSpread(target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i] != null ? arguments[i] : {}; if (i % 2) { ownKeys(source, true).forEach(function (key) { _defineProperty(target, key, source[key]); }); } else if (Object.getOwnPropertyDescriptors) { Object.defineProperties(target, Object.getOwnPropertyDescriptors(source)); } else { ownKeys(source).forEach(function (key) { Object.defineProperty(target, key, Object.getOwnPropertyDescriptor(source, key)); }); } } return target; }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

$(document).ready(function () {
  $('#user_table').DataTable();
  $('#sales_table').DataTable();
  var echartElem1 = document.getElementById('echart1');

  if (echartElem1) {
    var echart1 = echarts.init(echartElem1);
    echart1.setOption(_objectSpread({}, echartOptions.defaultOptions, {}, {
      grid: echartOptions.gridAlignLeft,
      series: [_objectSpread({
        data: [30, 40, 20, 50, 40, 80, 90, 40]
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
      grid: echartOptions.gridAlignLeft,
      series: [_objectSpread({
        data: [30, 40, 20, 50, 40, 80, 90, 40]
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
        echart2.resize();
      }, 500);
    });
  }

  var echartElem3 = document.getElementById('echart3');

  if (echartElem3) {
    var echart3 = echarts.init(echartElem3);
    echart3.setOption(_objectSpread({}, echartOptions.lineFullWidth, {}, {
      series: [_objectSpread({
        data: [80, 40, 90, 20, 80, 30, 90, 30, 80, 10, 70, 30, 90]
      }, echartOptions.smoothLine, {
        markArea: {
          label: {
            show: true
          }
        },
        areaStyle: {
          color: 'rgba(102, 51, 153, .15)',
          origin: 'start'
        },
        lineStyle: {
          // width: 1,
          color: 'rgba(102, 51, 153, 0.68)'
        },
        itemStyle: {
          color: '#663399'
        }
      }), _objectSpread({
        data: [20, 80, 40, 90, 20, 80, 30, 90, 30, 80, 10, 70, 30]
      }, echartOptions.smoothLine, {
        markArea: {
          label: {
            show: true
          }
        },
        areaStyle: {
          color: 'rgba(255, 152, 0, 0.15)',
          origin: 'start'
        },
        lineStyle: {
          // width: 1,
          color: 'rgba(255, 152, 0, .6)'
        },
        itemStyle: {
          color: 'rgba(255, 152, 0, 1)'
        }
      })]
    }));
    $(window).on('resize', function () {
      setTimeout(function () {
        echart3.resize();
      }, 500);
    });
  }
});