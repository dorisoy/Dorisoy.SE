"use strict";

$(document).ready(function () {
  // zero table
  $('#zero_configuration_table').DataTable(); // feature enable/disable

  $('#feature_disable_table').DataTable({
    "paging": false,
    "ordering": false,
    "info": false
  }); // ordering or sorting

  $('#deafult_ordering_table').DataTable({
    "order": [[3, "desc"]]
  }); // multi column ordering

  $('#multicolumn_ordering_table').DataTable({
    columnDefs: [{
      targets: [0],
      orderData: [0, 1]
    }, {
      targets: [1],
      orderData: [1, 0]
    }, {
      targets: [4],
      orderData: [4, 0]
    }]
  }); // hidden column

  $('#hidden_column_table').DataTable({
    responsive: true,
    "columnDefs": [{
      "targets": [2],
      "visible": false,
      "searchable": false
    }, {
      "targets": [3],
      "visible": false
    }]
  }); // complex header 

  $('#complex_header_table').DataTable(); // dom positioning

  $('#dom_positioning_table').DataTable({
    "dom": '<"top"i>rt<"bottom"flp><"clear">'
  }); // alternative pagination

  $('#alternative_pagination_table').DataTable({
    "pagingType": "full_numbers"
  }); // scroll vertical 

  $('#scroll_vertical_table').DataTable({
    "scrollY": "200px",
    "scrollCollapse": true,
    "paging": false
  }); // scroll horizontal 

  $('#scroll_horizontal_table').DataTable({
    "scrollX": true
  }); // scroll vertical dynamic height  

  $('#scroll_vertical_dynamic_height_table').DataTable({
    scrollY: '50vh',
    scrollCollapse: true,
    paging: false
  }); // scroll vertical and horizontal 

  $('#scroll_horizontal_vertical_table').DataTable({
    "scrollY": 200,
    "scrollX": true
  }); // comma decimal

  $('#comma_decimal_table').DataTable({
    "language": {
      "decimal": ",",
      "thousands": "."
    }
  }); // language option

  $('#language_option_table').DataTable({
    "language": {
      "lengthMenu": "Display _MENU_ records per page",
      "zeroRecords": "Nothing found - sorry",
      "info": "Showing page _PAGE_ of _PAGES_",
      "infoEmpty": "No records available",
      "infoFiltered": "(filtered from _MAX_ total records)"
    }
  });
});