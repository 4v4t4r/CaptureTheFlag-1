//
//  CTFAPICredentials.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIUserDataValidator.h"
#import "CTFAPIUserDataValidator+RegexpPatterns.h"
#import "TSStringValidator.h"
#import "TSStringValidatorItem.h"
#import "TSStringValidatorPattern.h"

@implementation CTFAPIUserDataValidator

+ (CredentialsValidationResult)validateSignUpCredentialsWithUsername:(NSString *)username
                                                        emailAddress:(NSString *)emailAddress
                                                            password:(NSString *)password
                                                          rePassword:(NSString *)repassword {
    /// If some parameter is nil
    if (!username || !emailAddress || !password || !repassword) {
        return CredentialsValidationResultFailure;
    }
    
    /// If password aren't the same
    if (![password isEqualToString:repassword]) {
        return CredentialsValidationResultDifferentPasswords;
    }
    
    /// Validate
    TSStringValidator *validator = [TSStringValidator new];
    StringValidationResult usernameResult = [validator validateString:username withPattern:[self usernamePattern].patternString allowsEmpty:NO];
    StringValidationResult emailResult = [validator validateString:emailAddress withPattern:[self emailPattern].patternString allowsEmpty:NO];
    StringValidationResult passwordResult = [validator validateString:password withPattern:[self passwordPattern].patternString allowsEmpty:NO];
    
    /// If fields are empty
    if (usernameResult == StringValidationResultEmptyField ||
        emailResult == StringValidationResultEmptyField ||
        passwordResult == StringValidationResultEmptyField) {
        return CredentialsValidationResultFailure;
    }
    
    /// If fields have been validated and aren't empty
    CredentialsValidationResult result = CredentialsValidationResultFailure;
    if (usernameResult == StringValidationResultOK &&
        emailResult == StringValidationResultOK &&
        passwordResult == StringValidationResultOK) {
        result = CredentialsValidationResultOK;
    } else if (usernameResult != StringValidationResultOK) {
        result = CredentialsValidationResultIncorrectUsername;
    } else if (emailResult != StringValidationResultOK) {
        result = CredentialsValidationResultIncorrectEmailAddress;
    } else if (passwordResult != StringValidationResultOK) {
        result = CredentialsValidationResultIncorrectPassword;
    }
    
    return result;
}

+ (CredentialsValidationResult)validateSignInCredentialsWithUsername:(NSString *)username
                                                            password:(NSString *)password {
    if (!username || !password) {
        return CredentialsValidationResultFailure;
    }
    
    /// Validate
    TSStringValidator *validator = [TSStringValidator new];
    StringValidationResult usernameResult = [validator validateString:username withPattern:[self usernamePattern].patternString allowsEmpty:NO];
    StringValidationResult passwordResult = [validator validateString:password withPattern:[self passwordPattern].patternString allowsEmpty:NO];

    CredentialsValidationResult result = CredentialsValidationResultOK;
    
    if (usernameResult != StringValidationResultOK) {
        result = CredentialsValidationResultIncorrectUsername;
    } else if (passwordResult != StringValidationResultOK) {
        result = CredentialsValidationResultIncorrectPassword;
    }
    
    return result;
}

+ (CredentialsValidationResult)validateUserCredentialsForUpdateWithFirstName:(NSString *)firstName lastName:(NSString *)lastName nick:(NSString *)nick emailAddress:(NSString *)email {
    
    if (!firstName || !lastName || !nick || !email) {
        return CredentialsValidationResultFailure;
    }
    
    /// Validate
    TSStringValidator *validator = [TSStringValidator new];
    TSStringValidatorPattern *namePattern = [self namePattern];
    
    StringValidationResult firstNameResult = [validator validateString:firstName withPattern:namePattern.patternString allowsEmpty:YES];
    StringValidationResult lastNameResult = [validator validateString:lastName withPattern:namePattern.patternString allowsEmpty:YES];
    StringValidationResult nickResult = [validator validateString:nick withPattern:[self nickPattern].patternString allowsEmpty:YES];
    StringValidationResult emailResult = [validator validateString:email withPattern:[self emailPattern].patternString allowsEmpty:NO];
    
    CredentialsValidationResult result = CredentialsValidationResultOK;
    
    if (firstNameResult != StringValidationResultOK) {
        result = CredentialsValidationResultIncorrectFirstName;
    } else if (lastNameResult != StringValidationResultOK) {
        result = CredentialsValidationResultIncorrectLastName;
    } else if (nickResult != StringValidationResultOK) {
        result = CredentialsValidationResultIncorrectNick;
    } else if (emailResult != StringValidationResultOK) {
        result = CredentialsValidationResultIncorrectEmailAddress;
    }
    
    return result;
}

@end
