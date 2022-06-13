
var currentLat = 35.78023853564178;
var currentLng = 51.46740881600891;
var firstTime = true;
var secondTime = true;
var firstlatlng = {};
var secondlatlng = {};
var markerArray = new Array();

//Initial Map
var map = L.map('map', {
    center: [currentLat, currentLng],
    minZoom: 10,
    maxZoom: 18,
    zoom: 17
});

var circle = L.circle([currentLat, currentLng], {
    //color: '#1bff00',
    //fillColor: 'blue',
    fillOpacity: 0.1,
    radius: 15
}).addTo(map);
//Initial Map

//Set Tile
var tile = L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', { attribution: '&copy;عن تو مملکت' }).addTo(map);
//Set Tile

//Create popup
var popup = L.popup();
//Create popup

//Create icons
var myIcon = L.Icon.extend({
    options: {
        iconSize: [40, 40]
    }
});

var firstIcon = new myIcon({ iconUrl: '/img/mapFirst.ico' });
var secondIcon = new myIcon({ iconUrl: '/img/mapSecond.ico' });
//Create icons

var marker = L.marker();


//functions
function onMapClick(e) {
    popup
        .setLatLng(e.latlng)
        .setContent('You clicked the map at ' + e.latlng.toString())
        .openOn(map);
    setMarker(e.latlng);
}

function setMarker(latlng) {
    if (firstTime == true) {
        firstlatlng = latlng;
        marker = L.marker([latlng.lat, latlng.lng], { icon: firstIcon, title: "مبدا" });
        firstTime = false;
    }
    else {
        secondlatlng = latlng;
        if (secondTime == false) {
            var remove = markerArray.pop();
            map.removeLayer(remove);
        }
        marker = L.marker([latlng.lat, latlng.lng], { icon: secondIcon, title: "مقصد" });
        secondTime = false;
    }
    map.addLayer(marker);
    markerArray.push(marker);

}

function myLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            latit = position.coords.latitude;
            longit = position.coords.longitude;
            marker = L.marker([position.coords.latitude, position.coords.longitude], { icon: firstIcon }).addTo(map);
            console.log('new pos : ' + position.coords.latitude + ' / ' + position.coords.longitude);
            map.panTo(new L.LatLng(latit, longit));
        });
    }
}

function getDistanceFromLatLonInKm(lat1, lon1, lat2, lon2) {
    var R = 6371; // km
    var dLat = toRad(lat2 - lat1);
    var dLon = toRad(lon2 - lon1);
    var lat1 = toRad(lat1);
    var lat2 = toRad(lat2);

    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.sin(dLon / 2) * Math.sin(dLon / 2) * Math.cos(lat1) * Math.cos(lat2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c;
    alert(d);
    return d;
}

function toRad(Value) {
    return Value * Math.PI / 180;
}

function clearMarkers() {
    while (markerArray.length > 0) {
        var remove = markerArray.pop();
        map.removeLayer(remove);
    }

    firstTime = true;
    secondTime = true;
    firstlatlng = {};
    secondlatlng = {};
}

function clearLastMarker() {

    if (markerArray.length <= 0)
        return;

    var remove = markerArray.pop();
    map.removeLayer(remove);

    if (secondTime == false) {
        secondTime = true;
        secondlatlng = {};
    }
    else {
        firstTime = true;
        firstlatlng = {};
    }
}

map.on('click', onMapClick);
//functions
