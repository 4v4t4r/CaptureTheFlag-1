var MY_MAPTYPE_ID = 'custom_style';
google.maps.visualRefresh = true;
var map;

function drawCircle(point, radius, dir) { 
	var d2r = Math.PI / 180;   // degrees to radians 
	var r2d = 180 / Math.PI;   // radians to degrees 
	var earthsradius = 6378.1; // 6378.1 is the radius of the earth in kilometers

	var points = 32; 

	// find the raidus in lat/lon 
	var rlat = (radius / earthsradius) * r2d; 
	var rlng = rlat / Math.cos(point.lat() * d2r); 


	var extp = new Array(); 
	if (dir==1){
		var start=0;var end=points+1
	} // one extra here makes sure we connect the
	else	{
		var start=points+1;var end=0
	}
	for (var i=start; (dir==1 ? i < end : i > end); i=i+dir) { 
		var theta = Math.PI * (i / (points/2)); 
		ey = point.lng() + (rlng * Math.cos(theta)); // center a + radius x * cos(theta) 
		ex = point.lat() + (rlat * Math.sin(theta)); // center b + radius y * sin(theta) 
		extp.push(new google.maps.LatLng(ex, ey));
	} 
	// alert(extp.length);
	return extp;
}

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
		//disableDefaultUI: true,
        mapTypeControlOptions: {
          mapTypeIds: [google.maps.MapTypeId.ROADMAP, google.maps.MapTypeId.HYBRID, google.maps.MapTypeId.SATELLITE, MY_MAPTYPE_ID]
        },
        mapTypeId: MY_MAPTYPE_ID
    }
    map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
	setPolygon();
    map.setTilt(45);
    map.setHeading(90);
    map.setZoom(17);
	
	var enemyMarker = addEnemyMarker();
	var ownMarker = addOwnMarker();
	var userMarker = addUserMarker();

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

function addEnemyMarker(){

	var enemyMarker = new google.maps.Marker({
		position: new google.maps.LatLng(53.4295, 14.5538),
		map: map,
		title: "Their flag",
		icon: "file:///android_res/drawable/flag_red.png"
	});
	
	var contentString = '<div id="content">'+
      '<div id="siteNotice">'+
      '</div>'+
      '<h1 id="firstHeading" class="firstHeading">Their flag</h1>'+
      '<div id="bodyContent">'+
      '<p>The flag of the enemy</p>'+
      '</div>'+
      '</div>';

	var infowindow = new google.maps.InfoWindow({
		content: contentString,
		maxWidth: 200
	});

	google.maps.event.addListener(enemyMarker, 'click', function() {
		infowindow.open(map,enemyMarker);
	});
	
	return enemyMarker;
}

function addOwnMarker(){
	var ownMarker = new google.maps.Marker({
		position: new google.maps.LatLng(53.4275, 14.5518),
		map: map,
		title: "Our flag",
		icon: "file:///android_res/drawable/flag_blue.png"
	});
	
	var contentString = '<div id="content">'+
      '<div id="siteNotice">'+
      '</div>'+
      '<h1 id="firstHeading" class="firstHeading">Our flag</h1>'+
      '<div id="bodyContent">'+
      '<p>Defend your flag against the enemy.</p>'+
      '</div>'+
      '</div>';

	var infowindow = new google.maps.InfoWindow({
		content: contentString,
		maxWidth: 200
	});

	google.maps.event.addListener(ownMarker, 'click', function() {
		infowindow.open(map,ownMarker);
	});
	
	return ownMarker;
}

function addUserMarker(){

	var userMarker = new google.maps.Marker({
		position: new google.maps.LatLng(53.4202, 14.5549),
		map: map,
		title: "Rafał",
		icon: "file:///android_res/drawable/character.png"
	});
	
	var contentString = '<div id="content">'+
      '<div id="siteNotice">'+
      '</div>'+
      '<h1 id="firstHeading" class="firstHeading">Rafał</h1>'+
      '<div id="bodyContent">'+
      '<p>From blue team.</p>'+
      '</div>'+
      '</div>';

	var infowindow = new google.maps.InfoWindow({
		content: contentString,
		maxWidth: 200
	});

	google.maps.event.addListener(userMarker, 'click', function() {
		infowindow.open(map,userMarker);
	});
	
	return userMarker;
}

function setPolygon(){
	var donut = new google.maps.Polygon({
		paths: [
			drawCircle(new google.maps.LatLng(53.4285,14.5528), 10, 1),
			drawCircle(new google.maps.LatLng(53.4285,14.5528), 1, -1)],
            strokeColor: "#000000",
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: "#000000",
            fillOpacity: 0.6
    });
    donut.setMap(map);
}