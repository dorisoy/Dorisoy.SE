"use strict"; // html demo

$('#html').jstree(); // inline data demo

$('#data').jstree({
  'core': {
    'data': [{
      "text": "Root node",
      "children": [{
        "text": "Child node 1"
      }, {
        "text": "Child node 2"
      }]
    }]
  }
}); // data format demo

$('#frmt').jstree({
  'core': {
    'data': [{
      "text": "Root node",
      "state": {
        "opened": true
      },
      "children": [{
        "text": "Child node 1",
        "state": {
          "selected": true
        },
        "icon": "jstree-file"
      }, {
        "text": "Child node 2",
        "state": {
          "disabled": true
        }
      }]
    }]
  }
}); // ajax demo

$('#ajax').jstree({
  'core': {
    'data': {
      "url": "./root.json",
      "dataType": "json" // needed only if you do not supply JSON headers

    }
  }
}); // lazy demo

$('#lazy').jstree({
  'core': {
    'data': {
      "url": "//www.jstree.com/fiddle/?lazy",
      "data": function data(node) {
        return {
          "id": node.id
        };
      }
    }
  }
}); // data from callback

$('#clbk').jstree({
  'core': {
    'data': function data(node, cb) {
      if (node.id === "#") {
        cb([{
          "text": "Root",
          "id": "1",
          "children": true
        }]);
      } else {
        cb(["Child"]);
      }
    }
  }
}); // interaction and events

$('#evts_button').on("click", function () {
  var instance = $('#evts').jstree(true);
  instance.deselect_all();
  instance.select_node('1');
});
$('#evts').on("changed.jstree", function (e, data) {
  if (data.selected.length) {
    alert('The selected node is: ' + data.instance.get_node(data.selected[0]).text);
  }
}).jstree({
  'core': {
    'multiple': false,
    'data': [{
      "text": "Root node",
      "children": [{
        "text": "Child node 1",
        "id": 1
      }, {
        "text": "Child node 2"
      }]
    }]
  }
});