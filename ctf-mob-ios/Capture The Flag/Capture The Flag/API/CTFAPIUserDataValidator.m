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
    
    /// Configure validator
    TSStringValidator *validator = [TSStringValidator new];
    TSStringValidatorPattern *usernamePattern = [self usernamePattern];
    TSStringValidatorPattern *emailPattern = [self emailPattern];
    TSStringValidatorPattern *passwordPattern = [self passwordPattern];
    
    [validator addPattern:usernamePattern];
    [validator addPattern:emailPattern];
    [validator addPattern:passwordPattern];
    
    /// Add items to validate
    TSStringValidatorItem *usernameItem = [TSStringValidatorItem itemWithString:username patternIdentifier:usernamePattern.identifier allowsEmpty:NO];
    TSStringValidatorItem *emailAddressItem = [TSStringValidatorItem itemWithString:emailAddress patternIdentifier:emailPattern.identifier allowsEmpty:NO];
    TSStringValidatorItem *passwordItem = [TSStringValidatorItem itemWithString:password patternIdentifier:passwordPattern.identifier allowsEmpty:NO];
    
    /// Validate
    StringValidationResult usernameResult = [validator validateItem:usernameItem];
    StringValidationResult emailResult = [validator validateItem:emailAddressItem];
    StringValidationResult passwordResult = [validator validateItem:passwordItem];
    
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
    
    /// Configure validator
    TSStringValidator *validator = [TSStringValidator new];
    TSStringValidatorPattern *usernamePattern = [self usernamePattern];
    TSStringValidatorPattern *passwordPattern = [self passwordPattern];
    
    [validator addPattern:usernamePattern];
    [validator addPattern:passwordPattern];
    
    /// Add items to validate
    TSStringValidatorItem *usernameItem = [TSStringValidatorItem itemWithString:username patternIdentifier:usernamePattern.identifier allowsEmpty:NO];
    TSStringValidatorItem *passwordItem = [TSStringValidatorItem itemWithString:password patternIdentifier:passwordPattern.identifier allowsEmpty:NO];
    
    /// Validate
    StringValidationResult usernameResult = [validator validateItem:usernameItem];
    StringValidationResult passwordResult = [validator validateItem:passwordItem];

    
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
    
    /// Configure validator
    TSStringValidator *validator = [TSStringValidator new];
    TSStringValidatorPattern *namePattern = [self namePattern];
    TSStringValidatorPattern *nickPattern = [self nickPattern];
    TSStringValidatorPattern *emailPattern = [self emailPattern];
    
    [validator addPattern:namePattern];
    [validator addPattern:nickPattern];
    [validator addPattern:emailPattern];

    /// Add items to validate
    TSStringValidatorItem *firstNameItem = [TSStringValidatorItem itemWithString:firstName patternIdentifier:namePattern.identifier];
    TSStringValidatorItem *lastNameItem = [TSStringValidatorItem itemWithString:lastName patternIdentifier:namePattern.identifier];
    TSStringValidatorItem *nickItem = [TSStringValidatorItem itemWithString:nick patternIdentifier:nickPattern.identifier];
    TSStringValidatorItem *emailItem = [TSStringValidatorItem itemWithString:email patternIdentifier:emailPattern.identifier allowsEmpty:NO];
    
    /// Validate
    StringValidationResult firstNameResult = [validator validateItem:firstNameItem];
    StringValidationResult lastNameResult = [validator validateItem:lastNameItem];
    StringValidationResult nickResult = [validator validateItem:nickItem];
    StringValidationResult emailResult = [validator validateItem:emailItem];
    
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
