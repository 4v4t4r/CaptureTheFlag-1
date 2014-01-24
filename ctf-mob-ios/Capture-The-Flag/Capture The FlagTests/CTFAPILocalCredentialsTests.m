//
//  CTFLocalCredentialsTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFAPILocalCredentials.h"

@interface CTFAPILocalCredentialsTests : XCTestCase

@end

@implementation CTFAPILocalCredentialsTests

- (CTFAPILocalCredentials *)credentialsWithUsername:(NSString *)username andPassword:(NSString *)password {
    return [[CTFAPILocalCredentials alloc] initWithUsername:username password:password];
}

- (void)testShouldInitCTFLocalCredentials {
    XCTAssertNotNil([self credentialsWithUsername:@"login" andPassword:@"password"], @"");
}

- (void)testShouldReturnNilObjectWhenSomeArgumentIsNil {
    XCTAssertNil([self credentialsWithUsername:nil andPassword:@"password"], @"");
    XCTAssertNil([self credentialsWithUsername:@"login" andPassword:nil], @"");
    XCTAssertNil([self credentialsWithUsername:nil andPassword:nil], @"");
}

- (void)testShouldReturnNilObjectWhenSomeArgumentIsEmpty {
    XCTAssertNil([self credentialsWithUsername:@"" andPassword:@"password"], @"");
    XCTAssertNil([self credentialsWithUsername:@"username" andPassword:@""], @"");
    XCTAssertNil([self credentialsWithUsername:@"" andPassword:@""], @"");
}

@end
