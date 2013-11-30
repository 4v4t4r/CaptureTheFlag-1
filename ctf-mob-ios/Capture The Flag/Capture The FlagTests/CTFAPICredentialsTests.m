//
//  CTFUserServiceTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFAPICredentials.h"

@interface CTFAPICredentialsTests : XCTestCase

@end

@implementation CTFAPICredentialsTests


#pragma mark - SignUp
- (void)testSignUpCredentialsShouldBeValid {
    CredentialsValidationResult result =
    [CTFAPICredentials validateSignUpCredentialsWithUsername:@"login111"
                                             emailAddress:@"login@login.com"
                                                 password:@"login123"
                                               rePassword:@"login123"];
    XCTAssertEqual(result, CredentialsValidationResultOK, @"Should be OK");
}

- (void)testSignUpCredentialsShouldNotPassBecauseOfWrongUsername {
    CredentialsValidationResult result =
    [CTFAPICredentials validateSignUpCredentialsWithUsername:@"l o g i n"
                                             emailAddress:@"login@login.com"
                                                 password:@"password"
                                               rePassword:@"password"];
    XCTAssertEqual(result, CredentialsValidationResultWrongUsername, @"");
}

- (void)testSignUpCredentialsShouldNotPassBecauseOfWrongEmailAddress {
    CredentialsValidationResult result =
    [CTFAPICredentials validateSignUpCredentialsWithUsername:@"login123"
                                             emailAddress:@"this is not an email"
                                                 password:@"password"
                                               rePassword:@"password"];
    XCTAssertEqual(result, CredentialsValidationResultWrongEmailAddress, @"");
}

- (void)testSignUpCredentialsShouldNotPassBecauseOfDifferentPasswords {
    CredentialsValidationResult result =
    [CTFAPICredentials validateSignUpCredentialsWithUsername:@"login123"
                                             emailAddress:@"login@login.com"
                                                 password:@"password1"
                                               rePassword:@"password2"];
    XCTAssertEqual(result, CredentialsValidationResultDifferentPasswords, @"");
}

- (void)testSignUpCredentialsShouldNotPassBecauseOfTooShortPassword {
    CredentialsValidationResult result =
    [CTFAPICredentials validateSignUpCredentialsWithUsername:@"login123"
                                             emailAddress:@"login@login.com"
                                                 password:@"pass1"
                                               rePassword:@"pass1"];
    XCTAssertEqual(result, CredentialsValidationResultWrongPassword, @"");
}


#pragma mark - SignIn
- (void)testSignInCredentialsShouldBeValid {
    CredentialsValidationResult result =
    [CTFAPICredentials validateSignInCredentialsWithUsername:@"login123" password:@"password123"];
    XCTAssertEqual(result, CredentialsValidationResultOK, @"");
}

- (void)testSignInCredentialsShouldNotPassBecauseOfWrongUsername {
    CredentialsValidationResult result =
    [CTFAPICredentials validateSignInCredentialsWithUsername:@"l o g i n " password:@"password123"];
    XCTAssertEqual(result, CredentialsValidationResultWrongUsername, @"");
}

- (void)testSignInCredentialsShouldNotPassBecauseOfWrongPassword {
    CredentialsValidationResult result =
    [CTFAPICredentials validateSignInCredentialsWithUsername:@"login123" password:@"pass1"];
    XCTAssertEqual(result, CredentialsValidationResultWrongPassword, @"Password should be too short");
}

@end
