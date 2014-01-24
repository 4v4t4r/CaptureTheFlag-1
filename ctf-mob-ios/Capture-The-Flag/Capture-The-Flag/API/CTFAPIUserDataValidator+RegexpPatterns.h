//
//  CTFAPIUserDataValidator+RegexpPatterns.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 21.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIUserDataValidator.h"

@class TSStringValidatorPattern;

extern NSString *const emailRegexp;
extern NSString *const usernameRegexp;
extern NSString *const passwordRegexp;
extern NSString *const nameRegexp;
extern NSString *const nickRegexp;

extern NSString *const emailIdentifier;
extern NSString *const usernameIdentifier;
extern NSString *const passwordIdentifier;
extern NSString *const nameIdentifier;
extern NSString *const nickIdentifier;

@interface CTFAPIUserDataValidator (RegexpPatterns)

+ (TSStringValidatorPattern *)emailPattern;
+ (TSStringValidatorPattern *)usernamePattern;
+ (TSStringValidatorPattern *)passwordPattern;
+ (TSStringValidatorPattern *)namePattern;
+ (TSStringValidatorPattern *)nickPattern;

@end
