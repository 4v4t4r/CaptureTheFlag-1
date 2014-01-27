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
#import "CoreDataService.h"
#import "CTFSession.h"

static NSString * const client_id = @"94f5481b9ac440230663";
static NSString * const client_secret = @"660e440fbe257bcfaadea794acd41230c18fe4d0";
static NSString * const scope = @"read+write";

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

    /// Run if validated
    NSDictionary *parameters = @{@"client_id": client_id,
                                 @"client_secret": client_secret,
                                 @"grant_type": @"password",
                                 @"username": username,
                                 @"password": password};
    [_connection.manager.HTTPClient postPath:@"/oauth2/access_token" parameters:parameters
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
    
    /// prepare user object
    CTFUser *user =
    [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFUser class])
                                  inManagedObjectContext:[CoreDataService sharedInstance].managedObjectContext];
    user.username = username;
    user.password = password;
    user.email = email;
    
    [_connection.manager postObject:user path:@"/api/registration/"
                         parameters:nil
                            success:^(RKObjectRequestOperation *operation, RKMappingResult *mappingResult) {
                                block(YES);
                            } failure:^(RKObjectRequestOperation *operation, NSError *error) {
                                block(NO);
                            }];
}

- (void)accountInfoForToken:(NSString *)token block:(ProfileBlock)block {
    if (!token) {
        block(nil);
        return;
    }
    
    __block CTFUser *user = [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFUser class]) inManagedObjectContext:[CoreDataService sharedInstance].managedObjectContext];
    
    [[CTFAPIConnection sharedConnection].manager
      getObject:user path:@"api/profile"
     parameters:nil
      success:^(RKObjectRequestOperation *operation, RKMappingResult *mappingResult) {
         NSArray *array = mappingResult.array;
         if (array.count)
             user = (CTFUser *)mappingResult.array[0];
         block(user);
     } failure:^(RKObjectRequestOperation *operation, NSError *error) {
         block(nil);
     }];
}

- (void)updateInfoForUser:(CTFUser *)user block:(UpdateBlock)block {
    if (!user) {
        block(NO);
        return;
    }
    static NSString * const base = @"api/users";
    NSString *userIdString = [NSString stringWithFormat:@"%d", [user.userId integerValue]];
    NSString *path = [NSString stringWithFormat:@"%@/%@/", base, userIdString];
    user.password = [CTFSession sharedInstance].fixedPassword;
    if (user.nick.length == 0) {
        user.nick = user.username;
    }
    
    [_connection.manager
     patchObject:user
     path:path
     parameters:nil
     success:^(RKObjectRequestOperation *operation, RKMappingResult *mappingResult) {
         block(YES);
     } failure:^(RKObjectRequestOperation *operation, NSError *error) {
         block(NO);
     }];
}

@end
