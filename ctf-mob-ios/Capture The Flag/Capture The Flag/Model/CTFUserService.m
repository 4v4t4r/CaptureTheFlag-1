//
//  CTFUserService.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFUserService.h"
#import "CTFCredentialsValidator.h"

@implementation CTFUserService

+ (CredentialsValidationResult)validateSignUpCredentialsWithUsername:(NSString *)username
                                                        emailAddress:(NSString *)emailAddress
                                                            password:(NSString *)password
                                                          rePassword:(NSString *)repassword {
    ValidationResult usernameResult = [CTFCredentialsValidator validCredential:username withType:CredentialTypeUsername];
    ValidationResult emailResult = [CTFCredentialsValidator validCredential:emailAddress withType:CredentialTypeEmail];
    
    ValidationResult passwordResult = ValidationWrongCredentials;
    BOOL theSamePasswords = [password isEqualToString:repassword];
    if (theSamePasswords)
        passwordResult = [CTFCredentialsValidator validCredential:password withType:CredentialTypePassword];
    
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

@end
