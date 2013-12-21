//
//  CTFAPIUserDataValidator+RegexpPatterns.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 21.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIUserDataValidator+RegexpPatterns.h"
#import "TSStringValidatorPattern.h"

@implementation CTFAPIUserDataValidator (RegexpPatterns)

+ (TSStringValidatorPattern *)emailPattern {
    NSString *regexp = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)+$";
    return [TSStringValidatorPattern patternWithString:regexp identifier:@"email"];
}

+ (TSStringValidatorPattern *)usernamePattern {
    NSString *regexp = @"[a-zA-Z0-9@.+-]{6,50}";
    return [TSStringValidatorPattern patternWithString:regexp identifier:@"username"];
}

+ (TSStringValidatorPattern *)passwordPattern {
    NSString *regexp = @"\\S{6,50}";
    return [TSStringValidatorPattern patternWithString:regexp identifier:@"password"];
}

+ (TSStringValidatorPattern *)namePattern {
    NSString *regexp = @"([a-zA-Z]+\\s?)+";
    return [TSStringValidatorPattern patternWithString:regexp identifier:@"name"];
}

+ (TSStringValidatorPattern *)nickPattern {
    NSString *regexp = @"([a-zA-Z]+\\s?)+";
    return [TSStringValidatorPattern patternWithString:regexp identifier:@"nick"];
}

@end
