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

- (void)testOutletsAndActions
{
    UIStoryboard *storyboard = [UIStoryboard storyboardWithName:@"LoginAndRegister" bundle:nil];
    CTFRegisterViewController *registerVC = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([CTFRegisterViewController class])];
    [registerVC view]; /// call this to force call viewDidLoad.
    
    /// Outlets
    XCTAssertNotNil(registerVC.emailTF, @"");
    XCTAssertNotNil(registerVC.usernameTF, @"");
    XCTAssertNotNil(registerVC.passwordTF, @"");
    XCTAssertNotNil(registerVC.rePasswordTF, @"");
    XCTAssertNotNil(registerVC.registerBtn, @"");
    
    /// Delegates
    XCTAssertEqualObjects(registerVC.emailTF.delegate, registerVC, @"");
    XCTAssertEqualObjects(registerVC.usernameTF.delegate, registerVC, @"");
    XCTAssertEqualObjects(registerVC.passwordTF.delegate, registerVC, @"");
    XCTAssertEqualObjects(registerVC.rePasswordTF.delegate, registerVC, @"");
}

@end
