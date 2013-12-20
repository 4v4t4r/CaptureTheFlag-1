//
//  CTFAPIRKConfigurator.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIRKConfigurator.h"
#import "CTFAPIRKConfigurator+Descriptors.h"

@implementation CTFAPIRKConfigurator

- (id)initWithManager:(RKObjectManager *)manager {
    if (!manager)
        return nil;

    self = [super init];
    if (self) {
        _manager = manager;
    }
    return self;
}

- (void)configure {
    /// api/users/
    [_manager addRequestDescriptor:[self usersPOSTRequestDescriptor]];
    [_manager addResponseDescriptor:[self usersPOSTResponseDescriptor]];
    
    /// api/profile
    [_manager addResponseDescriptor:[self profileResponseDescriptor]];
}

- (void)authorizeRequestsWithToken:(NSString *)token {
    [_manager.HTTPClient setDefaultHeader:@"Authorization" value:[NSString stringWithFormat:@"Bearer %@",token]];
}

@end
