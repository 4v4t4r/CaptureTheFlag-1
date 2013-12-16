//
//  CTFAPIRKDescriptors.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 03.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import <OCMock/OCMock.h>
#import <RestKit/Testing.h>

#import "CTFAPIRKDescriptors.h"
#import "CoreDataService.h"
#import "CTFUser.h"
#import "CTFCharacter.h"

@interface CTFAPIRKDescriptorsTests : XCTestCase

@end

@implementation CTFAPIRKDescriptorsTests {
    CoreDataService *_service;
    RKObjectManager *_manager;
    CTFAPIRKDescriptors *_descriptors;
}

- (void)setUp {
    [super setUp];
    [CTFAPIRKDescriptors setSharedInstance:nil];
    
    _service = [[CoreDataService alloc] initForUnitTesting];
    
    _manager = [[RKObjectManager alloc] init];
    RKManagedObjectStore *managedObjectStore = [[RKManagedObjectStore alloc] initWithPersistentStoreCoordinator:_service.persistentStoreCoordinator];
    _manager.managedObjectStore = managedObjectStore;
    [_manager.managedObjectStore createManagedObjectContexts];
    
    _descriptors = [[CTFAPIRKDescriptors alloc] initWithManager:_manager];
    
    NSBundle *bundle = [NSBundle bundleWithIdentifier:[[NSBundle mainBundle].bundleIdentifier stringByAppendingString:@"Tests"]];
    [RKTestFixture setFixtureBundle:bundle];
}

- (void)tearDown {
    _service = nil;
    _manager = nil;
    _descriptors = nil;
    [super tearDown];
}

- (void)testSharedInstanceShouldBeNil {
    XCTAssertNil([CTFAPIRKDescriptors sharedInstance], @"");
}

- (void)testSharedInstanceShouldBeNotNilAndManagerWithStoreShouldBeEquals {
    RKObjectManager *manager = [[RKObjectManager alloc] init];
    CTFAPIRKDescriptors *mappings = [[CTFAPIRKDescriptors alloc] initWithManager:manager];
    [CTFAPIRKDescriptors setSharedInstance:mappings];
    XCTAssertNotNil([CTFAPIRKDescriptors sharedInstance], @"");
}

- (void)testInstanceShouldHaveTheSameManagerAndStoreAsInjected {
    RKObjectManager *objectManager = [[RKObjectManager alloc] init];
    RKManagedObjectStore *store = [[RKManagedObjectStore alloc] initWithPersistentStoreCoordinator:_service.persistentStoreCoordinator];
    [store createManagedObjectContexts];
    
    objectManager.managedObjectStore = store;
    
    CTFAPIRKDescriptors *mappings = [[CTFAPIRKDescriptors alloc] initWithManager:objectManager];
    XCTAssertNotNil(mappings, @"");
    XCTAssertNotNil(mappings.manager, @"");
    XCTAssertEqualObjects(mappings.manager, objectManager, @"");
    XCTAssertEqualObjects(mappings.manager.managedObjectStore, objectManager.managedObjectStore, @"");
}


#pragma mark - Descriptors
- (void)testGetUserResponseDescriptor {
    RKResponseDescriptor *descriptor = [_descriptors getUserResponseDescriptor];
    XCTAssertEqualObjects(descriptor.pathPattern, @"test", @"");
    
    BOOL containsSuccessfulCodes = [descriptor.statusCodes containsIndexes:RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful)];
    XCTAssertTrue(containsSuccessfulCodes, @"");

    BOOL containsClientErrorCodes = [descriptor.statusCodes containsIndexes:RKStatusCodeIndexSetForClass(RKStatusCodeClassClientError)];
    XCTAssertTrue(containsClientErrorCodes, @"");
    
    XCTAssertEqual(descriptor.method, RKRequestMethodGET, @"");
}

#pragma mark - Mappings
- (void)testUserMapping {
    id parsedJSON = [RKTestFixture parsedObjectWithContentsOfFixture:@"user-response.json"];
        
    /// Configure expectations
    RKMappingTest *test = [RKMappingTest testForMapping:[_descriptors userMapping] sourceObject:parsedJSON destinationObject:nil];
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

- (void)testCharacterMapping {
    id parsedJSON = [RKTestFixture parsedObjectWithContentsOfFixture:@"character-response.json"];
    
    RKMappingTest *test = [RKMappingTest testForMapping:[_descriptors characterMapping] sourceObject:parsedJSON destinationObject:nil];
    test.managedObjectContext = _service.managedObjectContext;
    
    RKPropertyMappingTestExpectation *typeExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"type" destinationKeyPath:@"type" value:@(1)];
    [test addExpectation:typeExpectation];
    
    RKPropertyMappingTestExpectation *totalTimeExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"total_time" destinationKeyPath:@"totalTime" value:@(21)];
    [test addExpectation:totalTimeExpectation];
    
    RKPropertyMappingTestExpectation *totalScoreExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"total_score" destinationKeyPath:@"totalScore" value:@(100)];
    [test addExpectation:totalScoreExpectation];
    
    RKPropertyMappingTestExpectation *healthExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"health" destinationKeyPath:@"health" value:@(100)];
    [test addExpectation:healthExpectation];
    
    RKPropertyMappingTestExpectation *levelExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"level" destinationKeyPath:@"level" value:@(20)];
    [test addExpectation:levelExpectation];
    
    RKPropertyMappingTestExpectation *isActiveExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"is_active" destinationKeyPath:@"active" value:@(1)];
    [test addExpectation:isActiveExpectation];
    
    [test verify];
}

- (void)testMapMapping {
    id parsedJSON = [RKTestFixture parsedObjectWithContentsOfFixture:@"map-response.json"];
    
    RKMappingTest *test = [RKMappingTest testForMapping:[_descriptors mapMapping] sourceObject:parsedJSON destinationObject:nil];
    test.managedObjectContext = _service.managedObjectContext;
    
    RKPropertyMappingTestExpectation *idExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"id" destinationKeyPath:@"mapId" value:@(1)];
    [test addExpectation:idExpectation];
    
    RKPropertyMappingTestExpectation *nameExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"name" destinationKeyPath:@"name" value:@"map's name"];
    [test addExpectation:nameExpectation];
    
    RKPropertyMappingTestExpectation *descriptionExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"description" destinationKeyPath:@"desc" value:@"map description"];
    [test addExpectation:descriptionExpectation];
    
    RKPropertyMappingTestExpectation *locationExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"location" destinationKeyPath:@"location" value:@[@(12.33233), @(43.12122)]];
    [test addExpectation:locationExpectation];
    
    RKPropertyMappingTestExpectation *radiusExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"radius" destinationKeyPath:@"radius" value:@(5.0)];
    [test addExpectation:radiusExpectation];
    
    RKPropertyMappingTestExpectation *createdByExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"created_by" destinationKeyPath:@"createdBy" value:@"owner name"];
    [test addExpectation:createdByExpectation];
    
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
    [dateFormatter setDateFormat:@"dd-MM-YYYY hh-mm-ss"];
    NSDate *createdDate = [dateFormatter dateFromString:@"12-12-2013 12:12:00"];
    
    RKPropertyMappingTestExpectation *createdDateExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"created_date" destinationKeyPath:@"createdDate" value:createdDate];
    [test addExpectation:createdDateExpectation];
    
    NSDate *modifiedDate = [dateFormatter dateFromString:@"13-12-2013 12:12:00"];
    RKPropertyMappingTestExpectation *modifiedDateExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"modified_date" destinationKeyPath:@"modifiedDate" value:modifiedDate];
    [test addExpectation:modifiedDateExpectation];
    
    [test verify];
}

@end
