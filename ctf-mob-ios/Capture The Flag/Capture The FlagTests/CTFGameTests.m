//
//  CTFGameTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 03.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CoreDataService.h"
#import "CTFGame.h"
#import "CTFUser.h"

@interface CTFGameTests : XCTestCase

@end

@implementation CTFGameTests

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
    CTFGame *game = [[CTFGame alloc] initWithToken:token];
    XCTAssertNotNil(game, @"");
    XCTAssertEqualObjects(game.token, token, @"");
}

- (void)testShouldNotInitWithNilToken {
    CTFGame *game = [[CTFGame alloc] initWithToken:nil];
    XCTAssertNil(game, @"");
}

- (void)testShouldSetSharedGame {
    
    CTFGame *nilGame = [CTFGame sharedInstance];
    XCTAssertNil(nilGame, @"");
    
    CTFGame *game = [[CTFGame alloc] initWithToken:@"abc"];
    [CTFGame setSharedInstance:game];
    XCTAssertNotNil([CTFGame sharedInstance], @"");
    
    [CTFGame setSharedInstance:nil];
    XCTAssertNil([CTFGame sharedInstance], @"");
}

- (void)testShouldSetCurrentUser {
    CTFGame *game = [[CTFGame alloc] initWithToken:@"abc"];
    CTFUser *user = [CTFUser createObject];
    user.username = @"username";
    
    [game setCurrentUser:user];
    XCTAssertNotNil(game.currentUser, @"");
    XCTAssertEqualObjects(game.currentUser.username, user.username, @"");
}

@end
