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

#pragma mark - CredentailTypeUsername
- (void)testUsernameLength
{
    XCTAssertFalse([self validUsername:@"login"], @"");
    XCTAssertTrue([self validUsername:@"loginn"], @"");
    XCTAssertFalse([self validUsername:@"thisisverylonglogin"], @"");
}

- (void)testUsernameCharacters
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


#pragma mark - CredentailTypePassword
- (void)testPasswordLength
{
    XCTAssertFalse([self validPassword:@"abc"], @"");
    XCTAssertTrue([self validPassword:@"ancdefgv"], @"");
    XCTAssertFalse([self validPassword:@"123asd!@#%^|dggg66654"]);
}

- (void)testPasswordCharacters
{
    XCTAssertTrue([self validPassword:@"goodPass123!@#avc:)"], @"");
    XCTAssertTrue([self validPassword:@"goodPassButWeak"], @"");
    XCTAssertTrue([self validPassword:@"§1234567890-="], @"");
    XCTAssertTrue([self validPassword:@"!@#$$%%^&*())"], @"");
    XCTAssertFalse([self validPassword:@"this is not a password"], @"");
}

- (BOOL)validPassword:(NSString *)password
{
    return [CTFCredentialValidator validCredential:password withType:CredentialTypePassword];
}

@end
