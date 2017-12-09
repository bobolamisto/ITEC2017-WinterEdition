$(document).ready(function () {

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        console.log("Geolocation is not supported by this browser.");
    }
});

function showPosition(position) {
    //initMap();
}

function initMap() {
    var loc = { lat: window.centerlat, lng: window.centerlong };
    window.map = new google.maps.Map(document.getElementById('all-issues-map'), {
        zoom: 7,
        center: loc
    });
    for (var i= 0; i < window.issues.length; i++){
        var marker = new google.maps.Marker({
            position: { lat: window.issues[i].location.latitude, lng: window.issues[i].location.longitude },
            map: window.map,
            //title: 'your location',
            //animation: google.maps.animation.drop,
        });
    }

}