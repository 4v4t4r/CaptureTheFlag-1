//
//  CTFCredentialValidator.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFCredentialValidator.h"

@implementation CTFCredentialValidator

+ (ValidationResult)validCredential:(NSString *)credential withType:(CredentialType)type
{
    ValidationResult result = ValidationEmptyField;
    if (!credential || credential.length == 0)
        return result;
    
    NSString *pattern = [CTFCredentialValidator patternForType:type];
    
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

+ (NSString *)patternForType:(CredentialType)type
{
    NSString *pattern = nil;
    switch (type) {
        case CredentialTypeUsername:
            pattern = @"[a-zA-Z0-9]{6,15}";
            break;
            
        case CredentialTypePassword:
            pattern = @"\\S{6,20}";
            break;
            
        case CredentialTypeEmail:
#warning It's placeholder for simple addresses. We should think about it because it can valid following email address. "._%@a.pl"
            pattern = @"[A-Z0-9a-z._%+-]+@[A-Za-z0-9]+\\.[A-Za-z]{2,4}";
            break;
    }
    return pattern;
}

@end
