//
//  MapViewAnnotation.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 16/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "MapViewAnnotation.h"

@implementation MapViewAnnotation {
    NSString *_title;
    CLLocationCoordinate2D _coordinate;
}

- (id)initWithTitle:(NSString *)title andCoordinate:(CLLocationCoordinate2D)c2d {
    self = [super init];
    if (self) {
        _title = title;
        _coordinate = c2d;
    }
    return self;
}

- (void)dealloc {
    _title = nil;
}

@end
