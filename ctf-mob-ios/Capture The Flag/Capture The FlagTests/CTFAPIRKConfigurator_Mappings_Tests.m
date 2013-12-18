//
//  CTFAPIRKConfigurator_Mappings_Tests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIRKConfiguratorTests.h"
#import "CTFAPIRKConfigurator+Mappings.h"
#import <RestKit/RestKit.h>
#import <RestKit/Testing.h>

#import "CoreDataService.h"
#import "CTFCharacter.h"
#import "CTFMap.h"
#import "CTFUser.h"
#import "CTFItem.h"
#import "CTFGame.h"

@interface CTFAPIRKConfigurator_Mappings_Tests : CTFAPIRKConfiguratorTests
@end

@implementation CTFAPIRKConfigurator_Mappings_Tests

- (void)setUp
{
    [super setUp];
    self.configurator = [[CTFAPIRKConfigurator alloc] initWithManager:self.manager];

    NSBundle *bundle = [NSBundle bundleWithIdentifier:[[NSBundle mainBundle].bundleIdentifier stringByAppendingString:@"Tests"]];
    [RKTestFixture setFixtureBundle:bundle];
}

- (void)tearDown
{
    [super tearDown];
}


#pragma mark - Factory
- (void)testFactoryShouldReturnNilMappingWhenMappingNotExists {
    RKEntityMapping *mapping = [self.configurator entityMappingFromClass:[NSObject class]];
    XCTAssertNil(mapping, @"");
}

- (void)testFactoryShouldReturnMappingForCharacter {
    Class aClass = [CTFCharacter class];
    RKEntityMapping *mapping = [self.configurator entityMappingFromClass:aClass];
    XCTAssertEqualObjects(mapping.entity.name, NSStringFromClass(aClass), @"");
}

- (void)testFactoryShouldReturnMappingForGame {
    Class aClass = [CTFGame class];
    RKEntityMapping *mapping = [self.configurator entityMappingFromClass:aClass];
    XCTAssertEqualObjects(mapping.entity.name, NSStringFromClass(aClass), @"");
}

- (void)testFactoryShouldReturnMappingForItem {
    Class aClass = [CTFItem class];
    RKEntityMapping *mapping = [self.configurator entityMappingFromClass:aClass];
    XCTAssertEqualObjects(mapping.entity.name, NSStringFromClass(aClass), @"");
}

- (void)testFactoryShouldReturnMappingForMap {
    Class aClass = [CTFMap class];
    RKEntityMapping *mapping = [self.configurator entityMappingFromClass:aClass];
    XCTAssertEqualObjects(mapping.entity.name, NSStringFromClass(aClass), @"");
}

- (void)testFactoryShouldReturnMappingForUser {
    Class aClass = [CTFUser class];
    RKEntityMapping *mapping = [self.configurator entityMappingFromClass:aClass];
    XCTAssertEqualObjects(mapping.entity.name, NSStringFromClass(aClass), @"");
}


#pragma mark - Mappings
- (void)testUserMapping {
    id parsedJSON = [RKTestFixture parsedObjectWithContentsOfFixture:@"user-response.json"];
    
    /// Configure expectations
    RKMappingTest *test = [RKMappingTest testForMapping:[self.configurator entityMappingFromClass:[CTFUser class]] sourceObject:parsedJSON destinationObject:nil];
    test.managedObjectContext = self.service.managedObjectContext;
    
    RKPropertyMappingTestExpectation *idExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"id" destinationKeyPath:@"userId" value:@(1)];
    [test addExpectation:idExpectation];
    
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
        
        NSArray *commitedKeysToCompare = @[@"type, totalTime, totalScore, health, level"];
        
        NSArray *array = [mappedValue allObjects];
        BOOL containsTwoItems = array.count == 2;
        
        /// Test First character
        CTFCharacter *firstFetchedCharacter = (CTFCharacter *)array[0];
        
        CTFCharacter *firstReferenceCharacter = [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectContext:self.service.managedObjectContext];
        firstReferenceCharacter.type = @(1);
        firstReferenceCharacter.totalTime = @(21);
        firstReferenceCharacter.totalScore = @(100);
        firstReferenceCharacter.health = @(100);
        firstReferenceCharacter.level = @(20);
        
        BOOL isFirstEqual = [[firstFetchedCharacter committedValuesForKeys:commitedKeysToCompare] isEqual:[firstReferenceCharacter committedValuesForKeys:commitedKeysToCompare]];
        
        /// Test Second character
        CTFCharacter *secondFetchedCharacter = (CTFCharacter *)array[1];
        
        CTFCharacter *secondReferenceCharacter = [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectContext:self.service.managedObjectContext];
        secondReferenceCharacter.type = @(2);
        secondReferenceCharacter.totalTime = @(23);
        secondReferenceCharacter.totalScore = @(98);
        secondReferenceCharacter.health = @(55);
        secondReferenceCharacter.level = @(12);
        
        BOOL isSecondEqual = [[secondFetchedCharacter committedValuesForKeys:commitedKeysToCompare] isEqual:[secondReferenceCharacter committedValuesForKeys:commitedKeysToCompare]];
        
        return isFirstEqual && isSecondEqual && containsTwoItems;
    }];
    
    [test addExpectation:charactersExpectation];
    
    [test verify];
}

- (void)testCharacterMapping {
    id parsedJSON = [RKTestFixture parsedObjectWithContentsOfFixture:@"character-response.json"];
    
    RKMappingTest *test = [RKMappingTest testForMapping:[self.configurator entityMappingFromClass:[CTFCharacter class]] sourceObject:parsedJSON destinationObject:nil];
    test.managedObjectContext = self.service.managedObjectContext;
    
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
    
    RKPropertyMappingTestExpectation *userExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"user" destinationKeyPath:@"user"];
    [test addExpectation:userExpectation];
    
    [test verify];
}

- (void)testMapMapping {
    id parsedJSON = [RKTestFixture parsedObjectWithContentsOfFixture:@"map-response.json"];
    
    RKMappingTest *test = [RKMappingTest testForMapping:[self.configurator entityMappingFromClass:[CTFMap class]] sourceObject:parsedJSON destinationObject:nil];
    test.managedObjectContext = self.service.managedObjectContext;
    
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

- (void)testGameMapping {
    id parsedJSON = [RKTestFixture parsedObjectWithContentsOfFixture:@"game-response.json"];
    
    RKMappingTest *test = [RKMappingTest testForMapping:[self.configurator entityMappingFromClass:[CTFGame class]] sourceObject:parsedJSON destinationObject:nil];
    test.managedObjectContext = self.service.managedObjectContext;
    
    RKPropertyMappingTestExpectation *idExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"id" destinationKeyPath:@"gameId" value:@"12345hash"];
    [test addExpectation:idExpectation];
    
    RKPropertyMappingTestExpectation *nameExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"name" destinationKeyPath:@"name" value:@"game's name"];
    [test addExpectation:nameExpectation];
    
    RKPropertyMappingTestExpectation *descriptionExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"description" destinationKeyPath:@"desc" value:@"game description"];
    [test addExpectation:descriptionExpectation];
    
    RKPropertyMappingTestExpectation *statusExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"status" destinationKeyPath:@"status" value:@(1)];
    [test addExpectation:statusExpectation];
    
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
    [dateFormatter setDateFormat:@"dd-MM-YYYY hh-mm-ss"];
    NSDate *startTimeDate = [dateFormatter dateFromString:@"14-12-2013 15:13:00"];
    
    RKPropertyMappingTestExpectation *startTimeExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"start_time" destinationKeyPath:@"startTime" value:startTimeDate];
    [test addExpectation:startTimeExpectation];
    
    RKPropertyMappingTestExpectation *maxPlayersEpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"max_players" destinationKeyPath:@"maxPlayers" value:@(6)];
    [test addExpectation:maxPlayersEpectation];
    
    RKPropertyMappingTestExpectation *typeExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"type" destinationKeyPath:@"type" value:@(1)];
    [test addExpectation:typeExpectation];
    
    RKPropertyMappingTestExpectation *mapExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"map" destinationKeyPath:@"map" evaluationBlock:^BOOL(RKPropertyMappingTestExpectation *expectation, RKPropertyMapping *mapping, id mappedValue, NSError *__autoreleasing *error) {
        return (mappedValue != nil);
    }];
    [test addExpectation:mapExpectation];
    
    RKPropertyMappingTestExpectation *playersExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"players" destinationKeyPath:@"players" evaluationBlock:^BOOL(RKPropertyMappingTestExpectation *expectation, RKPropertyMapping *mapping, id mappedValue, NSError *__autoreleasing *error) {
        return ([mappedValue count] == 2);
    }];
    [test addExpectation:playersExpectation];
    
    RKPropertyMappingTestExpectation *itemsExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"items" destinationKeyPath:@"items" evaluationBlock:^BOOL(RKPropertyMappingTestExpectation *expectation, RKPropertyMapping *mapping, id mappedValue, NSError *__autoreleasing *error) {
        return ([mappedValue count] == 2);
    }];
    [test addExpectation:itemsExpectation];
    
    NSDate *createdDate = [dateFormatter dateFromString:@"10-11-2013 13:12:00"];
    RKPropertyMappingTestExpectation *createdDateExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"created_date" destinationKeyPath:@"createdDate" value:createdDate];
    [test addExpectation:createdDateExpectation];
    
    NSDate *modifiedDate = [dateFormatter dateFromString:@"11-12-2013 10:15:00"];
    RKPropertyMappingTestExpectation *modifiedDateExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"modified_date" destinationKeyPath:@"modifiedDate" value:modifiedDate];
    [test addExpectation:modifiedDateExpectation];
}

- (void)testItemMapping {
    id parsedJSON = [RKTestFixture parsedObjectWithContentsOfFixture:@"item-response.json"];
    
    RKMappingTest *test = [RKMappingTest testForMapping:[self.configurator entityMappingFromClass:[CTFItem class]] sourceObject:parsedJSON destinationObject:nil];
    test.managedObjectContext = self.service.managedObjectContext;
    
    RKPropertyMappingTestExpectation *nameExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"name" destinationKeyPath:@"name" value:@"item's name"];
    [test addExpectation:nameExpectation];
    
    RKPropertyMappingTestExpectation *descriptionExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"description" destinationKeyPath:@"desc" value:@"item's description"];
    [test addExpectation:descriptionExpectation];
    
    RKPropertyMappingTestExpectation *typeExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"type" destinationKeyPath:@"type" value:@(1)];
    [test addExpectation:typeExpectation];
    
    RKPropertyMappingTestExpectation *locationExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"location" destinationKeyPath:@"location" value:@[@(10), @(20)]];
    [test addExpectation:locationExpectation];
    
    RKPropertyMappingTestExpectation *valueExpectation =
    [RKPropertyMappingTestExpectation expectationWithSourceKeyPath:@"value" destinationKeyPath:@"value" value:@(10.4)];
    [test addExpectation:valueExpectation];
    
    [test verify];
}


@end
