//
//  CTFMainViewControllerTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFMainViewController.h"

@interface CTFMainViewControllerTests : XCTestCase

@end

@implementation CTFMainViewControllerTests
{
    UIStoryboard *storyboard;
    CTFMainViewController *vc;
}

- (void)setUp
{
    [super setUp];
    storyboard = [UIStoryboard storyboardWithName:@"Main" bundle:nil];
    vc = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([CTFMainViewController class])];
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


#pragma mark - Actions
- (void)testOnLogoutAction
{
    SEL selector = NSSelectorFromString(@"onLogout:");
    XCTAssertTrue([vc respondsToSelector:selector], @"vc should respond to selector logoutPressed:");
}

@end
