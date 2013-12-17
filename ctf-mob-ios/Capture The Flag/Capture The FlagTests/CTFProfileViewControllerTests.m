//
//  CTFProfileViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 17.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFProfileViewController.h"
@interface CTFProfileViewControllerTests : XCTestCase

@end

@implementation CTFProfileViewControllerTests

- (void)setUp
{
    [super setUp];
    // Put setup code here; it will be run once, before the first test case.
}

- (void)tearDown
{
    // Put teardown code here; it will be run once, after the last test case.
    [super tearDown];
}

- (void)testNavigationItemTitleShouldBeSet {
    UIStoryboard *storyboard = [UIStoryboard storyboardWithName:@"Main" bundle:[NSBundle mainBundle]];
    CTFProfileViewController *vc = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([CTFProfileViewController class])];
    [vc view];
    
    XCTAssertEqualObjects(vc.navigationItem.title, NSLocalizedString(@"view.profile.navigation.title", nil), @"");
}

@end
