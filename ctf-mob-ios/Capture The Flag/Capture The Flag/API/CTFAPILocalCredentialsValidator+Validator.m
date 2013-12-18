//
//  CTFAPICredentials+Validator.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPILocalCredentialsValidator+Validator.h"

@implementation CTFAPILocalCredentialsValidator (Validator)

+ (ValidationResult)validateCredential:(NSString *)credential withType:(CredentialType)type
{
    ValidationResult result = ValidationUndefined;
    if (!credential || credential.length == 0)
        return result;
    
    NSString *pattern = [self _patternForType:type];
    
    NSError *error = nil;
    NSRegularExpression *regex = [[NSRegularExpression alloc]
                                  initWithPattern:pattern
                                  options:NSRegularExpressionCaseInsensitive
                                  error:&error];
    
    NSRange credentialRange = NSMakeRange(0, credential.length);
    NSRange matchRange = [regex rangeOfFirstMatchInString:credential options:0 range:credentialRange];
    
    if (NSEqualRanges(matchRange, credentialRange))
        result = ValidationOK;
    else
        result = ValidationWrongCredentials;
    
    return result;
}

+ (NSString *)_patternForType:(CredentialType)type
{
    NSString *pattern = nil;
    switch (type) {
        case CredentialTypeUsername:
            pattern = @"[a-zA-Z0-9@.+-]{6,50}";
            break;
            
        case CredentialTypePassword:
            pattern = @"\\S{6,50}";
            break;
            
        case CredentialTypeEmail:
            pattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)+$";
            break;
    }
    return pattern;
}


@end
