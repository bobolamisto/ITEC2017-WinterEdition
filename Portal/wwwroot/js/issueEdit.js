$(document).ready(function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        console.log("Geolocation is not supported by this browser.");
    }
});

function showPosition(position) {

}


function initMap() {
    console.log($("#register-longitude-input").val());
    var loc = { lat: Number($("#register-latitude-input").val()), lng: Number($("#register-longitude-input").val()) };
    var map = new google.maps.Map(document.getElementById('issue-edit-map'), {
        zoom: 15,
        center: loc,
    });
    var marker = new google.maps.Marker({
        position: loc,
        map: map,
        title: 'Your location',
        animation: google.maps.Animation.DROP,
    });

    google.maps.event.addListener(map, 'click', function (event) {
        marker.setMap(null);
        marker = new google.maps.Marker({ position: event.latLng, map: map });
        //map.setCenter(marker.getPosition());
        $("#register-latitude-input").val(marker.position.lat);
        $("#register-longitude-input").val(marker.position.lng);
    });

}