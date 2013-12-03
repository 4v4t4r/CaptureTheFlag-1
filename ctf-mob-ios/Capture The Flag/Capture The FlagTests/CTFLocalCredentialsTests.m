//
//  CTFLocalCredentialsTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFLocalCredentials.h"

@interface CTFLocalCredentialsTests : XCTestCase

@end

@implementation CTFLocalCredentialsTests

- (void)testShouldInitCTFLocalCredentials {
    CTFLocalCredentials *credentials = [[CTFLocalCredentials alloc] initWithUsername:@"login" password:@"password"];
    XCTAssertNotNil(credentials, @"");
}

- (void)testShouldReturnNilObjectWhenSomeArgumentIsNil {
    CTFLocalCredentials *nilUsername = [[CTFLocalCredentials alloc] initWithUsername:nil password:@"password"];
    XCTAssertNil(nilUsername, @"");
    
    CTFLocalCredentials *nilPassword = [[CTFLocalCredentials alloc] initWithUsername:@"login" password:nil];
    XCTAssertNil(nilPassword, @"");
    
    CTFLocalCredentials *nilArguments = [[CTFLocalCredentials alloc] initWithUsername:nil password:nil];
    XCTAssertNil(nilArguments, @"");
}

- (void)testShouldReturnNilObjectWhenSomeArgumentIsEmpty {
    CTFLocalCredentials *emptyUsername = [[CTFLocalCredentials alloc] initWithUsername:@"" password:@"password"];
    XCTAssertNil(emptyUsername, @"");
    
    CTFLocalCredentials *emptyPassword = [[CTFLocalCredentials alloc] initWithUsername:@"username" password:@""];
    XCTAssertNil(emptyPassword, @"");
    
    CTFLocalCredentials *emptyArguments = [[CTFLocalCredentials alloc] initWithUsername:@"" password:@""];
    XCTAssertNil(emptyArguments, @"");
}

@end
