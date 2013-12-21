//
//  CTFUserServiceTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFAPILocalCredentialsValidator.h"

@interface CTFAPICredentialsTests : XCTestCase

@end

@implementation CTFAPICredentialsTests


#pragma mark - validateSignUpCredentialsWithUsername:emailAddress:password:rePassword:
- (void)testSignUpCredentialsShouldBeValid {
    CredentialsValidationResult result =
    [CTFAPILocalCredentialsValidator validateSignUpCredentialsWithUsername:@"login111"
                                             emailAddress:@"login@login.com"
                                                 password:@"login123"
                                               rePassword:@"login123"];
    XCTAssertEqual(result, CredentialsValidationResultOK, @"Should be OK");
}

- (void)testSignUpCredentialsShouldNotPassBecauseOfWrongUsername {
    CredentialsValidationResult result =
    [CTFAPILocalCredentialsValidator validateSignUpCredentialsWithUsername:@"l o g i n"
                                             emailAddress:@"login@login.com"
                                                 password:@"password"
                                               rePassword:@"password"];
    XCTAssertEqual(result, CredentialsValidationResultIncorrectUsername, @"");
}

- (void)testSignUpCredentialsShouldNotPassBecauseOfWrongEmailAddress {
    CredentialsValidationResult result =
    [CTFAPILocalCredentialsValidator validateSignUpCredentialsWithUsername:@"login123"
                                             emailAddress:@"this is not an email"
                                                 password:@"password"
                                               rePassword:@"password"];
    XCTAssertEqual(result, CredentialsValidationResultIncorrectEmailAddress, @"");
}

- (void)testSignUpCredentialsShouldNotPassBecauseOfDifferentPasswords {
    CredentialsValidationResult result =
    [CTFAPILocalCredentialsValidator validateSignUpCredentialsWithUsername:@"login123"
                                             emailAddress:@"login@login.com"
                                                 password:@"password1"
                                               rePassword:@"password2"];
    XCTAssertEqual(result, CredentialsValidationResultDifferentPasswords, @"");
}

- (void)testSignUpCredentialsShouldNotPassBecauseOfTooShortPassword {
    CredentialsValidationResult result =
    [CTFAPILocalCredentialsValidator validateSignUpCredentialsWithUsername:@"login123"
                                             emailAddress:@"login@login.com"
                                                 password:@"pass1"
                                               rePassword:@"pass1"];
    XCTAssertEqual(result, CredentialsValidationResultIncorrectPassword, @"");
}

- (void)testSignUpCredentialsShouldNotPassBecauseOfEmptyValue {
    CredentialsValidationResult result =
    [CTFAPILocalCredentialsValidator validateSignUpCredentialsWithUsername:@""
                                                emailAddress:nil
                                                    password:@""
                                                  rePassword:nil];
    XCTAssertEqual(result, CredentialsValidationResultFailure, @"");
}


#pragma mark - validateSignInCredentialsWithUsername:password:
- (void)testSignInCredentialsShouldBeValid {
    CredentialsValidationResult result =
    [CTFAPILocalCredentialsValidator validateSignInCredentialsWithUsername:@"login123" password:@"password123"];
    XCTAssertEqual(result, CredentialsValidationResultOK, @"");
}

- (void)testSignInCredentialsShouldNotPassBecauseOfWrongUsername {
    CredentialsValidationResult result =
    [CTFAPILocalCredentialsValidator validateSignInCredentialsWithUsername:@"l o g i n " password:@"password123"];
    XCTAssertEqual(result, CredentialsValidationResultIncorrectUsername, @"");
}

- (void)testSignInCredentialsShouldNotPassBecauseOfWrongPassword {
    CredentialsValidationResult result =
    [CTFAPILocalCredentialsValidator validateSignInCredentialsWithUsername:@"login123" password:@"pass1"];
    XCTAssertEqual(result, CredentialsValidationResultIncorrectPassword, @"Password should be too short");
}

- (void)testSignInCredentialsShouldNotPassBecauseOfEmptyValue {
    CredentialsValidationResult result =
    [CTFAPILocalCredentialsValidator validateSignInCredentialsWithUsername:@"" password:nil];
    
    XCTAssertEqual(result, CredentialsValidationResultFailure, @"");
}


#pragma mark - validateUserCredentialsForUpdateWithFirstName:lastName:nick:emailAddress:
- (void)testThatUpdateCredentialsShouldBeValid {
    CredentialsValidationResult result = [CTFAPILocalCredentialsValidator validateUserCredentialsForUpdateWithFirstName:@"username" lastName:@"lastname" nick:@"thisisnick" emailAddress:@"email@address.com"];
    XCTAssertEqual(result, CredentialsValidationResultOK, @"");
}

- (void)testThatUpdateCredentialsShouldReturnFailureWhenFirstNameIsNil {
    CredentialsValidationResult firstNameNil =
    [CTFAPILocalCredentialsValidator validateUserCredentialsForUpdateWithFirstName:nil lastName:@"" nick:@"" emailAddress:@""];
    XCTAssertEqual(firstNameNil, CredentialsValidationResultFailure, @"");
}

- (void)testThatUpdateCredentialsShouldReturnFailureWhenLastNameIsNil {
    CredentialsValidationResult lastNameNil =
    [CTFAPILocalCredentialsValidator validateUserCredentialsForUpdateWithFirstName:@"" lastName:nil nick:@"" emailAddress:@""];
    XCTAssertEqual(lastNameNil, CredentialsValidationResultFailure, @"");
}

- (void)testThatUpdateCredentialsShouldReturnFailureWhenNickIsIn {
    CredentialsValidationResult nickNil =
    [CTFAPILocalCredentialsValidator validateUserCredentialsForUpdateWithFirstName:@"" lastName:@"" nick:nil emailAddress:@""];
    XCTAssertEqual(nickNil, CredentialsValidationResultFailure, @"");
}

- (void)testThatUpdateCredentialsShouldReturnFailureWhenEmailIsNil {
    CredentialsValidationResult emailNil =
    [CTFAPILocalCredentialsValidator validateUserCredentialsForUpdateWithFirstName:@"" lastName:@"" nick:@"" emailAddress:nil];
    XCTAssertEqual(emailNil, CredentialsValidationResultFailure, @"");
}

- (void)testThatUpdateCredentialsShouldReturnIncorrectWhenFirstNameIsEmpty {
    CredentialsValidationResult result =
    [CTFAPILocalCredentialsValidator validateUserCredentialsForUpdateWithFirstName:@"" lastName:@"someting" nick:@"something" emailAddress:@"something"];
    XCTAssertEqual(result, CredentialsValidationResultIncorrectFirstName, @"");
}

@end
