//
//  CTFAPIAccountsTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import <RestKit/RestKit.h>
#import <OCMock/OCMock.h>
#import "CTFAPIConnection.h"
#import "CTFAPIAccounts.h"

@interface CTFAPIAccountsTests : XCTestCase

@end

@implementation CTFAPIAccountsTests {
    CTFAPIAccounts *_accounts;
}

- (void)setUp
{
    [super setUp];
}

- (void)tearDown
{
    _accounts = nil;
    [super tearDown];
}

- (void)testShouldInitObject {
    RKObjectManager *manager = [[RKObjectManager alloc] init];
    CTFAPIConnection *connection = [[CTFAPIConnection alloc] initWithManager:manager];
    _accounts = [[CTFAPIAccounts alloc] initWithConnection:connection];
    XCTAssertNotNil(_accounts, @"");
}


#pragma mark - signInWithUsername:andPassword:withBlock:
- (void)testRequestShouldnotBeCalledWhenCredentialsAreNil {
    id mockClient = [OCMockObject mockForClass:[AFHTTPClient class]];
    
    RKObjectManager *manager = [[RKObjectManager alloc] init];
    manager.HTTPClient = mockClient;
    
    CTFAPIConnection *connection = [[CTFAPIConnection alloc] initWithManager:manager];
    
    _accounts = [[CTFAPIAccounts alloc] initWithConnection:connection];
    id accountsMock = [OCMockObject partialMockForObject:_accounts];
    
    [accountsMock signInWithUsername:nil andPassword:nil withBlock:nil]; // 1
    [accountsMock signInWithUsername:OCMOCK_ANY andPassword:nil withBlock:nil]; // 2
    [accountsMock signInWithUsername:nil andPassword:OCMOCK_ANY withBlock:nil]; // 3
    [mockClient verify];
}

- (void)testTokenInBlockShouldBeNotNil {

    id mockClient = [OCMockObject mockForClass:[AFHTTPClient class]];
    
    [[[mockClient expect] andDo:^(NSInvocation *invocation) {
        
        void (^successBlock)(AFHTTPRequestOperation *operation, id responseObject) = nil;
        
        [invocation getArgument:&successBlock atIndex:4];
        
        successBlock(nil, @{@"access_token" : @"this_is_a_token"});
        
    }] getPath:[OCMArg any] parameters:[OCMArg any] success:[OCMArg any] failure:[OCMArg any]] ;
    
    RKObjectManager *manager = [[RKObjectManager alloc] init];
    manager.HTTPClient = mockClient;
    
    TokenBlock block = ^(NSString *token) {
        XCTAssertNotNil(token, @"");
    };
    
    CTFAPIConnection *connection = [[CTFAPIConnection alloc] initWithManager:manager];
    _accounts = [[CTFAPIAccounts alloc] initWithConnection:connection];
    id mockAccounts = [OCMockObject partialMockForObject:_accounts];
    [mockAccounts signInWithUsername:[OCMArg any] andPassword:[OCMArg any] withBlock:block];
}

- (void)testTokenInBlockShouldBeNil {
    
    id mockClient = [OCMockObject mockForClass:[AFHTTPClient class]];
    
    [[[mockClient expect] andDo:^(NSInvocation *invocation) {
        
        void (^failureBlock)(AFHTTPRequestOperation *operation, NSError *error) = nil;
        
        [invocation getArgument:&failureBlock atIndex:4];
        
        failureBlock(nil, nil);
        
    }] getPath:[OCMArg any] parameters:[OCMArg any] success:[OCMArg any] failure:[OCMArg any]] ;
    
    RKObjectManager *manager = [[RKObjectManager alloc] init];
    manager.HTTPClient = mockClient;
    
    TokenBlock block = ^(NSString *token) {
        XCTAssertNil(token, @"");
    };
    
    CTFAPIConnection *connection = [[CTFAPIConnection alloc] initWithManager:manager];
    _accounts = [[CTFAPIAccounts alloc] initWithConnection:connection];
    id mockAccounts = [OCMockObject partialMockForObject:_accounts];
    [mockAccounts signInWithUsername:[OCMArg any] andPassword:[OCMArg any] withBlock:block];
}

@end
