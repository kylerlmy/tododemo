﻿// Write your JavaScript code.
$(document).ready(function () {
    $('#add-item-button').on('click', addItem);

    $('.done-checkbox').on('click',function(e){
        markCompleted(e.target);
    })
})

function addItem() {
    $('#add-item-error').hide();
    var newTitle = $('#add-item-title').val();
    $.post('/Todo/AddItem', { title: newTitle }, function () {
        window.location = '/Todo';//使用 window.location 刷新页面,把 location 设置为 /Todo，也就是当前这个页面所在的位置
    })
        .fail(function (data) {
            if (data && data.responseJSON) {
                var firstError = data.responseJSON[Object.keys(data.responseJSON)[0]];
                $('#add-item-error').text(firstError);
                $('#add-item-error').show();
            }
        });
}

function markCompleted(checkbox)
{
    checkbox.disabled=true;
    $.post('/Todo/MarkDone',{id:checkbox.name},function(){
        var row=checkbox.parentElement.parentElement;
        $(row).addClass('done');//为选中的行添加类‘done’
    })
}