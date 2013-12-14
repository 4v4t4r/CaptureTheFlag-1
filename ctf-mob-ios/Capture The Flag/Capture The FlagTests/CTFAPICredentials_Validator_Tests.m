//
//  CTFCredentialValidatorTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFAPILocalCredentialsValidator.h"
#import "CTFAPILocalCredentialsValidator+Validator.h"

@interface CTFAPICredentials_Validator_Tests : XCTestCase
@end

@implementation CTFAPICredentials_Validator_Tests

#pragma mark - CredentailTypeUsername
- (void)testUsernameLength
{
    XCTAssert([self validUsername:@"login"] == ValidationWrongCredentials, @"");
    XCTAssert([self validUsername:@"loginn"] == ValidationOK, @"");
    XCTAssert([self validUsername:@"qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklmnbvkk"] == ValidationWrongCredentials, @"");
}

- (void)testUsernameCharacters
{
    XCTAssert([self validUsername:@"login1"] == ValidationOK, @"");
    XCTAssert([self validUsername:@"123456"] == ValidationOK, @"");
    XCTAssert([self validUsername:@"_123456"] == ValidationWrongCredentials, @"");
    XCTAssert([self validUsername:@"_123d5_"] == ValidationWrongCredentials, @"");
    XCTAssert([self validUsername:@"valid!@__l"] == ValidationWrongCredentials, @"");
}

- (ValidationResult)validUsername:(NSString *)username
{
    return [CTFAPILocalCredentialsValidator validateCredential:username withType:CredentialTypeUsername];
}


#pragma mark - CredentialTypePassword
- (void)testPasswordLength
{
    XCTAssert([self validPassword:@"abc"] == ValidationWrongCredentials, @"");
    XCTAssert([self validPassword:@"ancdefgv"] == ValidationOK, @"");
    XCTAssert([self validPassword:@"qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklmnbvkk"] == ValidationWrongCredentials, @"");
}

- (void)testPasswordCharacters
{
    XCTAssert([self validPassword:@"goodPass123!@#avc:)"] == ValidationOK, @"");
    XCTAssert([self validPassword:@"goodPassButWeak"] == ValidationOK, @"");
    XCTAssert([self validPassword:@"§1234567890-="] == ValidationOK, @"");
    XCTAssert([self validPassword:@"!@#$$%%^&*())"] == ValidationOK, @"");
    XCTAssert([self validPassword:@"this is not a password"] == ValidationWrongCredentials, @"");
}

- (ValidationResult)validPassword:(NSString *)password
{
    return [CTFAPILocalCredentialsValidator validateCredential:password withType:CredentialTypePassword];
}


#pragma mark - CredentialTypeEmail
- (void)testEmailCharacters
{
    XCTAssert([self validEmail:@"abc@abcd.pl"] == ValidationOK, @"");
    XCTAssert([self validEmail:@"abc.abcd@abc.abcd.pl"] == ValidationOK, @"");

    XCTAssert([self validEmail:@".%@.-.pl"] == ValidationWrongCredentials, @"");
    XCTAssert([self validEmail:@"@.pl"] == ValidationWrongCredentials, @"");
    XCTAssert([self validEmail:@".pl"] == ValidationWrongCredentials, @"");
    XCTAssert([self validEmail:@"a.pl"] == ValidationWrongCredentials, @"");
    XCTAssert([self validEmail:@"abc@abc"] == ValidationWrongCredentials, @"");
}

- (ValidationResult)validEmail:(NSString *)email
{
    return [CTFAPILocalCredentialsValidator validateCredential:email withType:CredentialTypeEmail];
}

@end
