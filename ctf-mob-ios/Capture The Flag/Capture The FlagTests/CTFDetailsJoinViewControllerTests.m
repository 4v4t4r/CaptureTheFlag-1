//
//  CTFDetailsJoinViewControllerTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 19.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFDetailsJoinViewController.h"
#import <JRSwizzle/JRSwizzle.h>

static NSString * const kConfigurationMapViewCalled = @"configurationMapViewEvoked";

@interface CTFDetailsJoinViewController (Swizzle)

- (void)_configureMapView_swizzle;

@end

@implementation CTFDetailsJoinViewController (Swizzle)

- (void)_configureMapView_swizzle {
    [[NSNotificationCenter defaultCenter] postNotificationName:kConfigurationMapViewCalled object:nil];
}

@end


@interface CTFDetailsJoinViewControllerTests : XCTestCase

@end

@implementation CTFDetailsJoinViewControllerTests {
    UIStoryboard *storyboard;
    CTFDetailsJoinViewController *vc;
    BOOL notificationPosted;
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

- (void)configurationMapViewCalled {
    notificationPosted = YES;
    [[NSNotificationCenter defaultCenter] removeObserver:self];
}

- (void)testThatViewDidLoadConfigureMapView {
    notificationPosted = NO;
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(configurationMapViewCalled) name:kConfigurationMapViewCalled object:nil];
    [[vc class] jr_swizzleMethod:NSSelectorFromString(@"_configureMapView") withMethod:@selector(_configureMapView_swizzle) error:nil];
    [vc viewDidLoad];
    
    if (!notificationPosted)
        XCTFail(@"notification with name: %@ should be called", kConfigurationMapViewCalled);
}


#pragma mark - Outlets and Delegates
- (void)testThatMapViewExists {
    XCTAssertNotNil(vc.mapView, @"");
}

- (void)testThatMapViewHasDelegate {
    XCTAssertEqual(vc.mapView.delegate, vc, @"");
}

@end
