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

- (StringValidationResult)resultForPattern:(TSStringValidatorPattern *)pattern string:(NSString *)string {
    TSStringValidator *validator = [TSStringValidator new];
    [validator addPattern:pattern];
    TSStringValidatorItem *item = [TSStringValidatorItem itemWithString:string patternIdentifier:pattern.identifier allowsEmpty:NO];
    
    return [validator validateItem:item];
}

- (StringValidationResult)validUsername:(NSString *)string {
    return [self resultForPattern:[CTFAPIUserDataValidator usernamePattern] string:string];
}

- (StringValidationResult)validPassword:(NSString *)string {
    return [self resultForPattern:[CTFAPIUserDataValidator passwordPattern] string:string];
}

- (StringValidationResult)validEmail:(NSString *)string {
    return [self resultForPattern:[CTFAPIUserDataValidator emailPattern] string:string];
}

- (StringValidationResult)validName:(NSString *)string {
    return [self resultForPattern:[CTFAPIUserDataValidator namePattern] string:string];
}

- (StringValidationResult)validNick:(NSString *)string {
    return [self resultForPattern:[CTFAPIUserDataValidator nickPattern] string:string];
}

#pragma mark - Regexps
- (void)testThatEmailPatternIsNotNil {
    XCTAssertNotNil([CTFAPIUserDataValidator emailPattern], @"");
}

- (void)testThatUsernamePatternIsNotNil {
    XCTAssertNotNil([CTFAPIUserDataValidator usernamePattern], @"");
}

- (void)testThatPasswordPatternIsNotNil {
    XCTAssertNotNil([CTFAPIUserDataValidator passwordPattern], @"");
}

- (void)testThatNamePatternIsNotNil {
    XCTAssertNotNil([CTFAPIUserDataValidator namePattern], @"");
}

- (void)testThatNickPatternIsNotNil {
    XCTAssertNotNil([CTFAPIUserDataValidator nickPattern], @"");
}

- (void)testThatEmailRegexpIsUsedInEmailPattern {
    TSStringValidatorPattern *pattern = [CTFAPIUserDataValidator emailPattern];
    XCTAssertEqualObjects(pattern.patternString, emailRegexp, @"");
    XCTAssertEqualObjects(pattern.identifier, emailIdentifier, @"");
}

- (void)testThatUsernameRegexpIsUsedInUsernamePattern {
    TSStringValidatorPattern *pattern = [CTFAPIUserDataValidator usernamePattern];
    XCTAssertEqualObjects(pattern.patternString, usernameRegexp, @"");
    XCTAssertEqualObjects(pattern.identifier, usernameIdentifier, @"");
}

- (void)testThatPasswordRegexpIsUsedInPasswordPattern {
    TSStringValidatorPattern *pattern = [CTFAPIUserDataValidator passwordPattern];
    XCTAssertEqualObjects(pattern.patternString, passwordRegexp, @"");
    XCTAssertEqualObjects(pattern.identifier, passwordIdentifier, @"");
}

- (void)testThatNameRegexpIsUsedInPasswordPattern {
    TSStringValidatorPattern *pattern = [CTFAPIUserDataValidator namePattern];
    XCTAssertEqualObjects(pattern.patternString, nameRegexp, @"");
    XCTAssertEqualObjects(pattern.identifier, nameIdentifier, @"");
}

- (void)testThatNickRegexpIsUsedInPasswordPattern {
    TSStringValidatorPattern *pattern = [CTFAPIUserDataValidator nickPattern];
    XCTAssertEqualObjects(pattern.patternString, nickRegexp, @"");
    XCTAssertEqualObjects(pattern.identifier, nickIdentifier, @"");
}

#pragma mark +usernamePattern
- (void)testThatUsernameIsTooShort {
    XCTAssertEqual([self validUsername:@"login"], StringValidationResultFailure, @"");
}

- (void)testThatUsernameIsOk {
    XCTAssertEqual([self validUsername:@"loginn"], StringValidationResultOK, @"");
}

- (void)testThatUsernameIsTooLong {
    XCTAssertEqual([self validUsername:@"qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklmnbvkk"], StringValidationResultFailure, @"");
}

- (void)testThatUsernameCanHasSomeSpecialCharacters {
    XCTAssertEqual([self validUsername:@"login1"], StringValidationResultOK, @"");
    XCTAssertEqual([self validUsername:@"123456"], StringValidationResultOK, @"");
    XCTAssertEqual([self validUsername:@"_123456"], StringValidationResultFailure, @"");
    XCTAssertEqual([self validUsername:@"_123d5_"], StringValidationResultFailure, @"");
    XCTAssertEqual([self validUsername:@"valid!@__l"], StringValidationResultFailure, @"");
}


#pragma mark +passwordPattern
- (void)testThatPasswordIsTooShort {
    XCTAssertEqual([self validPassword:@"abc"], StringValidationResultFailure, @"");
}

- (void)testThatPasswordIsOK {
    XCTAssertEqual([self validPassword:@"ancdefgv"], StringValidationResultOK, @"");
}

- (void)testThatPasswordIsTooLong {
    XCTAssertEqual([self validPassword:@"qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklmnbvkk"], StringValidationResultFailure, @"");
}

- (void)testThatPasswordCanHasSomeSpecialCharacters {
    XCTAssertEqual([self validPassword:@"goodPass123!@#avc:)"], StringValidationResultOK, @"");
    XCTAssertEqual([self validPassword:@"goodPassButWeak"], StringValidationResultOK, @"");
    XCTAssertEqual([self validPassword:@"§1234567890-="], StringValidationResultOK, @"");
    XCTAssertEqual([self validPassword:@"!@#$$%%^&*())"], StringValidationResultOK, @"");
    XCTAssertEqual([self validPassword:@"this is not a password"], StringValidationResultFailure, @"");
}


#pragma mark +emailPattern
- (void)testThatEmailIsOK {
    XCTAssertEqual([self validEmail:@"abc@abcd.pl"], StringValidationResultOK, @"");
    XCTAssertEqual([self validEmail:@"abc.abcd@abc.abcd.pl"], StringValidationResultOK, @"");
}

- (void)testThatEmailCanNotBeShortAndHasWrongStructure {
    XCTAssertEqual([self validEmail:@".%@.-.pl"], StringValidationResultFailure, @"");
    XCTAssertEqual([self validEmail:@"@.pl"], StringValidationResultFailure, @"");
    XCTAssertEqual([self validEmail:@".pl"], StringValidationResultFailure, @"");
    XCTAssertEqual([self validEmail:@"a.pl"], StringValidationResultFailure, @"");
    XCTAssertEqual([self validEmail:@"abc@abc"], StringValidationResultFailure, @"");
}


#pragma mark +namePattern
- (void)testThatNameIsOK {
    XCTAssertEqual([self validName:@"Tomasz"], StringValidationResultOK, @"");
    XCTAssertEqual([self validName:@"Cing Ciang"], StringValidationResultOK, @"");
}

- (void)testThatNameIsNotCorrect {
    XCTAssertEqual([self validName:@"Tomasz9993332"], StringValidationResultFailure, @"");
    XCTAssertEqual([self validName:@"Tom.#aasd"], StringValidationResultFailure, @"");
}


#pragma mark +nickPattern
- (void)testThatNickIsOK {
    XCTAssertEqual([self validNick:@"This is a nick"], StringValidationResultOK, @"");
    XCTAssertEqual([self validNick:@"Mordor5"], StringValidationResultOK, @"");
}

@end
