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

- (void)testLoggedUser
{
    CTFUser *user = [CTFUser createObject];
    user.login = @"logged";
    [user loginUser];
    
    CTFUser *current = [CTFUser loggedUser];
    XCTAssertEqualObjects(user, current, @"Users should be identically");
}

- (void)testLoginUser
{
    CTFUser *user = [CTFUser createObject];
    user.login = @"login";
    BOOL result = [user loginUser];
    XCTAssertTrue(result, @"User should be logged in");
}

- (void)testPreventAttemptToLogInAnotherUser
{
    CTFUser *user = [CTFUser createObject];
    user.login = @"first";
    [user loginUser];

    CTFUser *otherUser = [CTFUser createObject];
    otherUser.login = @"second";
    
    BOOL result = [otherUser loginUser];
    XCTAssertFalse(result, @"Second user shouldn't be logged in");
}

- (void)testLogoutUser
{
    CTFUser *user = [CTFUser createObject];
    [user loginUser];
    
    [user logoutUser];
    XCTAssertNil([CTFUser loggedUser], @"Current user should be nil");
}

@end
