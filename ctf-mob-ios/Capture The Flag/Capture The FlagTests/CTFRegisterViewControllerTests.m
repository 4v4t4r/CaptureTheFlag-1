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
    CTFRegisterViewController *vc;
}

- (void)setUp
{
    [super setUp];
    storyboard = [UIStoryboard storyboardWithName:@"LoginAndRegister" bundle:nil];
    vc = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([CTFRegisterViewController class])];
    [vc view];
}

- (void)tearDown
{
    vc = nil;
    storyboard = nil;
    [super tearDown];
}

- (void)testStoryboardShouldExists
{
    XCTAssertNotNil(storyboard, @"");
}

- (void)testViewControllerShouldExists
{
    XCTAssertNotNil(vc, @"");
}


#pragma mark - Outlets
- (void)testEmailTextFieldShouldExists
{
    XCTAssertNotNil(vc.emailTF, @"");
}

- (void)testUsernameTextFieldShouldExists
{
    XCTAssertNotNil(vc.usernameTF, @"");
}

- (void)testPasswordTextFieldShouldExists
{
    XCTAssertNotNil(vc.passwordTF, @"");
}

- (void)testRePasswordTextFieldShouldExists
{
    XCTAssertNotNil(vc.rePasswordTF, @"");
}

- (void)testRegisterButtonShouldExists
{
    XCTAssertNotNil(vc.registerBtn, @"");
}

- (void)testSatusLabelShouldExists
{
    XCTAssertNotNil(vc.statusLabel, @"");
}


#pragma mark - Actions
- (void)testRegisterButtonAction
{
    NSString *action = [vc.registerBtn actionsForTarget:vc forControlEvent:UIControlEventTouchUpInside][0];
    XCTAssertEqualObjects(action, @"registerPressed", @"");
}

@end
