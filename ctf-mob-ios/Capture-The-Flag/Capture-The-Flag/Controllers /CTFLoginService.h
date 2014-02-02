//
//  CTFLoginService.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.02.2014.
//  Copyright (c) 2014 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "CTFAPIAccounts.h"

@interface CTFLoginService : NSObject

typedef enum {
    LoginStateCredentialsNotValid,
    LoginStateInProgress,
    LoginStateSuccessful,
    LoginStateFailure
} LoginState;

typedef void (^LogInResponseBlock)(LoginState state);

@property (nonatomic, strong) CTFAPIAccounts *accounts;

- (instancetype)initWithAccounts:(CTFAPIAccounts *)accounts;
- (void)logInWithUsername:(NSString *)username password:(NSString *)password responseBlock:(LogInResponseBlock)block;

@end
