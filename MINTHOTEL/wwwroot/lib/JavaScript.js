function showAlert(message) {
    $('#alertMessage').html(message).fadeIn();
    setTimeout(function () {
        $('#alertMessage').fadeOut();
    }, 3000); // 3000 milisegundos = 3 segundos
}