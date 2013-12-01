//
//  CTFAPIAccounts.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIAccounts.h"

#import "CTFAPIConnection.h"
#import "CTFAPIOBJToken.h"

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

- (void)signInWithUsername:(NSString *)username andPassword:(NSString *)password withBlock:(SignInBlock)block {

    [_connection.manager addResponseDescriptor:[CTFAPIOBJToken responseDescriptor]];

    [_connection.manager getObjectsAtPath:@"test" parameters:nil success:^(RKObjectRequestOperation *operation, RKMappingResult *mappingResult) {
        CTFAPIOBJToken *token = (CTFAPIOBJToken *)mappingResult.firstObject;
        block(YES, token.value);
    } failure:^(RKObjectRequestOperation *operation, NSError *error) {
        block(NO, nil);
    }];
}

@end
