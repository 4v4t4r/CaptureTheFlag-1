//
//  CTFLocalCredentialsStoreTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import <OCMock/OCMock.h>
#import "CTFLocalCredentialsStore.h"
#import "CTFLocalCredentials.h"
#import "STKeychain.h"

@interface CTFLocalCredentialsStoreTests : XCTestCase

@end

@implementation CTFLocalCredentialsStoreTests

- (void)setUp {
    [super setUp];
    [CTFLocalCredentialsStore setSharedInstance:nil];
}

- (void)tearDown {
    [CTFLocalCredentialsStore setSharedInstance:nil];
    [super tearDown];
}

- (void)testShouldInitWithKeychain {
    CTFLocalCredentialsStore *store = [[CTFLocalCredentialsStore alloc] initWithKeychain:[STKeychain sharedInstance]];
    XCTAssertNotNil(store, @"");
}

- (void)testShouldNotInitWithNil {
    CTFLocalCredentialsStore *store = [[CTFLocalCredentialsStore alloc] initWithKeychain:nil];
    XCTAssertNil(store, @"");
}

- (void)testSharedInstanceShouldBeNotNilAfterAssignment {
    CTFLocalCredentialsStore *store = [[CTFLocalCredentialsStore alloc] initWithKeychain:[STKeychain sharedInstance]];
    XCTAssertNil([CTFLocalCredentialsStore sharedInstance], @"");
    [CTFLocalCredentialsStore setSharedInstance:store];
    XCTAssertNotNil([CTFLocalCredentialsStore sharedInstance], @"");
}

- (void)testShouldStoreCredentials {
    id mockKeychain = [OCMockObject mockForClass:[STKeychain class]];
    
    BOOL result = YES;
    [[[mockKeychain expect] andReturnValue:OCMOCK_VALUE(result)] storeUsername:OCMOCK_ANY andPassword:OCMOCK_ANY forServiceName:OCMOCK_ANY updateExisting:YES error:nil];
    
    CTFLocalCredentials *credentials = [[CTFLocalCredentials alloc] initWithUsername:@"username" password:@"password"];
    
    CTFLocalCredentialsStore *store = [[CTFLocalCredentialsStore alloc] initWithKeychain:mockKeychain];
    [store storeCredentials:credentials];
    
    [mockKeychain verify];
}

- (void)testShouldGetStoredCredentials {
    id mockKeychain = [OCMockObject mockForClass:[STKeychain class]];
    
    NSString *username = @"username";
    NSString *password = @"password";
    
    NSString *storedPassword = [NSString stringWithFormat:@"%@,%@", username, password];
    [[[mockKeychain expect] andReturn:storedPassword] getPasswordForUsername:OCMOCK_ANY andServiceName:OCMOCK_ANY error:nil];
    
    CTFLocalCredentialsStore *store = [[CTFLocalCredentialsStore alloc] initWithKeychain:mockKeychain];
    CTFLocalCredentials *credentials = [store getCredentials];
    [mockKeychain verify];

    XCTAssertNotNil(credentials, @"");
    XCTAssertEqualObjects(credentials.username, username, @"");
    XCTAssertEqualObjects(credentials.password, password, @"");
}

@end
