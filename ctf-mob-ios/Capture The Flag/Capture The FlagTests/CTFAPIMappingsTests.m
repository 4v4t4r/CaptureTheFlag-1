//
//  CTFAPIMappingsTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 03.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import <OCMock/OCMock.h>
#import <RestKit/Testing.h>

#import "CoreDataService.h"
#import "CTFUser.h"
#import "CTFCharacter.h"

@interface CTFAPIMappingsTests : XCTestCase

@end

@implementation CTFAPIMappingsTests {
    CoreDataService *_service;
    RKObjectManager *_manager;
}

- (void)setUp {
    [super setUp];
    _service = [[CoreDataService alloc] initForUnitTesting];
    
    _manager = [[RKObjectManager alloc] init];
    RKManagedObjectStore *managedObjectStore = [[RKManagedObjectStore alloc] initWithPersistentStoreCoordinator:_service.persistentStoreCoordinator];
    _manager.managedObjectStore = managedObjectStore;
    [_manager.managedObjectStore createManagedObjectContexts];
    
    NSBundle *bundle = [NSBundle bundleWithIdentifier:[[NSBundle mainBundle].bundleIdentifier stringByAppendingString:@"Tests"]];
    [RKTestFixture setFixtureBundle:bundle];
}

- (void)tearDown {
    _service = nil;
    _manager = nil;
    [super tearDown];
}

- (RKEntityMapping *)userMapping {
    /// Configure mapping
    RKEntityMapping *userMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFUser class]) inManagedObjectStore:_manager.managedObjectStore];
    
    NSDictionary *userAttributesDict = @{@"username" : @"username",
                               @"email" : @"email",
                               @"password" : @"password",
                               @"nick" : @"nick",
                               @"location" : @"location"/*,
                               @"characters": @"characters"*/};
    
    [userMapping addAttributeMappingsFromDictionary:userAttributesDict];
    
    /// Add relationship mapping
    [userMapping addPropertyMapping:[RKRelationshipMapping relationshipMappingFromKeyPath:@"characters" toKeyPath:@"characters" withMapping:[self characterMapping]]];
    
    return userMapping;
}

- (RKEntityMapping *)characterMapping {
    RKEntityMapping *characterMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectStore:_manager.managedObjectStore];
    
    NSDictionary *characterAttributesDict = @{@"type": @"type",
                                              @"total_time": @"totalTime",
                                              @"total_score": @"totalScore",
                                              @"health": @"health",
                                              @"level": @"level",
                                              @"is_active": @"active"};
    
    [characterMapping addAttributeMappingsFromDictionary:characterAttributesDict];
    return characterMapping;
}

- (void)testUserResponseMapping {
    id parsedJSON = [RKTestFixture parsedObjectWithContentsOfFixture:@"user-response.json"];
        
    /// Configure expectations
    RKMappingTest *test = [RKMappingTest testForMapping:[self userMapping] sourceObject:parsedJSON destinationObject:nil];
    test.managedObjectContext = _service.managedObjectContext;
    
    RKPropertyMappingTestExpectation *usernameExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"username" destinationKeyPath:@"username" value:@"tomkowz12"];
    [test addExpectation:usernameExpectation];

    RKPropertyMappingTestExpectation *emailExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"email" destinationKeyPath:@"email" value:@"tmk.szlc@gmail.com"];
    [test addExpectation:emailExpectation];

    RKPropertyMappingTestExpectation *passwordExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"password" destinationKeyPath:@"password" value:@"password123"];
    [test addExpectation:passwordExpectation];
    
    RKPropertyMappingTestExpectation *nickExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"nick" destinationKeyPath:@"nick" value:@"black_lord"];
    [test addExpectation:nickExpectation];
    
    RKPropertyMappingTestExpectation *locationExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"location" destinationKeyPath:@"location" value:@[@(10), @(20)]];
    [test addExpectation:locationExpectation];
    
    /// Configure expectation objects
    
    RKPropertyMappingTestExpectation *charactersExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"characters" destinationKeyPath:@"characters" evaluationBlock:^BOOL(RKPropertyMappingTestExpectation *expectation, RKPropertyMapping *mapping, id mappedValue, NSError *__autoreleasing *error) {
        
        NSArray *commitedKeysToCompare = @[@"type, totalTime, totalScore, healt, level, active"];
        
        NSArray *array = [mappedValue allObjects];
        BOOL containsTwoItems = array.count == 2;
        
        /// Test First character
        CTFCharacter *firstFetchedCharacter = (CTFCharacter *)array[0];
        
        CTFCharacter *firstReferenceCharacter = [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectContext:_service.managedObjectContext];
        firstReferenceCharacter.type = @(1);
        firstReferenceCharacter.totalTime = @(21);
        firstReferenceCharacter.totalScore = @(100);
        firstReferenceCharacter.health = @(100);
        firstReferenceCharacter.level = @(20);
        firstReferenceCharacter.active = @YES;
        
        BOOL isFirstEqual = [[firstFetchedCharacter committedValuesForKeys:commitedKeysToCompare] isEqual:[firstReferenceCharacter committedValuesForKeys:commitedKeysToCompare]];
        
        /// Test Second character
        CTFCharacter *secondFetchedCharacter = (CTFCharacter *)array[1];
        
        CTFCharacter *secondReferenceCharacter = [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectContext:_service.managedObjectContext];
        secondReferenceCharacter.type = @(2);
        secondReferenceCharacter.totalTime = @(23);
        secondReferenceCharacter.totalScore = @(98);
        secondReferenceCharacter.health = @(55);
        secondReferenceCharacter.level = @(12);
        secondReferenceCharacter.active = @NO;
        
        BOOL isSecondEqual = [[secondFetchedCharacter committedValuesForKeys:commitedKeysToCompare] isEqual:[secondReferenceCharacter committedValuesForKeys:commitedKeysToCompare]];

        return isFirstEqual && isSecondEqual && containsTwoItems;
    }];
    
    [test addExpectation:charactersExpectation];
    
    [test verify];
}


@end
