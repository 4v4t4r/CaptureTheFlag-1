//
//  MapViewAnnotationTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "MapViewAnnotation.h"

@interface MapViewAnnotationTests : XCTestCase

@end

@implementation MapViewAnnotationTests {
    MapViewAnnotation *_annotation;
    CLLocationCoordinate2D _coordinates;
}

- (void)setUp {
    [super setUp];
    _coordinates = CLLocationCoordinate2DMake(10, 20);
    _annotation = [[MapViewAnnotation alloc] initWithTitle:@"title" andCoordinate:_coordinates];
}

- (void)tearDown {
    _annotation = nil;
    _coordinates = CLLocationCoordinate2DMake(0, 0);
    [super tearDown];
}

- (void)testThatObjectShouldExists {
    XCTAssertNotNil(_annotation, @"");
}

- (void)testThatObjectShouldReturnCorrectPosition {
    XCTAssertEqual(_annotation.coordinate, CLLocationCoordinate2DMake(10, 20), @"");
}

- (void)testThatObjectShouldReturnCorrectTitle {
    XCTAssertEqualObjects(_annotation.title, @"title", @"");
}

@end
