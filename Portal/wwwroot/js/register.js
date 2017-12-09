$(document).ready(function () {
    window.latt = 45.943161, window.long = 24.966760000000022; //Romania

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        console.log("Geolocation is not supported by this browser.");
    }

});

function showPosition(position) {

    window.latt = position.coords.latitude;
    window.long = position.coords.longitude;

    $("#register-latitude-input").val(window.latt);
    $("#register-longitude-input").val(window.long);

    initMap();
}

function initMap() {
    var loc = { lat: window.latt, lng: window.long };
    window.map = new google.maps.Map(document.getElementById('map'), {
        zoom: 15,
        center: loc
    });
    var marker = new google.maps.Marker({
        position: loc,
        map: window.map,
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