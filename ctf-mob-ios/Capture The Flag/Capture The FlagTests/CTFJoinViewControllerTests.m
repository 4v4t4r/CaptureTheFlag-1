//
//  CTFJoinViewControllerTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 16.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFJoinViewController.h"

@interface CTFJoinViewControllerTests : XCTestCase

@end

@implementation CTFJoinViewControllerTests {
    UIStoryboard *storyboard;
    CTFJoinViewController *vc;
}

- (void)setUp
{
    [super setUp];
    storyboard = [UIStoryboard storyboardWithName:@"Main" bundle:nil];
    vc = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([CTFJoinViewController class])];
    [vc view];
}

- (void)tearDown
{
    storyboard = nil;
    vc = nil;
    [super tearDown];
}

- (void)testControllerShouldBeDelegateOfTableView {
    XCTAssertEqualObjects(vc.tableView.delegate, vc, @"");
}

- (void)testTitleOfViewShouldBeSet {
    XCTAssertEqualObjects(vc.navigationItem.title, NSLocalizedString(@"view.join.navigation.title", nil), @"");
}

- (void)testTableViewShouldNotBeNil {
    XCTAssertNotNil(vc.tableView, @"");
}


#pragma mark - Data Source tests
- (void)testDataSourceShouldBeNotNil {
    XCTAssertNotNil(vc.dataSource, @"");
}

- (void)testTableViewDataSourceShouldBeEqualToArrayDataSource {
    XCTAssertEqualObjects(vc.tableView.dataSource, vc.dataSource, @"");
}

@end
