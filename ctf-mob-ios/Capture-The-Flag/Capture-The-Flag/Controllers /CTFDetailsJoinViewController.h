//
//  CTFDetailsJoinViewController.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 16/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFViewController.h"
@import MapKit;

@class CTFGame;

@interface CTFDetailsJoinViewController : CTFViewController <MKMapViewDelegate>
@property (weak, nonatomic) IBOutlet MKMapView *mapView;

- (void)setGame:(CTFGame *)game;

@end
