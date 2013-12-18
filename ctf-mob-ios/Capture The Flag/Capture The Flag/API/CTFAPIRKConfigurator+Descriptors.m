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

#pragma mark - Response Descriptors
- (RKResponseDescriptor *)profileResponseDescriptor {
    RKResponseDescriptor *response =
    [RKResponseDescriptor responseDescriptorWithMapping:[self entityMappingFromClass:[CTFUser class]]
                                                 method:RKRequestMethodGET
                                            pathPattern:@"profile"
                                                keyPath:nil
                                            statusCodes:RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful)];
    return response;
}

@end
