var MY_MAPTYPE_ID = 'custom_style';
google.maps.visualRefresh = true;
var map;

function initialize() {
	var latitude = 53.4285;
    var longitude = 14.5528;
    if (window.android){
		latitude = window.android.getLatitude();
		longitude = window.android.getLongitude();
    }

    var myLatlng = new google.maps.LatLng(latitude,longitude);
    var myOptions = {
		zoom: 17,
		center: myLatlng,
		disableDefaultUI: true,
        mapTypeControlOptions: {
          mapTypeIds: [google.maps.MapTypeId.ROADMAP, MY_MAPTYPE_ID]
        },
        mapTypeId: MY_MAPTYPE_ID
    }
    map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
    map.setTilt(45);
    map.setHeading(90);

//url = http://maps.googleapis.com/maps/api/staticmap?center=53.430028,14.553151&zoom=18&format=png&sensor=false&size=640x480&maptype=roadmap&style=feature:water|color:0x47483e&style=feature:water|element:labels|visibility:off&style=feature:water|element:labels.icon|visibility:off&style=feature:water|element:labels.text.fill|visibility:off&style=feature:transit|element:labels|visibility:off&style=feature:road|visibility:simplified|invert_lightness:true|color:0xe8c3ae|weight:4.2&style=feature:poi|visibility:off&style=feature:road|element:labels|visibility:on|weight:0.1|color:0x080205&style=feature:administrative|element:labels|hue:0xff0000|lightness:100|gamma:1.91|saturation:100|visibility:off&style=feature:transit.line|visibility:off&style=feature:poi.park|element:labels|visibility:off&style=feature:landscape.natural.terrain|visibility:off&style=feature:landscape.man_made|element:labels|visibility:off&style=feature:landscape.natural.terrain|color:0x808080|visibility:off&style=feature:landscape.man_made|visibility:simplified|hue:0x005eff|saturation:1|lightness:-62
	var featureOpts = [
                        {
                          "featureType": "water",
                          "stylers": [
                            { "color": "#47483e" }
                          ]
                        },{
                          "featureType": "water",
                          "elementType": "labels",
                          "stylers": [
                            { "visibility": "off" }
                          ]
                        },{
                          "featureType": "water",
                          "elementType": "labels.icon",
                          "stylers": [
                            { "visibility": "off" }
                          ]
                        },{
                          "featureType": "water",
                          "elementType": "labels.text.fill",
                          "stylers": [
                            { "visibility": "off" }
                          ]
                        },{
                          "featureType": "transit",
                          "elementType": "labels",
                          "stylers": [
                            { "visibility": "off" }
                          ]
                        },{
                          "featureType": "road",
                          "stylers": [
                            { "visibility": "simplified" },
                            { "invert_lightness": true },
                            { "color": "#e8c3ae" },
                            { "weight": 4.2 }
                          ]
                        },{
                          "featureType": "poi",
                          "stylers": [
                            { "visibility": "off" }
                          ]
                        },{
                          "featureType": "road",
                          "elementType": "labels",
                          "stylers": [
                            { "visibility": "on" },
                            { "weight": 0.1 },
                            { "color": "#080205" }
                          ]
                        },{
                          "featureType": "administrative",
                          "elementType": "labels",
                          "stylers": [
                            { "hue": "#ff0000" },
                            { "lightness": 100 },
                            { "gamma": 1.91 },
                            { "saturation": 100 },
                            { "visibility": "off" }
                          ]
                        },{
                          "featureType": "transit.line",
                          "stylers": [
                            { "visibility": "off" }
                          ]
                        },{
                          "featureType": "poi.park",
                          "elementType": "labels",
                          "stylers": [
                            { "visibility": "off" }
                          ]
                        },{
                          "featureType": "landscape.natural.terrain",
                          "stylers": [
                            { "visibility": "off" }
                          ]
                        },{
                          "featureType": "landscape.man_made",
                          "elementType": "labels",
                          "stylers": [
                            { "visibility": "off" }
                          ]
                        },{
                          "featureType": "landscape.natural.terrain",
                          "stylers": [
                            { "color": "#808080" },
                            { "visibility": "off" }
                          ]
                        },{
                          "featureType": "landscape.man_made",
                          "stylers": [
                            { "visibility": "simplified" },
                            { "hue": "#005eff" },
                            { "saturation": 1 },
                            { "lightness": -62 }
                          ]
                        },{
                        }
                      ];

    var styledMapOptions = {
		name: 'Custom Style'
    };

    var customMapType = new google.maps.StyledMapType(featureOpts, styledMapOptions);

    map.mapTypes.set(MY_MAPTYPE_ID, customMapType);
}

function centerAt(latitude, longitude){
	myLatlng = new google.maps.LatLng(latitude,longitude);
    map.panTo(myLatlng);
}
