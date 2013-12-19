//
//  CTFAPIRKConfigurator+Mappings.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIRKConfigurator+Mappings.h"
#import "CTFCharacter.h"
#import "CTFGame.h"
#import "CTFItem.h"
#import "CTFMap.h"
#import "CTFUser.h"

@implementation CTFAPIRKConfigurator (Mappings)


#pragma mark - Factory
- (RKEntityMapping *)entityMappingFromClass:(Class)aClass {
    RKEntityMapping *mapping = nil;
    if (aClass == [CTFCharacter class]) {
        mapping = [self characterMapping];
    } else if (aClass == [CTFGame class]) {
        mapping = [self gameMapping];
    } else if (aClass == [CTFItem class]) {
        mapping = [self itemMapping];
    } else if (aClass == [CTFMap class]) {
        mapping = [self mapMapping];
    } else if (aClass == [CTFUser class]) {
        mapping = [self userMapping];
    }
    
    return mapping;
}


#pragma mark - Character Mapping
- (RKEntityMapping *)characterMapping {
    RKManagedObjectStore *store = self.manager.managedObjectStore;
    
    RKEntityMapping *characterMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectStore:store];
    [characterMapping addAttributeMappingsFromDictionary:[self _characterMappingDict]];
    
    /// User Mapping without Character relationship mapping
    RKEntityMapping *userMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFUser class]) inManagedObjectStore:store];
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
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFGame class]) inManagedObjectStore:self.manager.managedObjectStore];
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
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFItem class]) inManagedObjectStore:self.manager.managedObjectStore];
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
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFMap class]) inManagedObjectStore:self.manager.managedObjectStore];
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
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFUser class]) inManagedObjectStore:self.manager.managedObjectStore];
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
             @"location" : @"location",
             @"first_name" : @"firstName",
             @"last_name": @"lastName"};
}

@end

