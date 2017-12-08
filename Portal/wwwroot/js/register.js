$(document).ready(function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        console.log("Geolocation is not supported by this browser.");
    }
});

function showPosition(position) {
    $("#register-latitude-input").val(position.coords.latitude);
    $("#register-longitude-input").val(position.coords.longitude);
    console.log(position.coords.longitude);
}