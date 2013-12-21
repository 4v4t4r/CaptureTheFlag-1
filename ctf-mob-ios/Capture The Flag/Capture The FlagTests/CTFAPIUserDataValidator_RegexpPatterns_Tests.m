//
//  CTFCredentialValidatorTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFAPIUserDataValidator.h"
#import "CTFAPIUserDataValidator+RegexpPatterns.h"
#import "TSStringValidator.h"
#import "TSStringValidatorItem.h"
#import "TSStringValidatorPattern.h"

@interface CTFAPIUserDataValidator_RegexpPatterns_Tests : XCTestCase
@end

@implementation CTFAPIUserDataValidator_RegexpPatterns_Tests

#pragma mark - UsernamePattern
- (void)testUsernameLength
{
    XCTAssert([self validUsername:@"login"] == StringValidationResultFailure, @"");
    XCTAssert([self validUsername:@"loginn"] == StringValidationResultOK, @"");
    XCTAssert([self validUsername:@"qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklmnbvkk"] == StringValidationResultFailure, @"");
}

- (void)testUsernameCharacters
{
    XCTAssert([self validUsername:@"login1"] == StringValidationResultOK, @"");
    XCTAssert([self validUsername:@"123456"] == StringValidationResultOK, @"");
    XCTAssert([self validUsername:@"_123456"] == StringValidationResultFailure, @"");
    XCTAssert([self validUsername:@"_123d5_"] == StringValidationResultFailure, @"");
    XCTAssert([self validUsername:@"valid!@__l"] == StringValidationResultFailure, @"");
}

- (StringValidationResult)validUsername:(NSString *)username
{
    TSStringValidator *validator = [TSStringValidator new];
    TSStringValidatorPattern *pattern = [CTFAPIUserDataValidator usernamePattern];
    [validator addPattern:pattern];
    TSStringValidatorItem *item = [TSStringValidatorItem itemWithString:username patternIdentifier:pattern.identifier allowsEmpty:NO];
    
    return [validator validateItem:item];
}


#pragma mark - PasswordPattern
- (void)testPasswordLength
{
    XCTAssert([self validPassword:@"abc"] == StringValidationResultFailure, @"");
    XCTAssert([self validPassword:@"ancdefgv"] == StringValidationResultOK, @"");
    XCTAssert([self validPassword:@"qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklmnbvkk"] == StringValidationResultFailure, @"");
}

- (void)testPasswordCharacters
{
    XCTAssert([self validPassword:@"goodPass123!@#avc:)"] == StringValidationResultOK, @"");
    XCTAssert([self validPassword:@"goodPassButWeak"] == StringValidationResultOK, @"");
    XCTAssert([self validPassword:@"§1234567890-="] == StringValidationResultOK, @"");
    XCTAssert([self validPassword:@"!@#$$%%^&*())"] == StringValidationResultOK, @"");
    XCTAssert([self validPassword:@"this is not a password"] == StringValidationResultFailure, @"");
}

- (StringValidationResult)validPassword:(NSString *)password
{
    TSStringValidator *validator = [TSStringValidator new];
    TSStringValidatorPattern *pattern = [CTFAPIUserDataValidator passwordPattern];
    [validator addPattern:pattern];
    TSStringValidatorItem *item = [TSStringValidatorItem itemWithString:password patternIdentifier:pattern.identifier allowsEmpty:NO];

    return [validator validateItem:item];
}


#pragma mark - EmailPattern
- (void)testEmailCharacters
{
    XCTAssert([self validEmail:@"abc@abcd.pl"] == StringValidationResultOK, @"");
    XCTAssert([self validEmail:@"abc.abcd@abc.abcd.pl"] == StringValidationResultOK, @"");

    XCTAssert([self validEmail:@".%@.-.pl"] == StringValidationResultFailure, @"");
    XCTAssert([self validEmail:@"@.pl"] == StringValidationResultFailure, @"");
    XCTAssert([self validEmail:@".pl"] == StringValidationResultFailure, @"");
    XCTAssert([self validEmail:@"a.pl"] == StringValidationResultFailure, @"");
    XCTAssert([self validEmail:@"abc@abc"] == StringValidationResultFailure, @"");
}

- (StringValidationResult)validEmail:(NSString *)email
{
    TSStringValidator *validator = [TSStringValidator new];
    TSStringValidatorPattern *pattern = [CTFAPIUserDataValidator emailPattern];
    [validator addPattern:pattern];
    TSStringValidatorItem *item = [TSStringValidatorItem itemWithString:email patternIdentifier:pattern.identifier allowsEmpty:NO];
    
    return [validator validateItem:item];}

@end
