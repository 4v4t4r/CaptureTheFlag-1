//
//  CTFUserTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 19.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CoreDataService.h"
#import "CTFUser.h"
@interface CTFUserTests : XCTestCase

@end

@implementation CTFUserTests

- (void)setUp
{
    [super setUp];
    CoreDataService *service = [[CoreDataService alloc] initForUnitTesting];
    [CoreDataService setSharedInstance:service];
}

- (void)tearDown
{
    [CoreDataService setSharedInstance:nil];
    [super tearDown];
}

- (void)testCreateUser
{
    CTFUser *user = [CTFUser createObject];
    XCTAssertNotNil(user, @"User not nil");
    XCTAssertNotNil(user.managedObjectContext, @"User should have its moc");
}

@end
