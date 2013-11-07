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

@interface CTFCredentialValidator : NSObject

+ (BOOL)validCredential:(NSString *)credential withType:(CredentialType)type;

@end
