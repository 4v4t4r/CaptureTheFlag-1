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

- (void)testDataSource {
    /// Data Source
    [dataSource setItems:@[@"A"]];
    [dataSource setConfigureCellBlock:^(UITableViewCell *cell, NSString *object) {
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Warc-retain-cycles"
        XCTAssertNotNil(cell, @"");
        XCTAssertEqualObjects(object, @"A", @"");
#pragma clang diagnostic pop
    }];
    
    /// Table View
    id mockTableView = [OCMockObject niceMockForClass:[UITableView class]];
    [[[mockTableView stub] andReturn:dataSource] dataSource];
    
    UITableViewCell *cell = [[UITableViewCell alloc] initWithFrame:CGRectMake(0, 0, 320, 44)];
    NSIndexPath *indexPath = [NSIndexPath indexPathForRow:0 inSection:0];
    
    [[[mockTableView stub] andReturn:cell] dequeueReusableCellWithIdentifier:OCMOCK_ANY forIndexPath:OCMOCK_ANY];

    [[mockTableView dataSource] tableView:mockTableView cellForRowAtIndexPath:indexPath];
}


@end
