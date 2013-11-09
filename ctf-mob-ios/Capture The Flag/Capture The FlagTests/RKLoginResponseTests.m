//
//  RKLoginResponseTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 09.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import <RestKit/RestKit.h>
#import <RestKit/Testing.h>
#import "RKLoginResponse.h"

@interface RKLoginResponseTests : XCTestCase

@end

@implementation RKLoginResponseTests

- (void)setUp
{
    [super setUp];
    NSBundle *bundle = [NSBundle bundleWithIdentifier:[[NSBundle mainBundle].bundleIdentifier stringByAppendingString:@"Tests"]];
    [RKTestFixture setFixtureBundle:bundle];
}

- (RKMappingTest *)mappingTest
{
    id parsedJSON = [RKTestFixture parsedObjectWithContentsOfFixture:@"login-response.json"];
    RKMappingTest *test = [RKMappingTest testForMapping:[RKLoginResponse objectMapping] sourceObject:parsedJSON destinationObject:nil];
    return test;
}

- (void)testMappingOfSuccess
{
    XCTAssertTrue([[self mappingTest] evaluateExpectation:[RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"login.success" destinationKeyPath:@"success"] error:nil], @"success has not been set up");
}

- (void)testMappingOfToken
{
    XCTAssertTrue([[self mappingTest] evaluateExpectation:[RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"login.token" destinationKeyPath:@"token"] error:nil], @"token has not been set up");
}

- (void)testMappingOfMessage
{
    XCTAssertTrue([[self mappingTest] evaluateExpectation:[RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"login.message" destinationKeyPath:@"message"] error:nil], @"message has not been set up");
}

@end
