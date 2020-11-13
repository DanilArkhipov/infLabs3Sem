// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function loadTable(){
    $('#tableBody').empty();
    $.ajax({
        type:"GET",
        url:"https://localhost:44325/movies",
        dataType: "json",
        success: function (data){
            let num = 1;
            let str=""
            //<tr id="tr11"><td id="td11">11</td><td id="td12">12</td>
            data.forEach(function (el,i)
                {
                    str+="<tr><td>"+num+"</td><td>"+el.name+"</td><td>"+el.author+"</td><td>"+el.reliseDate+"</td></tr>"
                    num++;
                }
            )
            $('#tableBody').append(str);
                
            
        }
    })}
loadTable();
$("#btnInfo").click(function (){
    $("#formInfo").toggle();
});
$("#formInfo").submit(function (event){
    let str = $("#formInputInfo").val();
    $.ajax({
        type: "GET",
        url: "https://localhost:44325/movies/get/"+str,
        dataType: "json",
        success: function (data){
            let dataStr = "Id:" + data.id+ "<br>" + "Название:" + data.name+"<br>"+"Жанр:" + data.janr+"<br>"+ "Режиссёр:" + data.author+"<br>" + "Дата выхода:" + data.reliseDate;
            $("#lblInfo").empty();
            $("#lblInfo").append(dataStr);
        },
        error: function (){
            $("#lblInfo").empty()
            $("#lblInfo").append("Ничего не найдено");
        }
    })
    event.preventDefault();
})
$("#btnAdd").click(function (){
    $("#formAdd").toggle();
});
function addRecord(){
    $.post(
       "https://localhost:44325/movies",
        {
            id: $("#formInputId").val(),
            name: $("#formInputName").val(),
            janr: $("#formInputJanr").val(),
            author: $("#formInputAuthor").val(),
            reliseDate: $("#formInputReliseDate").val(),
        },
        function (){
           $("#lblAdd").empty().append("Запись добавлена");
           loadTable();
        }
        
        
    )
    return false;
}
$("#btnUpdate").click(function (){
    $("#formUpdate").toggle();
});
function updateRecord() {
    $.ajax({
            url: "https://localhost:44325/movies",
            type: 'PUT',
            data:{
            currentId: $("#formInputCurrentId").val(),
            chanedId: $("#formInputUpdateId").val(),
            name: $("#formInputUpdateName").val(),
            janr: $("#formInputUpdateJanr").val(),
            author: $("#formInputUpdateAuthor").val(),
            reliseDate: $("#formInputUpdateReliseDate").val()},
            success: function () {
                $("#lblUpdate").empty().append("Запись обновлена");
             loadTable();
            }})
    return false;
}

$("#btnDelete").click(function (){
    $("#formDelete").toggle();
});
function deleteRecord() {
    $.ajax({
        url: "https://localhost:44325/movies",
        type: 'DELETE',
        data:{
            id: $("#formInputDelete").val(),
            },
        success: function () {
            $("#lblDelete").empty().append("Запись удалена");
            loadTable();
        }})
    return false;
}

$(document).ready(function() {
    var connection = new WebSocketManager.Connection("wss://localhost:44325/chatManager");
    connection.enableLogging = true;

    connection.connectionMethods.onConnected = () => {

    }

    connection.connectionMethods.onDisconnected = () => {

    }

    connection.clientMethods["pingMessage"] = (socketId, message) => {
        var messageText = socketId + ' said: ' + message;
        $('#messages').append('<li>' + messageText + '</li>');
        $('#messages').scrollTop($('#messages').prop('scrollHeight'));
    }

    connection.start();

    var $messagecontent = $('#message-content');
    $messagecontent.keyup(function(e) {
        if (e.keyCode == 13) {
            var message = $messagecontent.val().trim();
            if (message.length == 0) {
                return false;
            }
            connection.invoke("SendMessage", connection.connectionId, message);
            $messagecontent.val('');
        }
    });
    $('#messages').scrollTop($('#messages').prop('scrollHeight'));
});
