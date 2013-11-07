//
//  CTFLoginViewControllerTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 08.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFLoginViewController.h"

@interface CTFLoginViewControllerTests : XCTestCase

@end

@implementation CTFLoginViewControllerTests

- (void)testOutletsAndActions
{
    UIStoryboard *storyboard = [UIStoryboard storyboardWithName:@"LoginAndRegister" bundle:nil];
    CTFLoginViewController *loginVC = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([CTFLoginViewController class])];
    [loginVC view]; /// call this to force call viewDidLoad.
    
    /// Outlets
    XCTAssertNotNil(loginVC.usernameTF, @"");
    XCTAssertNotNil(loginVC.passwordTF, @"");
    XCTAssertNotNil(loginVC.loginBtn, @"");
    XCTAssertNotNil(loginVC.registerBtn, @"");
    
    /// Delegates
    XCTAssertEqualObjects(loginVC.usernameTF.delegate, loginVC, @"");
    XCTAssertEqualObjects(loginVC.passwordTF.delegate, loginVC, @"");
}

@end
