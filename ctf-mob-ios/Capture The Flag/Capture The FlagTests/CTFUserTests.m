//
//  CTFUserTests.m
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

@interface CTFUserTests : XCTestCase

@end

@implementation CTFUserTests {
    CoreDataService *_service;
    RKObjectManager *_manager;
}

- (void)setUp {
    _service = [[CoreDataService alloc] initForUnitTesting];
    
    _manager = [[RKObjectManager alloc] init];
    RKManagedObjectStore *managedObjectStore = [[RKManagedObjectStore alloc] initWithManagedObjectModel:_service.managedObjectModel];
    _manager.managedObjectStore = managedObjectStore;
    
    NSBundle *bundle = [NSBundle bundleWithIdentifier:[[NSBundle mainBundle].bundleIdentifier stringByAppendingString:@"Tests"]];
    [RKTestFixture setFixtureBundle:bundle];
    
    [super setUp];
}

- (void)tearDown {
    _service = nil;
    _manager = nil;
    [super tearDown];
}


- (void)testUserResponseMApping {
    id parsedJSON = [RKTestFixture parsedObjectWithContentsOfFixture:@"user-response.json"];
    
    /// Configure mapping
    RKEntityMapping *userMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFUser class]) inManagedObjectStore:_manager.managedObjectStore];
    [userMapping addAttributeMappingsFromDictionary:[CTFUser dictionaryForResponseMapping]];
    
    RKEntityMapping *characterMapping =
    [RKEntityMapping mappingForEntityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectStore:_manager.managedObjectStore];
    [characterMapping addAttributeMappingsFromDictionary:[CTFCharacter dictionaryResponseMapping]];
    
    [userMapping addPropertyMapping:[RKRelationshipMapping relationshipMappingFromKeyPath:@"characters" toKeyPath:@"characters" withMapping:characterMapping]];
    
    /// Configure expectations
    RKMappingTest *test = [RKMappingTest testForMapping:userMapping sourceObject:parsedJSON destinationObject:nil];
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
    /*
    CTFCharacter *firstCharacter = [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectContext:_service.managedObjectContext];
    firstCharacter.type = @(1);
    firstCharacter.totalTime = @(21);
    firstCharacter.totalScore = @(100);
    firstCharacter.health = @(100);
    firstCharacter.level = @(20);
    firstCharacter.active = @YES;
    
    CTFCharacter *secondCharacter = [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectContext:_service.managedObjectContext];
    secondCharacter.type = @(2);
    secondCharacter.totalTime = @(23);
    secondCharacter.totalScore = @(98);
    secondCharacter.health = @(55);
    secondCharacter.level = @(12);
    secondCharacter.active = @NO;
    
    NSOrderedSet *set = [[NSOrderedSet alloc] initWithArray:@[firstCharacter, secondCharacter]];*/
    RKPropertyMappingTestExpectation *charactersExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"characters" destinationKeyPath:@"characters" /*value:set*/];
    [test addExpectation:charactersExpectation];
    
    [test verify];
}


@end
