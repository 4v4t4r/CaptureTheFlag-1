//
//  RKLogin.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 09.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "RKLogin.h"

@implementation RKLogin

+ (RKObjectMapping *)loginMapping
{
    RKObjectMapping *mapping = [RKObjectMapping mappingForClass:[self class]];
    NSDictionary *dict = @{@"login.success" : @"success",
                           @"login.token" : @"token",
                           @"login.message" : @"message"};
    [mapping addAttributeMappingsFromDictionary:dict];
    
    return mapping;
}

@end
