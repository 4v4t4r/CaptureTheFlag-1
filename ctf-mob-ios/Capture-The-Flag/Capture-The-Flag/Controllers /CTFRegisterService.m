//
//  CTFRegisterService.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.02.2014.
//  Copyright (c) 2014 Tomasz Szulc. All rights reserved.
//

#import "CTFRegisterService.h"

#import "CTFAPIAccounts.h"

@implementation CTFRegisterService

- (instancetype)initWithAccounts:(CTFAPIAccounts *)accounts {
    self = [super init];
    if (self) {
        _accounts = accounts;
    }
    return self;
}

- (void)registerWithUsername:(NSString *)username password:(NSString *)password rePassword:(NSString *)rePassword email:(NSString *)email responseBlock:(RegisterStateBlock)block {
    
    BOOL samePasswords = [password isEqualToString:rePassword];
    if (!samePasswords) {
        block(RegisterStateDifferentPasswords);
        return;
    }
    
    block(RegisterStateInProgress);
    
    [_accounts signUpWithUsername:username email:email password:password block:^(BOOL success) {
        RegisterState state = success ? RegisterStateSuccessful : RegisterStateFailure;
        block(state);
    }];
}

@end
