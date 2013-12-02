//
//  CTFAPIConnectionTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import <RestKit/RestKit.h>
#import "CTFAPIConnection.h"

@interface CTFAPIConnectionTests : XCTestCase

@end

@implementation CTFAPIConnectionTests

- (void)setUp {
    [super setUp];
    [CTFAPIConnection setSharedConnection:nil];
}

- (void)tearDown {
    [CTFAPIConnection setSharedConnection:nil];
    [super tearDown];
}

- (void)testShouldInitWithRKObjectManager {
    RKObjectManager *objectManager = [[RKObjectManager alloc] init];
    
    CTFAPIConnection *connection = [[CTFAPIConnection alloc] initWithManager:objectManager];
    XCTAssertNotNil(connection, @"");
    XCTAssertNotNil(connection.manager, @"");
}

- (void)testSharedConnectionShouldBeNilAfterInitialization {
    RKObjectManager *objectManager = [[RKObjectManager alloc] init];
    CTFAPIConnection *connection = [[CTFAPIConnection alloc] initWithManager:objectManager];
    [connection description]; /// called only for removing warning about unused object
    
    CTFAPIConnection *sharedConnection = [CTFAPIConnection sharedConnection];
    XCTAssertNil(sharedConnection, @"");
}

- (void)testSharedConnectionShouldBeSet {
    RKObjectManager *objectManager = [[RKObjectManager alloc] init];
    CTFAPIConnection *connection = [[CTFAPIConnection alloc] initWithManager:objectManager];
    [CTFAPIConnection setSharedConnection:connection];
    XCTAssertNotNil([CTFAPIConnection sharedConnection], @"");
}

@end
