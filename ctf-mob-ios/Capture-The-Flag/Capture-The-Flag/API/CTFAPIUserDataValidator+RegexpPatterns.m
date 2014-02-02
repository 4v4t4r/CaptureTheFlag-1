//
//  CTFAPIUserDataValidator+RegexpPatterns.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 21.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIUserDataValidator+RegexpPatterns.h"
#import "TSStringValidatorPattern.h"

NSString *const emailRegexp = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)+$";
NSString *const usernameRegexp = @"[a-zA-Z0-9@.+-]{6,50}";
NSString *const passwordRegexp = @"\\S{6,50}";
NSString *const nameRegexp = @"([a-zA-Z]+\\s?)+";
NSString *const nickRegexp = @"[\\S ]*";

NSString *const emailIdentifier = @"email";
NSString *const usernameIdentifier = @"username";
NSString *const passwordIdentifier = @"password";
NSString *const nameIdentifier = @"name";
NSString *const nickIdentifier = @"nick";

@implementation CTFAPIUserDataValidator (RegexpPatterns)

+ (TSStringValidatorPattern *)emailPattern {
    TSStringValidatorPattern *pattern = [TSStringValidatorPattern patternWithString:emailRegexp identifier:emailIdentifier];
    return pattern;
}

+ (TSStringValidatorPattern *)usernamePattern {
    TSStringValidatorPattern *pattern = [TSStringValidatorPattern patternWithString:usernameRegexp identifier:usernameIdentifier];
    return pattern;
}

+ (TSStringValidatorPattern *)passwordPattern {
    TSStringValidatorPattern *pattern = [TSStringValidatorPattern patternWithString:passwordRegexp identifier:passwordIdentifier];
    return pattern;
}

+ (TSStringValidatorPattern *)namePattern {
    TSStringValidatorPattern *pattern = [TSStringValidatorPattern patternWithString:nameRegexp identifier:nameIdentifier];
    return pattern;
}

+ (TSStringValidatorPattern *)nickPattern {
    TSStringValidatorPattern *pattern = [TSStringValidatorPattern patternWithString:nickRegexp identifier:nickIdentifier];
    return pattern;
}

@end
