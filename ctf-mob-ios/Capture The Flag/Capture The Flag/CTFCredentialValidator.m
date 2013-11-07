//
//  CTFCredentialValidator.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFCredentialValidator.h"

@implementation CTFCredentialValidator

+ (BOOL)validCredential:(NSString *)credential withType:(CredentialType)type
{
    BOOL valid = NO;
    NSString *pattern = [CTFCredentialValidator patternForType:type];
    
    NSError *error = nil;
    NSRegularExpression *regex = [[NSRegularExpression alloc] initWithPattern:pattern options:NSRegularExpressionCaseInsensitive error:&error];
    NSRange credentialRange = NSMakeRange(0, credential.length);
    NSRange matchRange = [regex rangeOfFirstMatchInString:credential options:0 range:credentialRange];
    
    if (error)
        NSLog(@"%s, %@", __PRETTY_FUNCTION__, [error localizedDescription]);
    
    NSLog(@"credential = %@ | range = %d/%d",credential, matchRange.location, matchRange.length);
    if (NSEqualRanges(matchRange, credentialRange))
        valid = YES;
    
    return valid;
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
    }
    return pattern;
}

@end
