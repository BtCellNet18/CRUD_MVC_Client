// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {

});

function showConfirm(title = 'Confirm', body = 'Are you sure?', url = '/') {
  $('#frmConfirm').attr('action', url);
  $('.modal-title').text(title);
  $('.modal-body').text(body);
  $('.modal').modal('show');
}
