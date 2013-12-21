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
    /// api/registration/
    [_manager addRequestDescriptor:[self registrationPOSTRequestDescriptor]];
    [_manager addResponseDescriptor:[self registrationPOSTResponseDescriptor]];
    
    /// api/profile
    [_manager addResponseDescriptor:[self profileResponseDescriptor]];
    
    /// api/users/{id}
    [_manager addRequestDescriptor:[self usersPATCHRequestDescriptor]];
    [_manager addResponseDescriptor:[self usersPATCHResponseDescriptor]];
}

- (void)authorizeRequestsWithToken:(NSString *)token {
    [_manager.HTTPClient setDefaultHeader:@"Authorization" value:[NSString stringWithFormat:@"Bearer %@",token]];
}

@end
