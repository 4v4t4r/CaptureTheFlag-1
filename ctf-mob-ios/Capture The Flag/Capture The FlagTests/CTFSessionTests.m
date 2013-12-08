//
//  CTFSessionTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 03.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CoreDataService.h"
#import "CTFSession.h"
#import "CTFUser.h"

@interface CTFSessionTests : XCTestCase

@end

@implementation CTFSessionTests

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

- (void)testShouldInitWithToken {
    NSString *token = @"123456789";
    CTFSession *session = [[CTFSession alloc] initWithToken:token];
    XCTAssertNotNil(session, @"");
    XCTAssertEqualObjects(session.token, token, @"");
}

- (void)testShouldNotInitWithNilToken {
    CTFSession *session = [[CTFSession alloc] initWithToken:nil];
    XCTAssertNil(session, @"");
}

- (void)testShouldSetSharedGame {
    
    CTFSession *nilSession = [CTFSession sharedInstance];
    XCTAssertNil(nilSession, @"");
    
    CTFSession *session = [[CTFSession alloc] initWithToken:@"abc"];
    [CTFSession setSharedInstance:session];
    XCTAssertNotNil([CTFSession sharedInstance], @"");
    
    [CTFSession setSharedInstance:nil];
    XCTAssertNil([CTFSession sharedInstance], @"");
}

- (void)testShouldSetCurrentUser {
    CTFSession *session = [[CTFSession alloc] initWithToken:@"abc"];
    CTFUser *user = [CTFUser createObject];
    user.username = @"username";
    
    [session setCurrentUser:user];
    XCTAssertNotNil(session.currentUser, @"");
    XCTAssertEqualObjects(session.currentUser.username, user.username, @"");
}

@end
