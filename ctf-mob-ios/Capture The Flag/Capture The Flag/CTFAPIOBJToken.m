//
//  CTFAPIOBJToken.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 01.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIOBJToken.h"

@implementation CTFAPIOBJToken

+ (RKResponseDescriptor *)responseDescriptor {
    
    RKObjectMapping *mapping = [RKObjectMapping mappingForClass:[CTFAPIOBJToken class]];
    [mapping addAttributeMappingsFromDictionary:@{@"access_token" : @"value"}];

    NSIndexSet *statusCodes = RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful);
    RKResponseDescriptor *descriptor =
    [RKResponseDescriptor responseDescriptorWithMapping:mapping
                                                 method:RKRequestMethodGET
                                            pathPattern:@"test"
                                                keyPath:nil
                                            statusCodes:statusCodes];
    return descriptor;
}

@end
