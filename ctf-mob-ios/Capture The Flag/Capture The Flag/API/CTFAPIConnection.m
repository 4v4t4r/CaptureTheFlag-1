//
//  CTFAPIConnection.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 01.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIConnection.h"

@implementation CTFAPIConnection {
    RKObjectManager *_manager;
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

- (instancetype)initWithManager:(RKObjectManager *)manager {
    self = [super init];
    if (self) {
        _manager = manager;
    }
    return self;
}

#pragma mark - Accessors
- (RKObjectManager *)manager {
    return _manager;
}

@end
