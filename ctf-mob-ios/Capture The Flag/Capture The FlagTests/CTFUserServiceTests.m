//
//  CTFUserServiceTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFUserService.h"

@interface CTFUserServiceTests : XCTestCase

@end

@implementation CTFUserServiceTests

- (void)testSignUpCredentialsShouldBeValid {
    CredentialsValidationResult result =
    [CTFUserService validateSignUpCredentialsWithUsername:@"login111"
                                             emailAddress:@"login@login.com"
                                                 password:@"login123"
                                               rePassword:@"login123"];
    XCTAssertEqual(result, CredentialsValidationResultOK, @"Should be OK");
}

- (void)testSignUpCredentialsShouldNotPassBecauseOfWrongUsername {
    CredentialsValidationResult result =
    [CTFUserService validateSignUpCredentialsWithUsername:@"l o g i n"
                                             emailAddress:@"login@login.com"
                                                 password:@"password"
                                               rePassword:@"password"];
    XCTAssertEqual(result, CredentialsValidationResultWrongUsername, @"");
}

- (void)testSignUpCredentialsShouldNotPassBecauseOfWrongEmailAddress {
    CredentialsValidationResult result =
    [CTFUserService validateSignUpCredentialsWithUsername:@"login123"
                                             emailAddress:@"this is not an email"
                                                 password:@"password"
                                               rePassword:@"password"];
    XCTAssertEqual(result, CredentialsValidationResultWrongEmailAddress, @"");
}

- (void)testSignUpCredentialsShouldNotPassBecauseOfDifferentPasswords {
    CredentialsValidationResult result =
    [CTFUserService validateSignUpCredentialsWithUsername:@"login123"
                                             emailAddress:@"login@login.com"
                                                 password:@"password1"
                                               rePassword:@"password2"];
    XCTAssertEqual(result, CredentialsValidationResultDifferentPasswords, @"");
}

- (void)testSignUpCredentialsShouldNotPassBecauseOfTooShortPassword {
    CredentialsValidationResult result =
    [CTFUserService validateSignUpCredentialsWithUsername:@"login123"
                                             emailAddress:@"login@login.com"
                                                 password:@"pass1"
                                               rePassword:@"pass1"];
    XCTAssertEqual(result, CredentialsValidationResultWrongPassword, @"");
}

@end
