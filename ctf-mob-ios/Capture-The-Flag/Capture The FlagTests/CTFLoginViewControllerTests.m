//
//  CTFLoginViewControllerTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 08.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFLoginViewController.h"
#import <JRSwizzle/JRSwizzle.h>

static NSString * const kFillTextFieldIfNecesaryCalled = @"FillTextFieldIfNecessary";

@interface CTFLoginViewController (Swizzle)
- (void)_fillTextFieldIfNecessary_swizzle;
@end

@implementation CTFLoginViewController (Swizzle)

- (void)_fillTextFieldIfNecessary_swizzle {
    [[NSNotificationCenter defaultCenter] postNotificationName:kFillTextFieldIfNecesaryCalled object:nil];
}

@end


@interface CTFLoginViewControllerTests : XCTestCase

@end

@implementation CTFLoginViewControllerTests
{
    UIStoryboard *storyboard;
    CTFLoginViewController *vc;
    BOOL notificationPosted;
}

- (void)setUp
{
    [super setUp];
    notificationPosted = NO;
    storyboard = [UIStoryboard storyboardWithName:@"LoginAndRegister" bundle:nil];
    vc = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([CTFLoginViewController class])];
    [vc view];
}

- (void)tearDown
{
    notificationPosted = NO;
    vc = nil;
    storyboard = nil;
    [super tearDown];
}

- (void)testStoryboardShouldExists
{
    XCTAssertNotNil(storyboard, @"Storyboard should exists");
}

- (void)testCTFLoginVCShouldExists
{
    XCTAssertNotNil(vc, @"Controller with this identifier should exists");
}

#pragma mark - Outlets
- (void)testUserNameTextFieldShouldExists
{
    XCTAssertNotNil(vc.usernameTF, @"");
}

- (void)testPasswordTextFieldShouldExists
{
    XCTAssertNotNil(vc.passwordTF, @"");
}

- (void)testLoginButtonShouldExists
{
    XCTAssertNotNil(vc.loginBtn, @"");
}

- (void)testRegisterButtonShouldExists
{
    XCTAssertNotNil(vc.registerBtn, @"");
}

- (void)testStatusLabelShouldExists
{
    XCTAssertNotNil(vc.statusLabel, @"");
}


#pragma mark - Delegates
- (void)testThatUserNameTextFieldShouldHasDelegate {
    XCTAssertEqualObjects(vc.usernameTF.delegate, vc, @"");
}

- (void)testThatPasswordTextFieldShouldHasDelegate {
    XCTAssertEqualObjects(vc.passwordTF.delegate, vc, @"");
}


#pragma mark - Localization
- (void)testThatNavigationItemTitleShouldBeSet {
    XCTAssertEqualObjects(vc.navigationItem.title, NSLocalizedStringFromTable(@"navigation.title", @"Login", @""), @"");
}

- (void)testThatUserNameTextFieldPlaceholderShouldBeSet {
    XCTAssertEqualObjects(vc.usernameTF.placeholder, NSLocalizedStringFromTable(@"textField.username.placeholder", @"Login", @""), @"");
}

- (void)testThatPasswordTextFieldPlaceholderShouldBeSet {
    XCTAssertEqualObjects(vc.passwordTF.placeholder, NSLocalizedStringFromTable(@"textField.password.placeholder", @"Login", @""), @"");
}

- (void)testThatLoginButtonTitleShouldBeSet {
    XCTAssertEqualObjects(vc.loginBtn.titleLabel.text, NSLocalizedStringFromTable(@"button.login.title", @"Login", @""), @"");
}

- (void)testThatRegisterButtonTitleShouldBeSet {
    XCTAssertEqualObjects(vc.registerBtn.titleLabel.text, NSLocalizedStringFromTable(@"button.register.title", @"Login", @""), @"");
}


#pragma mark - Actions
- (void)testLoginButtonAction
{
    NSString *action = [vc.loginBtn actionsForTarget:vc forControlEvent:UIControlEventTouchUpInside][0];
    XCTAssertEqualObjects(action, @"loginPressed", @"action should be loginPressed");
}


#pragma mark - Segues
- (void)testToRegisterSegue
{
    XCTAssertTrue([vc shouldPerformSegueWithIdentifier:@"ToRegisterSegue" sender:vc], @"");
}


#pragma mark - fillTextFieldsIfNecessary
- (void)fillTextFieldsIfNecessaryCalled {
    notificationPosted = YES;
    [[NSNotificationCenter defaultCenter] removeObserver:self];
}

- (void)testThatFillTextFieldsIfNecessaryCalledInViewWillAppear {
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(fillTextFieldsIfNecessaryCalled) name:kFillTextFieldIfNecesaryCalled object:nil];
    [[vc class] jr_swizzleMethod:NSSelectorFromString(@"_fillTextFieldIfNecessary") withMethod:@selector(_fillTextFieldIfNecessary_swizzle) error:nil];
    [vc viewWillAppear:NO];
    
    if (!notificationPosted) {
        XCTFail(@"method _fillTextFieldsIfNecessary should be called");
    }
}

@end
