//
//  RKLoginRequestTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 09.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import <RestKit/RestKit.h>
#import <RestKit/Testing.h>
#import "RKLoginRequest.h"

@interface RKLoginRequestTests : XCTestCase

@end

@implementation RKLoginRequestTests

- (void)setUp
{
    [super setUp];
    NSBundle *bundle = [NSBundle bundleWithIdentifier:[[NSBundle mainBundle].bundleIdentifier stringByAppendingString:@"Tests"]];
    [RKTestFixture setFixtureBundle:bundle];
}

- (RKMappingTest *)mappingTest
{
    id parsedJSON = [RKTestFixture parsedObjectWithContentsOfFixture:@"login-request.json"];
    RKMappingTest *test = [RKMappingTest testForMapping:[RKLoginRequest objectMapping] sourceObject:parsedJSON destinationObject:nil];
    return test;
}

- (void)testMappingOfLogin
{
    XCTAssertTrue([[self mappingTest] evaluateExpectation:[RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"login.login" destinationKeyPath:@"login"] error:nil], @"login has not been set up");
}

- (void)testMappingOfPassword
{
    XCTAssertTrue([[self mappingTest] evaluateExpectation:[RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"login.password" destinationKeyPath:@"password"] error:nil], @"password has not been set up");
}

@end
