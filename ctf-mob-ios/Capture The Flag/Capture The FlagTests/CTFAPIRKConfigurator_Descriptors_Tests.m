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

- (void)testProfileResponseDescriptor {
    RKResponseDescriptor *descriptor = [self.configurator profileResponseDescriptor];
    XCTAssertEqual(descriptor.method, RKRequestMethodGET, @"");
    XCTAssertEqualObjects(descriptor.pathPattern, @"profile", @"");
}

@end
