var map;
function initialize() {
    var latlng = new google.maps.LatLng(43.415504, 24.620746);
    var myOptions = {
        zoom: 17,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        mapTypeControl: true,
        mapTypeControlOptions:
        {
            style: google.maps.MapTypeControlStyle.DROPDOWN_MENU,
            poistion: google.maps.ControlPosition.TOP_RIGHT,
            mapTypeIds: [google.maps.MapTypeId.ROADMAP,
              google.maps.MapTypeId.TERRAIN,
              google.maps.MapTypeId.HYBRID,
              google.maps.MapTypeId.SATELLITE]
        },
        navigationControl: true,
        navigationControlOptions:
        {
            style: google.maps.NavigationControlStyle.ZOOM_PAN
        },
        scaleControl: true,
        disableDoubleClickZoom: true,
        draggable: true,
        streetViewControl: true,
    };
    map = new google.maps.Map(document.getElementById("map"), myOptions);
    var marker = new google.maps.Marker
    (
        {
            position: latlng,
            map: map,
            title: 'Click me'
        }
    );
    var infowindow = new google.maps.InfoWindow({
        content: 'Vassil Aprilov 17<br/>TechZone<br/> Pleven, 5809<br/>Bulgaria<br/>+359885428144'
    });
    google.maps.event.addListener(marker, 'click', function () {
        // Calling the open method of the infoWindow 
        infowindow.open(map, marker);
    });
}