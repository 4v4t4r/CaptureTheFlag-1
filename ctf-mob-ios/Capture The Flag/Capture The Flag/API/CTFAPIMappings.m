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

    RKEntityMapping *userMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFUser class]) inManagedObjectStore:_manager.managedObjectStore];
    [userMapping addAttributeMappingsFromDictionary:[self _userAttributes]];
    
    RKRelationshipMapping *relationshipMapping =
    [RKRelationshipMapping relationshipMappingFromKeyPath:@"characters" toKeyPath:@"characters" withMapping:[self characterMapping]];
    [userMapping addPropertyMapping:relationshipMapping];
    
    return userMapping;
}

- (RKEntityMapping *)characterMapping {
    RKEntityMapping *characterMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectStore:_manager.managedObjectStore];
    [characterMapping addAttributeMappingsFromDictionary:[self _characterMappingDict]];
    return characterMapping;
}


#pragma mark - Dictionaries with attribute mappings
- (NSDictionary *)_userAttributes {
    return @{@"username" : @"username",
             @"email" : @"email",
             @"password" : @"password",
             @"nick" : @"nick",
             @"location" : @"location"/*,
             @"characters": @"characters"*/};
}

- (NSDictionary *)_characterMappingDict {
    return @{@"type": @"type",
             @"total_time": @"totalTime",
             @"total_score": @"totalScore",
             @"health": @"health",
             @"level": @"level",
             @"is_active": @"active"};
}


#pragma mark - Accessors
- (RKObjectManager *)manager {
    return _manager;
}

@end
