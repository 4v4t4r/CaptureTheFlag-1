//
//  CTFCredentialValidator.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface CTFCredentialValidator : NSObject

+ (BOOL)validUsername:(NSString *)username;
+ (BOOL)validPassword:(NSString *)password;

@end
