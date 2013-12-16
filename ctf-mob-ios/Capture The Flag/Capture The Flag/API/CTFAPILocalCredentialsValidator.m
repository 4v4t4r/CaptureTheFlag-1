//
//  CTFAPICredentials.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPILocalCredentialsValidator.h"
#import "CTFAPILocalCredentialsValidator+Validator.h"

@implementation CTFAPILocalCredentialsValidator

+ (CredentialsValidationResult)validateSignUpCredentialsWithUsername:(NSString *)username
                                                        emailAddress:(NSString *)emailAddress
                                                            password:(NSString *)password
                                                          rePassword:(NSString *)repassword {
    if (!username || username.length == 0 ||
        !emailAddress || emailAddress.length == 0 ||
        !password || password.length == 0 ||
        !repassword || repassword.length == 0) {
        return CredentialsValidationResultFailure;
    }
    
    ValidationResult usernameResult = [CTFAPILocalCredentialsValidator validateCredential:username
                                                                withType:CredentialTypeUsername];
    ValidationResult emailResult = [CTFAPILocalCredentialsValidator validateCredential:emailAddress
                                                             withType:CredentialTypeEmail];
    
    ValidationResult passwordResult = ValidationWrongCredentials;
    BOOL theSamePasswords = [password isEqualToString:repassword];
    if (theSamePasswords)
        passwordResult = [CTFAPILocalCredentialsValidator validateCredential:password
                                                   withType:CredentialTypePassword];
    
    CredentialsValidationResult result = CredentialsValidationResultUndefined;
    if (usernameResult == ValidationOK &&
        emailResult == ValidationOK &&
        passwordResult == ValidationOK) {
        result = CredentialsValidationResultOK;
    } else if (usernameResult != ValidationOK) {
        result = CredentialsValidationResultWrongUsername;
    } else if (emailResult != ValidationOK) {
        result = CredentialsValidationResultWrongEmailAddress;
    } else if (!theSamePasswords) {
        result = CredentialsValidationResultDifferentPasswords;
    } else if (passwordResult != ValidationOK) {
        result = CredentialsValidationResultWrongPassword;
    }
    
    return result;
}

+ (CredentialsValidationResult)validateSignInCredentialsWithUsername:(NSString *)username
                                                            password:(NSString *)password {
    if (!username || username.length == 0 ||
        !password || password.length == 0) {
        return CredentialsValidationResultFailure;
    }
    
    ValidationResult usernameResult =
    [CTFAPILocalCredentialsValidator validateCredential:username withType:CredentialTypeUsername];
    
    ValidationResult passwordResult =
    [CTFAPILocalCredentialsValidator validateCredential:password withType:CredentialTypePassword];
    
    CredentialsValidationResult result = CredentialsValidationResultUndefined;
    if (usernameResult == ValidationOK &&
        passwordResult == ValidationOK) {
        result = CredentialsValidationResultOK;
    } else if (usernameResult != ValidationOK) {
        result = CredentialsValidationResultWrongUsername;
    } else if (passwordResult != ValidationOK) {
        result = CredentialsValidationResultWrongPassword;
    }
    
    return result;
}

@end
