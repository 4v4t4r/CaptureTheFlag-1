//
//  CTFRegisterViewControllerTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 08.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFRegisterViewController.h"

@interface CTFRegisterViewControllerTests : XCTestCase

@end

@implementation CTFRegisterViewControllerTests
{
    UIStoryboard *storyboard;
    CTFRegisterViewController *_vc;
}

- (void)setUp
{
    [super setUp];
    storyboard = [UIStoryboard storyboardWithName:@"LoginAndRegister" bundle:nil];
    _vc = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([CTFRegisterViewController class])];
    [_vc view];
}

- (void)tearDown
{
    _vc = nil;
    storyboard = nil;
    [super tearDown];
}

- (void)testStoryboardShouldExists
{
    XCTAssertNotNil(storyboard, @"");
}

- (void)testViewControllerShouldExists
{
    XCTAssertNotNil(_vc, @"");
}


#pragma mark - Outlets
- (void)testEmailTextFieldShouldExists
{
    XCTAssertNotNil(_vc.emailTF, @"");
}

- (void)testUsernameTextFieldShouldExists
{
    XCTAssertNotNil(_vc.usernameTF, @"");
}

- (void)testPasswordTextFieldShouldExists
{
    XCTAssertNotNil(_vc.passwordTF, @"");
}

- (void)testRePasswordTextFieldShouldExists
{
    XCTAssertNotNil(_vc.rePasswordTF, @"");
}

- (void)testRegisterButtonShouldExists
{
    XCTAssertNotNil(_vc.registerBtn, @"");
}

- (void)testSatusLabelShouldExists
{
    XCTAssertNotNil(_vc.statusLabel, @"");
}


#pragma mark - Localization
- (void)testThatNavigationItemTitleIsSet {
    XCTAssertEqualObjects(_vc.navigationItem.title, NSLocalizedStringFromTable(@"navigationItem.title", @"Register", @""), @"");
}

- (void)testThatEmailTextFieldPlaceholderIsSet {
    XCTAssertEqualObjects(_vc.emailTF.placeholder, NSLocalizedStringFromTable(@"textField.email.placeholder", @"Register", @""), @"");
}

- (void)testthatUserNameTextFieldPlaceholderIsSet {
    XCTAssertEqualObjects(_vc.usernameTF.placeholder, NSLocalizedStringFromTable(@"textField.username.placeholder", @"Register", @""), @"");
}

- (void)testThatPasswordTextFieldPlaceholderIsSet {
    XCTAssertEqualObjects(_vc.passwordTF.placeholder, NSLocalizedStringFromTable(@"textField.password.placeholder", @"Register", @""), @"");
}

- (void)testthatRePasswordTextFieldPlaceholderIsSet {
    XCTAssertEqualObjects(_vc.rePasswordTF.placeholder, NSLocalizedStringFromTable(@"textField.re-password.placeholder", @"Register", @""), @"");
}


#pragma mark - Actions
- (void)testRegisterButtonAction
{
    NSString *action = [_vc.registerBtn actionsForTarget:_vc forControlEvent:UIControlEventTouchUpInside][0];
    XCTAssertEqualObjects(action, @"registerPressed", @"");
}

@end
