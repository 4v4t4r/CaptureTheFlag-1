//
//  CTFLoginServiceTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.02.2014.
//  Copyright (c) 2014 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>

#import "CTFAPIAccounts.h"
#import "CTFLoginService.h"
#import "CTFAPIConnection.h"

@interface CTFLoginServiceTests : XCTestCase

@end

@implementation CTFLoginServiceTests

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

- (void)testThatServiceShouldNotBeNil {
    CTFLoginService *service = [[CTFLoginService alloc] initWithAccounts:[CTFAPIAccounts new]];
    XCTAssertNotNil(service, @"");
}

- (void)testThatAccountsPropertyShouldBeSetAfterInit {
    CTFAPIAccounts *_acc = [[CTFAPIAccounts alloc] initWithConnection:[CTFAPIConnection new]];
    CTFLoginService *service = [[CTFLoginService alloc] initWithAccounts:_acc];
    XCTAssertNotNil(service.accounts, @"");
}

@end
