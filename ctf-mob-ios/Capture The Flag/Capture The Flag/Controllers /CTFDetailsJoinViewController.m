//
//  CTFDetailsJoinViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 16/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFDetailsJoinViewController.h"
#import "CTFGame.h"
#import "CTFMap.h"
#import "MapViewAnnotation.h"

@implementation CTFDetailsJoinViewController {
    CTFGame *_game;
}

- (void)viewDidLoad
{
    [super viewDidLoad];
    
    _mapView.delegate = self;
    
    self.navigationItem.title = _game.name;
    [self _configureMapView];
}

- (void)_configureMapView {
    MapViewAnnotation *annotation = [[MapViewAnnotation alloc] initWithTitle:_game.name andCoordinate:_game.map.locationCoordinates.coordinate];
    [_mapView addAnnotation:annotation];
}

- (void)setGame:(CTFGame *)game {
    _game = game;
}


#pragma mark - MKMapViewDelegate
- (void)mapView:(MKMapView *)mapView didAddAnnotationViews:(NSArray *)views {
    MKAnnotationView *annotationView = views[0];
    id <MKAnnotation> annotation = [annotationView annotation];
    
    CLLocation *annotationLocation = [[CLLocation alloc] initWithLatitude:annotation.coordinate.latitude longitude:annotation.coordinate.longitude];
    
    CLLocationDistance offset = 1000;
    CLLocationDistance distance = [annotationLocation distanceFromLocation:_mapView.userLocation.location];
    MKCoordinateRegion viewRegion = MKCoordinateRegionMakeWithDistance(annotationLocation.coordinate, distance + offset, distance + offset);
    [_mapView setRegion:viewRegion animated:YES];
}

@end
