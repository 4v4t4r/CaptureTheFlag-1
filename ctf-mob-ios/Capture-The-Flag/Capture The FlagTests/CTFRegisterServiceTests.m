//
//  CTFRegisterServiceTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.02.2014.
//  Copyright (c) 2014 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>

#import <OCMock/OCMock.h>

#import "CoreDataService.h"
#import "CTFAPIAccounts.h"
#import "CTFRegisterService.h"

@interface CTFRegisterServiceTests : XCTestCase

@end

@implementation CTFRegisterServiceTests

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

- (void)testThatRegisterServiceIsNotNilAfterInit {
    XCTAssertNotNil([CTFRegisterService new], @"");
}

- (void)testThatAccountsIsNotNilAfterInit {
    CTFRegisterService *service = [[CTFRegisterService alloc] initWithAccounts:[CTFAPIAccounts new]];
    XCTAssertNotNil(service.accounts, @"");
}

- (void)testThatRegisterStateDifferentPasswordsShouldBeReturnedWhenPasswordAreDifferent {
    CTFRegisterService *service = [[CTFRegisterService alloc] initWithAccounts:[CTFAPIAccounts new]];
    __block BOOL blockEvoked = NO;
    [service registerWithUsername:@"ABC" password:@"ABCD" rePassword:@"ABCDE" email:@"ABCD" responseBlock:^(RegisterState state) {
        blockEvoked = YES;
        XCTAssertEqual(state, RegisterStateDifferentPasswords, @"");
    }];
    
    if (!blockEvoked) {
        XCTFail(@"Block should be evoked!");
    }
}

- (void)testThatRegisterStateInProgressShouldBeReturnedWhenCredentialsAreValid {
    CTFRegisterService *service = [[CTFRegisterService alloc] initWithAccounts:[CTFAPIAccounts new]];
    __block BOOL blockEvoked = NO;
    [service registerWithUsername:@"ABC" password:@"ABCD" rePassword:@"ABCD" email:@"ABCD" responseBlock:^(RegisterState state) {
        if (state == RegisterStateInProgress) {
            blockEvoked = YES;
        }
    }];
    
    if (!blockEvoked) {
        XCTFail(@"Block should be evoked!");
    }
}

- (void)testThatRegisterStateSuccessShouldBeReturnedWhenCredentialsAreValidOnServer {
    id mockAccounts = [OCMockObject mockForClass:[CTFAPIAccounts class]];
    [[[mockAccounts expect] andDo:^(NSInvocation *invocation) {
        void (^RegisterBlock)(BOOL success) = nil;
        [invocation getArgument:&RegisterBlock atIndex:5];
        RegisterBlock(YES);
    }] signUpWithUsername:OCMOCK_ANY email:OCMOCK_ANY password:OCMOCK_ANY block:OCMOCK_ANY];
    
    
    CTFRegisterService *service = [[CTFRegisterService alloc] initWithAccounts:mockAccounts];
    __block BOOL blockEvoked = NO;
    [service registerWithUsername:@"ABC" password:@"ABCD" rePassword:@"ABCD" email:@"ABCD" responseBlock:^(RegisterState state) {
        if (state == RegisterStateSuccessful) {
            blockEvoked = YES;
        }
    }];
    
    if (!blockEvoked) {
        XCTFail(@"Block should be evoked!");
    }
}

- (void)testThatRegisterStateFailureShouldBeReturnedWhenCredentialsAreInvalidOnServer {
    id mockAccounts = [OCMockObject mockForClass:[CTFAPIAccounts class]];
    [[[mockAccounts expect] andDo:^(NSInvocation *invocation) {
        void (^RegisterBlock)(BOOL success) = nil;
        [invocation getArgument:&RegisterBlock atIndex:5];
        RegisterBlock(NO);
    }] signUpWithUsername:OCMOCK_ANY email:OCMOCK_ANY password:OCMOCK_ANY block:OCMOCK_ANY];
    
    
    CTFRegisterService *service = [[CTFRegisterService alloc] initWithAccounts:mockAccounts];
    __block BOOL blockEvoked = NO;
    [service registerWithUsername:@"ABC" password:@"ABCD" rePassword:@"ABCD" email:@"ABCD" responseBlock:^(RegisterState state) {
        if (state == RegisterStateFailure) {
            blockEvoked = YES;
        }
    }];
    
    if (!blockEvoked) {
        XCTFail(@"Block should be evoked!");
    }
}

@end
