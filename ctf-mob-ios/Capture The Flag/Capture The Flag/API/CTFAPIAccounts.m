//
//  CTFAPIAccounts.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIAccounts.h"
#import "CTFAPIConnection.h"
#import "CTFUser.h"

@implementation CTFAPIAccounts
{
    CTFAPIConnection *_connection;
}

- (instancetype)initWithConnection:(CTFAPIConnection *)connection {
    self = [super init];
    if (self) {
        _connection = connection;
    }
    return self;
}

- (void)signInWithUsername:(NSString *)username andPassword:(NSString *)password withBlock:(TokenBlock)block {
    static NSString *kAccessTokenKey = @"access_token";
    /// Validation
    if (!username || !password ) {
        return;
    }
#warning [tsu] set official path and test with server when available
    /// Run if validated
    NSDictionary *parameters = @{@"username": username, @"password": password};
    [_connection.manager.HTTPClient getPath:@"test" parameters:parameters
                                    success:^(AFHTTPRequestOperation *operation, id responseObject) {
                                        NSString *token = [responseObject objectForKey:kAccessTokenKey];
                                        if (block)
                                            block(token);
                                    } failure:^(AFHTTPRequestOperation *operation, NSError *error) {
                                        if (block)
                                            block(nil);
                                    }];
}

- (void)signUpWithUsername:(NSString *)username email:(NSString *)email password:(NSString *)password block:(SignUpBlock)block {
    
    if (!username || !email || !password) {
        return;
    }
    
    CTFUser *user = [CTFUser createObject];
    user.username = username;
    user.email = email;
    user.password = password;
#warning [tsu] set official path and test with server when available. I think here should be request and response descriptor configurated
#warning [tsu] path is available. Please update
    [_connection.manager postObject:user path:@"path" parameters:nil
                            success:^(RKObjectRequestOperation *operation, RKMappingResult *mappingResult) {
                                block(YES);
                            } failure:^(RKObjectRequestOperation *operation, NSError *error) {
                                block(NO);
                            }];
}

- (void)accountInfoForToken:(NSString *)token block:(ProfileBlock)block {
#warning [tsu] need to pass token in parameter
     [[CTFAPIConnection sharedConnection].manager getObject:nil path:@"profile" parameters:nil success:^(RKObjectRequestOperation *operation, RKMappingResult *mappingResult) {
         CTFUser *object;
         NSArray *array = mappingResult.array;
         if (array.count)
             object = (CTFUser *)mappingResult.array[0];
         block(object);
     } failure:^(RKObjectRequestOperation *operation, NSError *error) {
         block(nil);
     }];
    
}

@end
