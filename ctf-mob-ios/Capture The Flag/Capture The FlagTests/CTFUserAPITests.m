//
//  CTFUserAPITests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import <OCMock/OCMock.h>
#import "CTFUser.h"

@interface CTFUserAPITests : XCTestCase

@end

@implementation CTFUserAPITests

- (void)testUserShouldCanRegister {

    CTFUser *user = [CTFUser createObject];
    user.username = @"login";
    user.password = @"password";
    
    [CTFUser registerUser:user withBlock:^(bool success, NSError *error) {
        XCTAssertTrue(success, @"Should be success");
        XCTAssertNil(error, @"Should be nil");
    }];
}

@end
