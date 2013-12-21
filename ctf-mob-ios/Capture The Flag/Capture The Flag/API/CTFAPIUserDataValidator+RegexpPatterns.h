//
//  CTFAPIUserDataValidator+RegexpPatterns.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 21.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIUserDataValidator.h"

@class TSStringValidatorPattern;

@interface CTFAPIUserDataValidator (RegexpPatterns)

+ (TSStringValidatorPattern *)emailPattern;
+ (TSStringValidatorPattern *)usernamePattern;
+ (TSStringValidatorPattern *)passwordPattern;
+ (TSStringValidatorPattern *)namePattern;
+ (TSStringValidatorPattern *)nickPattern;

@end
