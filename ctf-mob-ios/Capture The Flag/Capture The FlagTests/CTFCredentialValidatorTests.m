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
- (void)testEmptyUsername
{
    XCTAssertTrue([self validUsername:@""] == ValidationEmptyField, @"");
}

- (void)testUsernameLength
{
    XCTAssertTrue([self validUsername:@"login"] == ValidationWrongCredentials, @"");
    XCTAssertTrue([self validUsername:@"loginn"] == ValidationOK, @"");
    XCTAssertTrue([self validUsername:@"thisisverylonglogin"] == ValidationWrongCredentials, @"");
}

- (void)testUsernameCharacters
{
    XCTAssertTrue([self validUsername:@"login1"] == ValidationOK, @"");
    XCTAssertTrue([self validUsername:@"123456"] == ValidationOK, @"");
    XCTAssertTrue([self validUsername:@"_123456"] == ValidationWrongCredentials, @"");
    XCTAssertTrue([self validUsername:@"_123d5_"] == ValidationWrongCredentials, @"");
    XCTAssertTrue([self validUsername:@"valid!@__l"] == ValidationWrongCredentials, @"");
}

- (ValidationResult)validUsername:(NSString *)username
{
    return [CTFCredentialValidator validCredential:username withType:CredentialTypeUsername];
}


#pragma mark - CredentialTypePassword
- (void)testEmptyPassword
{
    XCTAssertTrue([self validPassword:@""] == ValidationEmptyField, @"");
}

- (void)testPasswordLength
{
    XCTAssertTrue([self validPassword:@"abc"] == ValidationWrongCredentials, @"");
    XCTAssertTrue([self validPassword:@"ancdefgv"] == ValidationOK, @"");
    XCTAssertTrue([self validPassword:@"123asd!@#%^|dggg66654"] == ValidationWrongCredentials, @"");
}

- (void)testPasswordCharacters
{
    XCTAssertTrue([self validPassword:@"goodPass123!@#avc:)"] == ValidationOK, @"");
    XCTAssertTrue([self validPassword:@"goodPassButWeak"] == ValidationOK, @"");
    XCTAssertTrue([self validPassword:@"§1234567890-="] == ValidationOK, @"");
    XCTAssertTrue([self validPassword:@"!@#$$%%^&*())"] == ValidationOK, @"");
    XCTAssertTrue([self validPassword:@"this is not a password"] == ValidationWrongCredentials, @"");
}

- (ValidationResult)validPassword:(NSString *)password
{
    return [CTFCredentialValidator validCredential:password withType:CredentialTypePassword];
}


#pragma mark - CredentialTypeEmail
- (void)testEmptyEmail
{
    XCTAssertTrue([self validEmail:@""] == ValidationEmptyField, @"");
}

- (void)testEmailCharacters
{
    XCTAssertTrue([self validEmail:@"abc@abcd.pl"] == ValidationOK, @"");
//    XCTAssertTrue([self validEmail:@".%@.-.pl"] == ValidationOK, @""); /// this must not pass!
    
    XCTAssertTrue([self validEmail:@"abcd@a.p"] == ValidationWrongCredentials, @"");
    XCTAssertTrue([self validEmail:@"@.pl"] == ValidationWrongCredentials, @"");
    XCTAssertTrue([self validEmail:@".pl"] == ValidationWrongCredentials, @"");
    XCTAssertTrue([self validEmail:@"a.pl"] == ValidationWrongCredentials, @"");
    XCTAssertTrue([self validEmail:@"a@pl"] == ValidationWrongCredentials, @"");
}

- (ValidationResult)validEmail:(NSString *)email
{
    return [CTFCredentialValidator validCredential:email withType:CredentialTypeEmail];
}

@end
