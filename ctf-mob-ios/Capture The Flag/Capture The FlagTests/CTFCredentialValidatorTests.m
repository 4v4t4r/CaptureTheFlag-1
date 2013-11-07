//
//  CTFCredentialValidatorTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFCredentialValidator.h"

@interface CTFCredentialValidatorTests : XCTestCase
@end

@implementation CTFCredentialValidatorTests

- (void)testLoginLength
{
    XCTAssertFalse([self validUsername:@"login"], @"");
    XCTAssertTrue([self validUsername:@"loginn"], @"");
    XCTAssertFalse([self validUsername:@"thisisverylonglogin"], @"");
}

- (void)testLoginCharacters
{
    XCTAssertTrue([self validUsername:@"login1"], @"");
    XCTAssertTrue([self validUsername:@"123456"], @"");
    XCTAssertFalse([self validUsername:@"_123456"], @"");
    XCTAssertFalse([self validUsername:@"_123d5_"], @"");
    XCTAssertFalse([self validUsername:@"valid!@__l"], @"");
}

- (BOOL)validUsername:(NSString *)username
{
    return [CTFCredentialValidator validCredential:username withType:CredentialTypeUsername];
}

@end
