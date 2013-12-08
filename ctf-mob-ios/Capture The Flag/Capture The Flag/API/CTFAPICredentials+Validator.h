//
//  CTFAPICredentials+Validator.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPICredentials.h"
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

@interface CTFAPICredentials (Validator)

+ (ValidationResult)validateCredential:(NSString *)credential withType:(CredentialType)type;

@end
