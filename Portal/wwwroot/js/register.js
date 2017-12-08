﻿$(document).ready(function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        console.log("Geolocation is not supported by this browser.");
    }
});

function showPosition(position) {
    console.log(position.coords.latitude +
        "<br>Longitude: " + position.coords.longitude);
}