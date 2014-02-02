//
//  CTFLoginServiceTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.02.2014.
//  Copyright (c) 2014 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>

#import <OCMock/OCMock.h>

#import "CoreDataService.h"
#import "CTFAPIAccounts.h"
#import "CTFLoginService.h"
#import "CTFAPIConnection.h"
#import "CTFSession.h"

@interface CTFLoginServiceTests : XCTestCase

@end

@implementation CTFLoginServiceTests

- (void)setUp {
    [super setUp];
    CoreDataService *service = [[CoreDataService alloc] initForUnitTesting];
    [CoreDataService setSharedInstance:service];
    [CTFSession setSharedInstance:nil];
}

- (void)tearDown {
    [CTFSession setSharedInstance:nil];
    [CoreDataService setSharedInstance:nil];
    [super tearDown];
}

- (void)testThatServiceShouldNotBeNil {
    CTFLoginService *service = [[CTFLoginService alloc] initWithAccounts:[CTFAPIAccounts new]];
    XCTAssertNotNil(service, @"");
}

- (void)testThatAccountsPropertyShouldBeSetAfterInit {
    CTFAPIAccounts *_acc = [[CTFAPIAccounts alloc] initWithConnection:[CTFAPIConnection new]];
    CTFLoginService *service = [[CTFLoginService alloc] initWithAccounts:_acc];
    XCTAssertNotNil(service.accounts, @"");
}

#pragma mark - logInWithUsername:password:responseBlock:
- (void)testThatMethodShouldReturnWrongCredentialsStateIfCredentialAreNotValid {
    CTFLoginService *service = [[CTFLoginService alloc] initWithAccounts:[CTFAPIAccounts new]];

    __block BOOL blockEvoked = NO;
    [service logInWithUsername:@"abc" password:@"def" responseBlock:^(LoginState state) {
        XCTAssertEqual((NSUInteger)state, (NSUInteger)LoginStateCredentialsNotValid, @"");
        blockEvoked = YES;
    }];
    
    if (!blockEvoked) {
        XCTFail(@"Block should be evoked!");
    }
}

- (void)testThatMethodShouldReturnLoginStateInProgressWhenCredentialsAreValid {
    CTFLoginService *service = [[CTFLoginService alloc] initWithAccounts:[CTFAPIAccounts new]];

    __block BOOL blockEvoked = NO;
    [service logInWithUsername:@"testuser1" password:@"password123" responseBlock:^(LoginState state) {
        if (state == LoginStateInProgress) {
            XCTAssertEqual((NSUInteger)state, (NSUInteger)LoginStateInProgress, @"");
            blockEvoked = YES;
        }
    }];
    
    if (!blockEvoked) {
        XCTFail(@"Block should be evoked!");
    }
}

- (void)testThatMethodShouldReturnLoginStateSuccessful {
    id mockAccounts = [OCMockObject mockForClass:[CTFAPIAccounts class]];
    [[[mockAccounts expect] andDo:^(NSInvocation *invocation) {
        void (^TokenBlock)(NSString *token) = nil;
        [invocation getArgument:&TokenBlock atIndex:4];
        TokenBlock(@"ABDFDSD");
    }] signInWithUsername:OCMOCK_ANY andPassword:OCMOCK_ANY withBlock:OCMOCK_ANY];
    
    CTFLoginService *service = [[CTFLoginService alloc] initWithAccounts:mockAccounts];
    
    __block BOOL blockEvoked = NO;
    [service logInWithUsername:@"testuser1" password:@"password123" responseBlock:^(LoginState state) {
        if (state == LoginStateSuccessful) {
            XCTAssertEqual((NSUInteger)state, (NSUInteger)LoginStateSuccessful, @"");
            blockEvoked = YES;
        }
    }];
    
    if (!blockEvoked) {
        XCTFail(@"Block should be evoked!");
    }
}

- (void)testThatMethodShouldReturnLoginStateFailure {
    id mockAccounts = [OCMockObject mockForClass:[CTFAPIAccounts class]];
    [[[mockAccounts expect] andDo:^(NSInvocation *invocation) {
        void (^TokenBlock)(NSString *token) = nil;
        [invocation getArgument:&TokenBlock atIndex:4];
        TokenBlock(nil);
    }] signInWithUsername:OCMOCK_ANY andPassword:OCMOCK_ANY withBlock:OCMOCK_ANY];
    
    CTFLoginService *service = [[CTFLoginService alloc] initWithAccounts:mockAccounts];
    
    __block BOOL blockEvoked = NO;
    [service logInWithUsername:@"testuser1" password:@"password123" responseBlock:^(LoginState state) {
        if (state == LoginStateFailure) {
            XCTAssertEqual((NSUInteger)state, (NSUInteger)LoginStateFailure, @"");
            blockEvoked = YES;
        }
    }];
    
    if (!blockEvoked) {
        XCTFail(@"Block should be evoked!");
    }
}

- (void)testThatCTFSessionSingletonShouldBeSetAfterLogIn {
    id mockAccounts = [OCMockObject mockForClass:[CTFAPIAccounts class]];
    [[[mockAccounts expect] andDo:^(NSInvocation *invocation) {
        void (^TokenBlock)(NSString *token) = nil;
        [invocation getArgument:&TokenBlock atIndex:4];
        TokenBlock(@"ABDFDSD");
    }] signInWithUsername:OCMOCK_ANY andPassword:OCMOCK_ANY withBlock:OCMOCK_ANY];
    
    CTFLoginService *service = [[CTFLoginService alloc] initWithAccounts:mockAccounts];
    
    __block BOOL blockEvoked = NO;
    [service logInWithUsername:@"testuser1" password:@"password123" responseBlock:^(LoginState state) {
        if (state == LoginStateSuccessful) {
            XCTAssertNotNil([CTFSession sharedInstance] , @"");
            blockEvoked = YES;
        }
    }];
    
    if (!blockEvoked) {
        XCTFail(@"Block should be evoked!");
    }
}

@end
