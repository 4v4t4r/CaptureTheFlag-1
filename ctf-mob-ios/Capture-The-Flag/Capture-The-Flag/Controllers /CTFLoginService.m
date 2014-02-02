//
//  CTFLoginService.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.02.2014.
//  Copyright (c) 2014 Tomasz Szulc. All rights reserved.
//

#import "CTFLoginService.h"

#import "CoreDataService.h"
#import "CTFAPIAccounts.h"
#import "CTFAPIConnection.h"
#import "CTFAPILocalCredentials.h"
#import "CTFAPILocalCredentialsStore.h"
#import "CTFAPIRKConfigurator.h"
#import "CTFAPIUserDataValidator.h"
#import "CTFSession.h"
#import "CTFUser.h"

@implementation CTFLoginService

- (instancetype)initWithAccounts:(CTFAPIAccounts *)accounts {
    self = [super init];
    if (self) {
        _accounts = accounts;
    }
    return self;
}

- (void)logInWithUsername:(NSString *)username password:(NSString *)password responseBlock:(LogInResponseBlock)block {
    
    CredentialsValidationResult result =
    [CTFAPIUserDataValidator validateSignInCredentialsWithUsername:username
                                                          password:password];
    if (result == CredentialsValidationResultOK) {
        block(LoginStateInProgress);
        [_accounts signInWithUsername:username andPassword:password withBlock:^(NSString *token) {
            if (token) {
                /// Configure game object with token and logged user
                CTFSession *session = [[CTFSession alloc] initWithToken:token];
                
                CTFAPIRKConfigurator *configurator = [[CTFAPIRKConfigurator alloc] initWithManager:[CTFAPIConnection sharedConnection].manager];
                [configurator authorizeRequestsWithToken:token];
                
                CTFUser *user =
                [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFUser class])
                                              inManagedObjectContext:[CoreDataService sharedInstance].managedObjectContext];
                
                user.username = username;
                session.currentUser = user;
                
                [CTFSession setSharedInstance:session];
                
                CTFAPILocalCredentials *credentials = [[CTFAPILocalCredentials alloc] initWithUsername:username password:password];
                [[CTFAPILocalCredentialsStore sharedInstance] storeCredentials:credentials];
                block(LoginStateSuccessful);
            } else {
                block(LoginStateFailure);
            }
        }];

    } else {
        block(LoginStateCredentialsNotValid);
    }
}

@end
