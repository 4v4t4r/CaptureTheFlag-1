//
//  CTFAPIRKConfigurator+Descriptors.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIRKConfigurator+Descriptors.h"
#import "CTFAPIRKConfigurator+Mappings.h"
#import "CTFUser.h"

@implementation CTFAPIRKConfigurator (Descriptors)

#pragma mark api/registration/ (POST)
- (RKRequestDescriptor *)registrationPOSTRequestDescriptor {
    RKEntityMapping *mapping = [self entityMappingFromClass:[CTFUser class]];

    RKRequestDescriptor *descriptor =
    [RKRequestDescriptor requestDescriptorWithMapping:[mapping inverseMapping]
                                          objectClass:[CTFUser class]
                                          rootKeyPath:nil
                                               method:RKRequestMethodPOST];
    return descriptor;
}

- (RKResponseDescriptor *)registrationPOSTResponseDescriptor {
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


#pragma mark - api/users/{id} (PATCH)
- (RKRequestDescriptor *)usersPATCHRequestDescriptor {
    RKEntityMapping *mapping = [self entityMappingFromClass:[CTFUser class]];
    RKRequestDescriptor *descriptor =
    [RKRequestDescriptor requestDescriptorWithMapping:[mapping inverseMapping]
                                          objectClass:[CTFUser class]
                                          rootKeyPath:nil
                                               method:RKRequestMethodPATCH];
    return descriptor;
}

- (RKResponseDescriptor *)usersPATCHResponseDescriptor {
    RKResponseDescriptor *descriptor =
    [RKResponseDescriptor responseDescriptorWithMapping:[self entityMappingFromClass:[CTFUser class]]
                                                 method:RKRequestMethodPATCH
                                            pathPattern:@"/api/users/:id/"
                                                keyPath:nil
                                            statusCodes:RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful)];
    return descriptor;
}

@end
