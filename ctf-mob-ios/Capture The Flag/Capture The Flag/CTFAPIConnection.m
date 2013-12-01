//
//  CTFAPIConnection.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 01.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIConnection.h"

@implementation CTFAPIConnection {
    AFHTTPClient *_client;
}

static CTFAPIConnection *_sharedConnection = nil;
+ (void)setSharedConnection:(CTFAPIConnection *)connection {
    @synchronized (self) {
        _sharedConnection = connection;
    }
}

+ (instancetype)sharedConnection {
    return _sharedConnection;
}

- (instancetype)initWithClient:(AFHTTPClient *)client {
    self = [super init];
    if (self) {
        _client = client;
    }
    return self;
}


#pragma mark - Accessors
- (AFHTTPClient *)client {
    return _client;
}

@end
