//
//  RKLoginRequest.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 09.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "RKLoginRequest.h"

@implementation RKLoginRequest

+ (RKObjectMapping *)objectMapping
{
    RKObjectMapping *mapping = [RKObjectMapping mappingForClass:[self class]];
    NSDictionary *dict = @{@"login.login" : @"login",
                           @"login.password" : @"password"};
    [mapping addAttributeMappingsFromDictionary:dict];
    
    return mapping;
}

@end
