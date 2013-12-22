//
//  CTFNoCharactersCellTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 22.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFBrowseCharactersViewController.h"
#import "CTFNoCharactersCell.h"

@interface CTFNoCharactersCellTests : XCTestCase

@end

@implementation CTFNoCharactersCellTests {
    UIStoryboard *storyboard;
    CTFBrowseCharactersViewController *vc;
    CTFNoCharactersCell *cell;
}

- (void)setUp
{
    [super setUp];
    
    storyboard = [UIStoryboard storyboardWithName:@"Main" bundle:nil];
    vc = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([CTFBrowseCharactersViewController class])];
    [vc view];
    cell = [vc.tableView dequeueReusableCellWithIdentifier:NSStringFromClass([CTFNoCharactersCell class])];
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

@end