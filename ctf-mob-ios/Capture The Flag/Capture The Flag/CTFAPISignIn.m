//
//  CTFAPISignIn.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPISignIn.h"
#import <RestKit/RestKit.h>

#import "CTFAPIConnection.h"

@implementation CTFAPISignIn
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

- (void)signInWithUsername:(NSString *)username andPassword:(NSString *)password withBlock:(SignInBlock)block {
    [_connection.client getPath:@"test.json" parameters:Nil success:^(AFHTTPRequestOperation *operation, id responseObject) {
        block(YES, [responseObject objectForKey:@"access_token"]);
    } failure:^(AFHTTPRequestOperation *operation, NSError *error) {
        block(NO, nil);
    }];
}

@end
