//
//  CTFAPIRKConfiguratorTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIRKConfiguratorTests.h"
#import "CoreDataService.h"
#import "CTFAPIRKConfigurator.h"

@implementation CTFAPIRKConfiguratorTests

- (void)setUp
{
    [super setUp];
    _service = [[CoreDataService alloc] initForUnitTesting];
    
    _manager = [RKObjectManager managerWithBaseURL:[NSURL URLWithString:@"http://baseurl.com/api"]];
    RKManagedObjectStore *managedObjectStore = [[RKManagedObjectStore alloc] initWithPersistentStoreCoordinator:_service.persistentStoreCoordinator];
    _manager.managedObjectStore = managedObjectStore;
    [_manager.managedObjectStore createManagedObjectContexts];
}

- (void)tearDown
{
    _service = nil;
    _manager = nil;
    _configurator = nil;
    [super tearDown];
}

- (void)testInstanceShouldBeNotNil {
    _configurator = [[CTFAPIRKConfigurator alloc] initWithManager:_manager];
    XCTAssertNotNil(_configurator, @"");
}

- (void)testInstanceShouldBeNilWithoutManager {
    _configurator = [[CTFAPIRKConfigurator alloc] initWithManager:nil];
    XCTAssertNil(_configurator, @"");
}

- (void)testConfigure {
    _configurator = [[CTFAPIRKConfigurator alloc] initWithManager:_manager];
    [_configurator configure];

    XCTAssert(_manager.responseDescriptors > 0, @"");
}


@end
