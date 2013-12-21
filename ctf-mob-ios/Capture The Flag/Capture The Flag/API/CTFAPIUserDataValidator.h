//
//  CTFAPICredentials.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef enum {
    CredentialsValidationResultFailure,
    CredentialsValidationResultOK,
    
    CredentialsValidationResultIncorrectUsername,
    CredentialsValidationResultIncorrectEmailAddress,
    CredentialsValidationResultIncorrectPassword,
    CredentialsValidationResultIncorrectFirstName,
    CredentialsValidationResultIncorrectLastName,
    CredentialsValidationResultIncorrectNick,
    
    CredentialsValidationResultDifferentPasswords,
} CredentialsValidationResult;

@interface CTFAPIUserDataValidator : NSObject

/**
 @define A CredentialsValidationResult value define if passed credentials for registration are correct.
 @abstract Return CredentialsValidationResultOK if all passed credentials are valid. Otherwise return other not-valid state.
 @discussion Method used to validate user credentials in registration process (locally).
 @param username The username value to valid.
 @param emailAddress The email address value to valid.
 @param password The password value to valid.
 @param repassword It should be a password from second field of registration form.
 */
+ (CredentialsValidationResult)validateSignUpCredentialsWithUsername:(NSString *)username
                                                        emailAddress:(NSString *)emailAddress
                                                            password:(NSString *)password
                                                          rePassword:(NSString *)repassword;

/**
 @define A CredentialsValidationResult value define if passed credentials for sign in are correct.
 @abstract Return CredentialsValidationResultOK if all passed credentials are valid. Otherwise return other non-valid state.
 @discussion Method used to validate user credentials in sign in process (locally).
 @param username The username value to valid.
 @param password The password value to valid.
 */
+ (CredentialsValidationResult)validateSignInCredentialsWithUsername:(NSString *)username
                                                            password:(NSString *)password;

/**
 @define CredentialsValidationResult value define if passed credentials for update user info are correct.
 @abstract Return CredentialsValidationResultOK if all passed credentials are valid. Otherwise return other non-valid state.
 @discussion Method used to validate user information before update
 @param firstName User's first name (can be empty but not nil)
 @param lastName User's last name (can be empty but not nil)
 @param nick User's nick (can be empty but not nil)
 @param emailAddress USer's email address
 */
+ (CredentialsValidationResult)validateUserCredentialsForUpdateWithFirstName:(NSString *)firstName lastName:(NSString *)lastName nick:(NSString *)nick emailAddress:(NSString *)email;

@end