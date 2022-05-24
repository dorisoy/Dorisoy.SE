"use strict";

var _card_body = document.getElementById('card-body');

var _card_footer = document.getElementById('card-footer');

var _arrow_down = document.getElementById('arrow-down');

var closeWindow = document.getElementById('close-window');

var _card = document.getElementById('card');

var _reload = document.getElementById('reload');

_arrow_down.addEventListener('click', minimizeButton); // minimize window


function minimizeButton(e) {
  _card_body.classList.toggle('active');

  e.preventDefault();
} // reload window


_reload.addEventListener('click', _reloadFunction);

function _reloadFunction(e) {
  _card_body.reload();

  e.preventDefault();
} // remove window


closeWindow.addEventListener('click', removeWindow);

function removeWindow(e) {
  _card.classList.add('active');

  e.preventDefault();
} // Searching Js ====================================================------------------------->
// get input element


var filterInput = document.getElementById('filterInput'); //Add Eventlistener
// filterInput.addEventListener('keyup',filterNames);
// function filterNames()
// {
//     // get value of input
//     let filterValue = document.getElementById('filterInput').value.toUpperCase();
//     //get names tr
//     let tr = document.getElementById('names');
//     // Get td from tr
//     let td = tr.querySelectorAll('td.collection-item');
//       // Loop through collection-item td
//       for(let i = 0;i < td.length;i++){
//         let a = td[i].getElementsByTagName('a')[0];
//         // If matched
//         if(a.innerHTML.toUpperCase().indexOf(filterValue) > -1){
//           td[i].style.display = '';
//         } else {
//           td[i].style.display = 'none';
//         }
//       }
// }