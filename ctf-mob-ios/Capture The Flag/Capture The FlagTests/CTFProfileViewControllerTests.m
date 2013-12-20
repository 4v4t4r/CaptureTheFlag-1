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

@implementation CTFProfileViewControllerTests {
    UIStoryboard *_storyboard;
    CTFProfileViewController *_vc;
}

- (void)setUp {
    [super setUp];
    _storyboard = [UIStoryboard storyboardWithName:@"Main" bundle:[NSBundle mainBundle]];
    _vc = [_storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([CTFProfileViewController class])];
    [_vc view];
}

- (void)tearDown {
    _storyboard = nil;
    _vc = nil;
    [super tearDown];
}

- (void)testNavigationItemTitleShouldBeSet {
    XCTAssertEqualObjects(_vc.navigationItem.title, NSLocalizedString(@"view.profile.navigation.title", nil), @"");
}

- (void)testThatViewShouldHasFirstNameTextField {
    XCTAssertNotNil(_vc.firstNameTextField, @"");
}

- (void)testThatViewShouldHasLastNameTextField {
    XCTAssertNotNil(_vc.lastNameTextField, @"");
}

- (void)testThatViewShouldHasNickTextField {
    XCTAssertNotNil(_vc.nickTextField, @"");
}

- (void)testThatViewShouldHasEmailTextField {
    XCTAssertNotNil(_vc.emailTextField, @"");
}

@end
