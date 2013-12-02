//
//  CTFAPIAccounts.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIAccounts.h"

#import "CTFAPIConnection.h"

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

static NSString *kAccessTokenKey = @"access_token";
- (void)signInWithUsername:(NSString *)username andPassword:(NSString *)password withBlock:(TokenBlock)block {

    /// Validation
    if (!username || !password ) {
        return;
    }
    
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

@end
