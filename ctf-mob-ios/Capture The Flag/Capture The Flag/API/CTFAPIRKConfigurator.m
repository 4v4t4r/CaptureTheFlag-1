//
//  CTFAPIRKConfigurator.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIRKConfigurator.h"
#import "CTFAPIRKConfigurator+Descriptors.h"

@implementation CTFAPIRKConfigurator {
    RKObjectManager *_manager;
}

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
    [_manager addResponseDescriptor:[self profileResponseDescriptor]];
}


#pragma mark - Accessors for categories
- (RKObjectManager *)manager {
    return _manager;
}

@end
