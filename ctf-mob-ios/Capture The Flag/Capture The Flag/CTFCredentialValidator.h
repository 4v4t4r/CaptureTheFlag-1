//
//  CTFCredentialValidator.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef enum
{
    CredentialTypeUsername,
    CredentialTypePassword
} CredentialType;

typedef enum
{
    ValidationOK,
    ValidationWrongCredentials,
    ValidationEmptyField
} ValidationResult;

@interface CTFCredentialValidator : NSObject

+ (ValidationResult)validCredential:(NSString *)credential withType:(CredentialType)type;

@end
