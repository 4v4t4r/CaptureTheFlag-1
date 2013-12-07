//
//  CTFAPIMappings.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 06.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIMappings.h"

#import "CTFUser.h"
#import "CTFCharacter.h"

@implementation CTFAPIMappings {
    RKObjectManager *_manager;
}

static CTFAPIMappings *_sharedInstance = nil;
+ (instancetype)sharedInstance {
    return _sharedInstance;
}

+ (void)setSharedInstance:(CTFAPIMappings *)sharedInstance {
    _sharedInstance = sharedInstance;
}

- (instancetype)initWithManager:(RKObjectManager *)manager {
    self = [super init];
    if (self) {
        _manager = manager;
    }
    return self;
}

- (RKEntityMapping *)userMapping {
    /// Configure basic user mapping
    RKEntityMapping *userMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFUser class]) inManagedObjectStore:_manager.managedObjectStore];
    [userMapping addAttributeMappingsFromDictionary:[self userMappingDict]];
    
    /// Configure character mapping
    RKEntityMapping *characterMapping = [self characteMapping];
    
    RKRelationshipMapping *relationshipMapping =
    [RKRelationshipMapping relationshipMappingFromKeyPath:@"characters" toKeyPath:@"characters" withMapping:characterMapping];
    [userMapping addPropertyMapping:relationshipMapping];
    return userMapping;
}

- (RKEntityMapping *)characteMapping {
    RKEntityMapping *characterMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectStore:_manager.managedObjectStore];
    [characterMapping addAttributeMappingsFromDictionary:[self characterMappingDict]];
    return characterMapping;
}

- (RKEntityMapping *)testUserMappingWithStore:(RKManagedObjectStore *)store {
    RKEntityMapping *userMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFUser class]) inManagedObjectStore:store];
    [userMapping addAttributeMappingsFromDictionary:[self userMappingDict]];
    /*
    RKEntityMapping *characterMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectStore:store];
    [characterMapping addAttributeMappingsFromDictionary:[self characterMappingDict]];
    
    [userMapping addPropertyMapping:[RKRelationshipMapping relationshipMappingFromKeyPath:@"characters" toKeyPath:@"characters" withMapping:characterMapping]];
    
     */return userMapping;
}

- (NSDictionary *)userMappingDict {
    return @{@"username" : @"username",
             @"email" : @"email",
             @"password" : @"password",
             @"nick" : @"nick",
             @"location" : @"location"/*,
            @"characters": @"characters"*/};
}

- (NSDictionary *)characterMappingDict {
    return @{@"type": @"type",
             @"total_time": @"totalTime",
             @"total_score": @"totalScore",
             @"health": @"health",
             @"level": @"level",
             @"is_active": @"active"};
}

- (RKObjectManager *)manager {
    return _manager;
}

- (RKEntityMapping *)simpleUserMappingWithStore:(RKManagedObjectStore *)store {
    RKEntityMapping *userMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFUser class]) inManagedObjectStore:_manager.managedObjectStore];
    [userMapping addAttributeMappingsFromDictionary:[self userMappingDict]];
    return userMapping;
}

@end
