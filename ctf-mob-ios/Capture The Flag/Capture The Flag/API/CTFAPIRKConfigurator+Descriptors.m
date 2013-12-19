//
//  CTFAPIRKConfigurator+Descriptors.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIRKConfigurator+Descriptors.h"
#import "CTFAPIRKConfigurator+Mappings.h"

@implementation CTFAPIRKConfigurator (Descriptors)

#pragma mark api/users/ (POST)
- (RKRequestDescriptor *)usersPOSTRequestDescriptor {
    RKEntityMapping *mapping = [self entityMappingFromClass:[CTFUser class]];

    RKRequestDescriptor *descriptor =
    [RKRequestDescriptor requestDescriptorWithMapping:[mapping inverseMapping]
                                          objectClass:[CTFUser class]
                                          rootKeyPath:nil
                                               method:RKRequestMethodPOST];
    return descriptor;
}

- (RKResponseDescriptor *)usersPOSTResponseDescriptor {
    RKResponseDescriptor *response =
    [RKResponseDescriptor responseDescriptorWithMapping:[self entityMappingFromClass:[CTFUser class]]
                                                 method:RKRequestMethodPOST
                                            pathPattern:@"/api/registration/"
                                                keyPath:nil
                                            statusCodes:RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful)];
    return response;
}



#pragma mark - api/profile (GET)
- (RKResponseDescriptor *)profileResponseDescriptor {
    RKResponseDescriptor *response =
    [RKResponseDescriptor responseDescriptorWithMapping:[self entityMappingFromClass:[CTFUser class]]
                                                 method:RKRequestMethodGET
                                            pathPattern:@"/api/profile"
                                                keyPath:nil
                                            statusCodes:RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful)];
    return response;
}

@end
