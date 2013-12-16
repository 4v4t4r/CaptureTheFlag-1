//
//  ArrayDataSourceTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 16.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import <OCMock/OCMock.h>
#import "ArrayDataSource.h"

@interface ArrayDataSourceTests : XCTestCase

@end

@implementation ArrayDataSourceTests {
    ArrayDataSource *dataSource;
}

- (void)setUp {
    
    NSArray *items = @[@"1", @"2"];
    NSString *cellIdentifier = @"cellIdentifier";
    
    ConfigureCellBlock block = ^(id cell, id object) {
        NSLog(@"do something");
    };
    
    dataSource =
    [[ArrayDataSource alloc] initWithItems:items
                            cellIdentifier:cellIdentifier
                        configureCellBlock:block];
    [super setUp];
}

- (void)tearDown {
    dataSource = nil;
    [super tearDown];
}

- (void)testArrayDataSourceShouldBeSetWithCorrectObjects {
    XCTAssertNotNil(dataSource.items, @"");
    XCTAssertNotNil(dataSource.cellIdentifier, @"");
}

- (void)testSetItems {
    NSArray *array = @[@"3"];
    [dataSource setItems:array];
    XCTAssertEqualObjects(dataSource.items, array, @"");
}

- (void)testSetCellIdentifier {
    NSString *cellIdentifier = @"customIdentifier";
    [dataSource setCellIdentifier:cellIdentifier];
    XCTAssertEqualObjects(dataSource.cellIdentifier, cellIdentifier, @"");
}


@end
