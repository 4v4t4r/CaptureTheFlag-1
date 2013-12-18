//
//  CTFAPIRKConfigurator_Descriptors_Tests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIRKConfiguratorTests.h"
#import "CTFAPIRKConfigurator.h"
#import "CTFAPIRKConfigurator+Descriptors.h"

@interface CTFAPIRKConfigurator_Descriptors_Tests : CTFAPIRKConfiguratorTests

@end

@implementation CTFAPIRKConfigurator_Descriptors_Tests

- (void)setUp
{
    [super setUp];
    self.configurator = [[CTFAPIRKConfigurator alloc] initWithManager:self.manager];
}

- (void)tearDown
{
    [super tearDown];
}


#pragma mark - /api/users/
- (void)testUsersPOSTRequestDescriptor {
    RKRequestDescriptor *descriptor = [self.configurator usersPOSTRequestDescriptor];
    XCTAssertEqual(descriptor.method, RKRequestMethodPOST, @"");
    XCTAssertEqualObjects(descriptor.objectClass, [CTFUser class], @"");
    XCTAssertNil(descriptor.rootKeyPath, @"");
}

- (void)testUsersPOSTResponseDescriptor {
    RKResponseDescriptor *descriptor = [self.configurator usersPOSTResponseDescriptor];
    XCTAssertEqual(descriptor.method, RKRequestMethodPOST, @"");
    XCTAssertNil(descriptor.keyPath, @"");
    XCTAssertEqualObjects(descriptor.pathPattern, @"/api/users/", @"");
}


#pragma mark - /api/profile
- (void)testProfileResponseDescriptor {
    RKResponseDescriptor *descriptor = [self.configurator profileResponseDescriptor];
    XCTAssertEqual(descriptor.method, RKRequestMethodGET, @"");
    XCTAssertNil(descriptor.keyPath, @"");
    XCTAssertEqualObjects(descriptor.pathPattern, @"/api/profile", @"");
}

@end
