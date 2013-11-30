//
//  CTFUserAPITests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFUser.h"

@interface CTFUserAPITests : XCTestCase

@end

@implementation CTFUserAPITests

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

- (void)testUserShouldCanRegister {
    
    CTFUser *user = [CTFUser createObject];
    user.username = @"login";
    user.password = @"password";
}

@end
