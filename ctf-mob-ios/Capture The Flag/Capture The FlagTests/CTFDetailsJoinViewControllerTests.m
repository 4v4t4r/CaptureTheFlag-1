//
//  CTFDetailsJoinViewControllerTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 19.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFDetailsJoinViewController.h"

@interface CTFDetailsJoinViewControllerTests : XCTestCase

@end

@implementation CTFDetailsJoinViewControllerTests {
    UIStoryboard *storyboard;
    CTFDetailsJoinViewController *vc;
}

- (void)setUp
{
    [super setUp];
    storyboard = [UIStoryboard storyboardWithName:@"Main" bundle:nil];
    vc = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([CTFDetailsJoinViewController class])];
    [vc view];
}

- (void)tearDown
{
    vc = nil;
    storyboard = nil;
    [super tearDown];
}

- (void)testThatMapViewExists {
    XCTAssertNotNil(vc.mapView, @"");
}

@end
