//
//  CTFAPICredentials+Validator.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPILocalCredentialsValidator.h"
/** This is a category of CTFAPICredentials used to validate credentials */

typedef enum
{
    CredentialTypeUsername,
    CredentialTypePassword,
    CredentialTypeEmail
} CredentialType;

typedef enum
{
    ValidationUndefined         = -1,
    ValidationOK                = 0,
    ValidationWrongCredentials
} ValidationResult;

@interface CTFAPILocalCredentialsValidator (Validator)
#warning [tsu] There should be parameter which says you if value can be empty. Maybe there should be class with such attribute
/**
 @define Method validate credential with type.
 @abstract Return ValidationResultOK if value is correct typed, otherwise return ValidationResultWrongCredentials
 @discussion Class used to validate credential.
 @param credential Credential to check.
 @param type Type of credential to check.
 */
+ (ValidationResult)validateCredential:(NSString *)credential withType:(CredentialType)type;

@end
