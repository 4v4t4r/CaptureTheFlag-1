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


#pragma mark - Outlets
- (void)testThatFirstNameTextFieldShouldBeNotNil {
    XCTAssertNotNil(_vc.firstNameTextField, @"");
}

- (void)testThatLastNameTextFieldShouldBeNotNil {
    XCTAssertNotNil(_vc.lastNameTextField, @"");
}

- (void)testThatNickTextFieldShouldBeNotNil {
    XCTAssertNotNil(_vc.nickTextField, @"");
}

- (void)testThatEmailTextFieldShouldBeNotNil {
    XCTAssertNotNil(_vc.emailTextField, @"");
}

- (void)testThatUpdateButtonShouldBeNotNil {
    XCTAssertNotNil(_vc.updateButton, @"");
}

#pragma mark - Localization
- (void)testNavigationItemTitleShouldBeSet {
    XCTAssertEqualObjects(_vc.navigationItem.title, NSLocalizedStringFromTable(@"navigationItem.title", @"Profile", nil), @"");
}

- (void)testThatFirstNameTextFieldIsLocalized {
    XCTAssertEqualObjects(_vc.firstNameTextField.placeholder, NSLocalizedStringFromTable(@"textField.firstName.placeholder", @"Profile", nil), @"");
}

- (void)testThatLastNameTextFieldIsLocalzied {
    XCTAssertEqualObjects(_vc.lastNameTextField.placeholder, NSLocalizedStringFromTable(@"textField.lastName.placeholder", @"Profile", nil), @"");
}

- (void)testThatNickTextFieldIsLocalized {
    XCTAssertEqualObjects(_vc.nickTextField.placeholder, NSLocalizedStringFromTable(@"textField.nick.placeholder", @"Profile", nil), @"");
}

- (void)testThatEmailTextFieldIsLocalized {
    XCTAssertEqualObjects(_vc.emailTextField.placeholder, NSLocalizedStringFromTable(@"textField.email.placeholder", @"Profile", nil), @"");
}

- (void)testThatUpdateButtonFieldIsLocalized {
    XCTAssertEqualObjects(_vc.updateButton.title, NSLocalizedStringFromTable(@"button.update.title", @"Profile", nil), @"");
}

@end
