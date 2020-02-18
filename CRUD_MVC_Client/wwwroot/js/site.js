// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {

});

function confirmDelete(url) {
  $('#frmDelete').attr('action', url);
  $('.modal').modal('show');
}
