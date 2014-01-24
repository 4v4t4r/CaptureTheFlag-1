//
//  MapViewAnnotation.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 16/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>
@import MapKit;

@interface MapViewAnnotation : NSObject <MKAnnotation>

@property (nonatomic, copy) NSString *title;
@property (nonatomic, readonly) CLLocationCoordinate2D coordinate;

- initWithTitle:(NSString *)title andCoordinate:(CLLocationCoordinate2D)c2d;

@end
