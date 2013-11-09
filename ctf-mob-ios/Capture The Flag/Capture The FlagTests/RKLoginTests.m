//
//  RKLoginTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 09.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import <RestKit/RestKit.h>
#import <RestKit/Testing.h>
#import "RKLogin.h"

@interface RKLoginTests : XCTestCase

@end

@implementation RKLoginTests

- (void)setUp
{
    [super setUp];
    NSBundle *bundle = [NSBundle bundleWithIdentifier:[[NSBundle mainBundle].bundleIdentifier stringByAppendingString:@"Tests"]];
    [RKTestFixture setFixtureBundle:bundle];
}

- (void)testLoginMapping
{
    /// Get json
    id parsedJSON = [RKTestFixture parsedObjectWithContentsOfFixture:@"login.json"];
    RKMappingTest *test = [RKMappingTest testForMapping:[RKLogin loginMapping] sourceObject:parsedJSON destinationObject:nil];
    
    /// Configure expectations
    RKPropertyMappingTestExpectation *successExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"login.success" destinationKeyPath:@"success"];
    
    RKPropertyMappingTestExpectation *tokenExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"login.token" destinationKeyPath:@"token"];
    
    RKPropertyMappingTestExpectation *messageExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"login.message" destinationKeyPath:@"message"];
    
    /// Add
    [test addExpectation:successExpectation];
    [test addExpectation:tokenExpectation];
    [test addExpectation:messageExpectation];
    
    /// Validate
    XCTAssertTrue([test evaluateExpectation:successExpectation error:nil], @"Success has not been set up");
    XCTAssertTrue([test evaluateExpectation:tokenExpectation error:nil], @"Token has not been set up");
    XCTAssertTrue([test evaluateExpectation:messageExpectation error:nil], @"Message has not been set up");
}

@end
