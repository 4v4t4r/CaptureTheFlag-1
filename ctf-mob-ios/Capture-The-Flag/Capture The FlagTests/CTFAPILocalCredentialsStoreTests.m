//
//  CTFLocalCredentialsStoreTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import <OCMock/OCMock.h>
#import "CTFAPILocalCredentialsStore.h"
#import "CTFAPILocalCredentials.h"
#import "STKeychain.h"

@interface CTFAPILocalCredentialsStoreTests : XCTestCase

@end

@implementation CTFAPILocalCredentialsStoreTests

- (void)setUp {
    [super setUp];
    [CTFAPILocalCredentialsStore setSharedInstance:nil];
}

- (void)tearDown {
    [CTFAPILocalCredentialsStore setSharedInstance:nil];
    [super tearDown];
}

- (void)testShouldInitWithKeychain {
    CTFAPILocalCredentialsStore *store = [[CTFAPILocalCredentialsStore alloc] initWithKeychain:[STKeychain sharedInstance]];
    XCTAssertNotNil(store, @"");
}

- (void)testShouldNotInitWithNil {
    CTFAPILocalCredentialsStore *store = [[CTFAPILocalCredentialsStore alloc] initWithKeychain:nil];
    XCTAssertNil(store, @"");
}

- (void)testSharedInstanceShouldBeNotNilAfterAssignment {
    CTFAPILocalCredentialsStore *store = [[CTFAPILocalCredentialsStore alloc] initWithKeychain:[STKeychain sharedInstance]];
    XCTAssertNil([CTFAPILocalCredentialsStore sharedInstance], @"");
    [CTFAPILocalCredentialsStore setSharedInstance:store];
    XCTAssertNotNil([CTFAPILocalCredentialsStore sharedInstance], @"");
}

- (void)testShouldStoreCredentials {
    id mockKeychain = [OCMockObject mockForClass:[STKeychain class]];
    
    BOOL result = YES;
    NSError *error = nil;
    [[[mockKeychain expect] andReturnValue:OCMOCK_VALUE(result)] storeUsername:OCMOCK_ANY andPassword:OCMOCK_ANY forServiceName:OCMOCK_ANY updateExisting:YES error:[OCMArg setTo:error]];
    
    CTFAPILocalCredentials *credentials = [[CTFAPILocalCredentials alloc] initWithUsername:@"username" password:@"password"];
    
    CTFAPILocalCredentialsStore *store = [[CTFAPILocalCredentialsStore alloc] initWithKeychain:mockKeychain];
    [store storeCredentials:credentials];
    
    [mockKeychain verify];
}

- (void)testShouldNotStoreNilCredentials {
    CTFAPILocalCredentialsStore *store = [[CTFAPILocalCredentialsStore alloc] initWithKeychain:[STKeychain new]];
    BOOL stored = [store storeCredentials:nil];
    XCTAssertFalse(stored, @"");
}

- (void)testShouldGetStoredCredentials {
    id mockKeychain = [OCMockObject mockForClass:[STKeychain class]];
    
    NSString *username = @"username";
    NSString *password = @"password";
    
    NSString *storedPassword = [NSString stringWithFormat:@"%@,%@", username, password];
    NSError *error = nil;

    [[[mockKeychain expect] andReturn:storedPassword] getPasswordForUsername:OCMOCK_ANY andServiceName:OCMOCK_ANY error:[OCMArg setTo:error]];
    
    CTFAPILocalCredentialsStore *store = [[CTFAPILocalCredentialsStore alloc] initWithKeychain:mockKeychain];
    CTFAPILocalCredentials *credentials = [store getCredentials];
    [mockKeychain verify];

    XCTAssertNotNil(credentials, @"");
    XCTAssertEqualObjects(credentials.username, username, @"");
    XCTAssertEqualObjects(credentials.password, password, @"");
}

- (void)testThatSTKeychainShouldReturnErrorInStoreCredentials {
    id mockKeychain = [OCMockObject mockForClass:[STKeychain class]];
    CTFAPILocalCredentialsStore *store = [[CTFAPILocalCredentialsStore alloc] initWithKeychain:mockKeychain];
    id mockError = [OCMockObject niceMockForClass:[NSError class]];
    [[mockError expect] localizedDescription];
    
    [[[mockKeychain stub] andReturnValue:OCMOCK_VALUE((BOOL){NO})] storeUsername:OCMOCK_ANY andPassword:OCMOCK_ANY forServiceName:OCMOCK_ANY updateExisting:YES error:[OCMArg setTo:mockError]];
    
    CTFAPILocalCredentials *credentials = [[CTFAPILocalCredentials alloc] initWithUsername:@"ABC" password:@"PASS"];
    
    [store storeCredentials:credentials];
    [mockError verify];
}

- (void)testThatSTKeychainReturnsErrorInGetCredentials {
    id mockKeychain = [OCMockObject mockForClass:[STKeychain class]];

    CTFAPILocalCredentialsStore *store = [[CTFAPILocalCredentialsStore alloc] initWithKeychain:mockKeychain];
    id mockError = [OCMockObject niceMockForClass:[NSError class]];
    [[mockError expect] localizedDescription];
    
    [[[mockKeychain stub] andReturn:nil] getPasswordForUsername:OCMOCK_ANY andServiceName:OCMOCK_ANY error:[OCMArg setTo:mockError]];
    [store getCredentials];
    [mockError verify];
}

@end
