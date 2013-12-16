//
//  CTFAPIRKDescriptors.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 06.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIRKDescriptors.h"

#import "CTFUser.h"
#import "CTFCharacter.h"
#import "CTFMap.h"

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


#pragma mark - User Mapping
- (RKEntityMapping *)userMapping {

    RKEntityMapping *userMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFUser class]) inManagedObjectStore:_manager.managedObjectStore];
    [userMapping addAttributeMappingsFromDictionary:[self _userAttributes]];
    
    RKRelationshipMapping *relationshipMapping =
    [RKRelationshipMapping relationshipMappingFromKeyPath:@"characters" toKeyPath:@"characters" withMapping:[self characterMapping]];
    [userMapping addPropertyMapping:relationshipMapping];
    
    return userMapping;
}

- (NSDictionary *)_userAttributes {
    return @{@"username" : @"username",
             @"email" : @"email",
             @"password" : @"password",
             @"nick" : @"nick",
             @"location" : @"location"/*,
                                       @"characters": @"characters"*/};
}


#pragma mark - Character Mapping
- (RKEntityMapping *)characterMapping {
    RKEntityMapping *characterMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectStore:_manager.managedObjectStore];
    [characterMapping addAttributeMappingsFromDictionary:[self _characterMappingDict]];
    return characterMapping;
}

- (NSDictionary *)_characterMappingDict {
    return @{@"type": @"type",
             @"total_time": @"totalTime",
             @"total_score": @"totalScore",
             @"health": @"health",
             @"level": @"level",
             @"is_active": @"active"};
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

#pragma mark - Accessors
- (RKObjectManager *)manager {
    return _manager;
}

@end
