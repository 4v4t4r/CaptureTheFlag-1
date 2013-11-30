//
//  CTFUserService.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef enum {
    CredentialsValidationResultUndefined,
    CredentialsValidationResultOK,
    
    CredentialsValidationResultWrongUsername,
    CredentialsValidationResultWrongEmailAddress,
    CredentialsValidationResultDifferentPasswords,
    CredentialsValidationResultWrongPassword
} CredentialsValidationResult;

@interface CTFUserService : NSObject

+ (CredentialsValidationResult)validateSignUpCredentialsWithUsername:(NSString *)username
                                                        emailAddress:(NSString *)emailAddress
                                                            password:(NSString *)password
                                                          rePassword:(NSString *)repassword;

@end
