//
//  CTFAPIRKDescriptors.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 06.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIRKDescriptors.h"
#import "CTFCharacter.h"
#import "CTFGame.h"
#import "CTFMap.h"
#import "CTFItem.h"
#import "CTFUser.h"

@implementation CTFAPIRKDescriptors {
    RKObjectManager *_manager;
}

static CTFAPIRKDescriptors *_sharedInstance = nil;
+ (instancetype)sharedInstance {
    return _sharedInstance;
}

+ (void)setSharedInstance:(CTFAPIRKDescriptors *)sharedInstance {
    _sharedInstance = sharedInstance;
}

- (instancetype)initWithManager:(RKObjectManager *)manager {
    self = [super init];
    if (self) {
        _manager = manager;
    }
    return self;
}


#pragma mark - Descriptors
- (RKResponseDescriptor *)getUserResponseDescriptor {
    RKResponseDescriptor *descriptor;
    NSMutableIndexSet *indexSet = [NSMutableIndexSet indexSet];
    [indexSet addIndexes:[self successfulCodes]];
    [indexSet addIndexes:[self clientErrorCodes]];
    
#warning [tsu] Update path when server will be available.
    descriptor = [RKResponseDescriptor responseDescriptorWithMapping:[self userMapping]
                                                              method:RKRequestMethodGET
                                                         pathPattern:@"test"
                                                             keyPath:nil
                                                         statusCodes:indexSet];
    return descriptor;
}

- (NSIndexSet *)successfulCodes {
    return RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful);
}

- (NSIndexSet *)clientErrorCodes {
    return  RKStatusCodeIndexSetForClass(RKStatusCodeClassClientError);
}

#pragma mark api/profile
- (RKResponseDescriptor *)profileResponseDescriptor {
    
    RKResponseDescriptor *response =
    [RKResponseDescriptor responseDescriptorWithMapping:[self userMapping]
                                                 method:RKRequestMethodGET
                                            pathPattern:@"profile"
                                                keyPath:nil
                                            statusCodes:RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful)];
    return response;
}





#pragma mark - Character Mapping
- (RKEntityMapping *)characterMapping {
    RKEntityMapping *characterMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectStore:_manager.managedObjectStore];
    [characterMapping addAttributeMappingsFromDictionary:[self _characterMappingDict]];
    
    /// User Mapping without Character relationship mapping
    RKEntityMapping *userMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFUser class]) inManagedObjectStore:_manager.managedObjectStore];
    [userMapping addAttributeMappingsFromDictionary:[self _userAttributesDict]];
    
    RKRelationshipMapping *userRelationshipMapping =
    [RKRelationshipMapping relationshipMappingFromKeyPath:@"user" toKeyPath:@"user" withMapping:userMapping];
    [characterMapping addPropertyMapping:userRelationshipMapping];
    
    return characterMapping;
}

- (NSDictionary *)_characterMappingDict {
    return @{@"type": @"type",
             @"total_time": @"totalTime",
             @"total_score": @"totalScore",
             @"health": @"health",
             @"level": @"level"};
}


#pragma mark - Game Mapping
- (RKEntityMapping *)gameMapping {
    RKEntityMapping *gameMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFGame class]) inManagedObjectStore:_manager.managedObjectStore];
    [gameMapping addAttributeMappingsFromDictionary:[self _gameMappingDict]];
    
    RKRelationshipMapping *mapMapping =
    [RKRelationshipMapping relationshipMappingFromKeyPath:@"map" toKeyPath:@"map" withMapping:[self mapMapping]];
    [gameMapping addPropertyMapping:mapMapping];
    
    RKRelationshipMapping *charactersMapping =
    [RKRelationshipMapping relationshipMappingFromKeyPath:@"players" toKeyPath:@"players" withMapping:[self characterMapping]];
    [gameMapping addPropertyMapping:charactersMapping];
    
    RKRelationshipMapping *itemsMapping =
    [RKRelationshipMapping relationshipMappingFromKeyPath:@"items" toKeyPath:@"items" withMapping:[self itemMapping]];
    [gameMapping addPropertyMapping:itemsMapping];
    
    return gameMapping;
}

- (NSDictionary *)_gameMappingDict {
    return @{@"id": @"gameId",
             @"name": @"name",
             @"description": @"desc",
             @"status": @"status",
             @"start_time": @"startTime",
             @"max_players": @"maxPlayers",
             @"type": @"type",
             @"created_date": @"createdDate",
             @"modified_date": @"modifiedDate"};
}


#pragma mark - Item Mapping
- (RKEntityMapping *)itemMapping {
    RKEntityMapping *itemMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFItem class]) inManagedObjectStore:_manager.managedObjectStore];
    [itemMapping addAttributeMappingsFromDictionary:[self _itemMappingDict]];
    return itemMapping;
}

- (NSDictionary *)_itemMappingDict {
    return @{@"name": @"name",
             @"description": @"desc",
             @"type": @"type",
             @"location": @"location",
             @"value": @"value"};
}


#pragma mark - Map Mapping
- (RKEntityMapping *)mapMapping {
    RKEntityMapping *mapMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFMap class]) inManagedObjectStore:_manager.managedObjectStore];
    [mapMapping addAttributeMappingsFromDictionary:[self _mapMappingDict]];
    return mapMapping;
}

- (NSDictionary *)_mapMappingDict {
    return @{@"id": @"mapId",
             @"name": @"name",
             @"description": @"desc",
             @"location": @"location",
             @"radius": @"radius",
             @"created_by": @"createdBy",
             @"created_date": @"createdDate",
             @"modified_date": @"modifiedDate"};
}


#pragma mark - User Mapping
- (RKEntityMapping *)userMapping {
    
    RKEntityMapping *userMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFUser class]) inManagedObjectStore:_manager.managedObjectStore];
    [userMapping addAttributeMappingsFromDictionary:[self _userAttributesDict]];
    
    RKRelationshipMapping *relationshipMapping =
    [RKRelationshipMapping relationshipMappingFromKeyPath:@"characters" toKeyPath:@"characters" withMapping:[self characterMapping]];
    [userMapping addPropertyMapping:relationshipMapping];
    
    return userMapping;
}

- (NSDictionary *)_userAttributesDict {
    return @{@"id" : @"userId",
             @"username" : @"username",
             @"email" : @"email",
             @"password" : @"password",
             @"nick" : @"nick",
             @"location" : @"location"/*,
                                       @"characters": @"characters"*/};
}

#pragma mark - Accessors
- (RKObjectManager *)manager {
    return _manager;
}

@end
