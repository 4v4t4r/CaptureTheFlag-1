//
//  CTFJoinGameCellTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 19.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFJoinViewController.h"
#import "CTFJoinGameCell.h"

@interface CTFJoinGameCellTests : XCTestCase

@end

@implementation CTFJoinGameCellTests {
    UIStoryboard *storyboard;
    CTFJoinViewController *vc;
    CTFJoinGameCell *cell;
}

- (void)setUp
{
    [super setUp];

    storyboard = [UIStoryboard storyboardWithName:@"Main" bundle:nil];
    vc = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([CTFJoinViewController class])];
    [vc view];
    cell = [vc.tableView dequeueReusableCellWithIdentifier:NSStringFromClass([CTFJoinGameCell class])];
}

- (void)tearDown
{
    cell = nil;
    vc = nil;
    storyboard = nil;
    [super tearDown];
}

- (void)testThatCellExists
{
    XCTAssertNotNil(cell, @"");
}

- (void)testThatTitleLabelExists {
    XCTAssertNotNil(cell.titleLabel, @"");
}

- (void)testThatDistanceLabelExists {
    XCTAssertNotNil(cell.distanceLabel, @"");
}

- (void)testThatStartDateLabelExists {
    XCTAssertNotNil(cell.startDateLabel, @"");
}

@end
