//
//  CTFRegisterService.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.02.2014.
//  Copyright (c) 2014 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@class CTFAPIAccounts;

typedef enum {
    RegisterStateDifferentPasswords,
    RegisterStateInProgress,
    RegisterStateSuccessful,
    RegisterStateFailure
} RegisterState;

typedef void (^RegisterStateBlock)(RegisterState state);

@interface CTFRegisterService : NSObject

@property (nonatomic, readonly) CTFAPIAccounts *accounts;

- (instancetype)initWithAccounts:(CTFAPIAccounts *)accounts;
- (void)registerWithUsername:(NSString *)username password:(NSString *)password rePassword:(NSString *)rePassword email:(NSString *)email responseBlock:(RegisterStateBlock)block;

@end
