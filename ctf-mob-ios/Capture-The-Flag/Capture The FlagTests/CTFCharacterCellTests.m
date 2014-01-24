//
//  CTFCharacterCellTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 22.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFBrowseCharactersViewController.h"
#import "CTFCharacterCell.h"

@interface CTFCharacterCellTests : XCTestCase

@end

@implementation CTFCharacterCellTests {
    UIStoryboard *storyboard;
    CTFBrowseCharactersViewController *vc;
    CTFCharacterCell *cell;
}

- (void)setUp
{
    [super setUp];
    
    storyboard = [UIStoryboard storyboardWithName:@"Main" bundle:nil];
    vc = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([CTFBrowseCharactersViewController class])];
    [vc view];
    cell = [vc.tableView dequeueReusableCellWithIdentifier:NSStringFromClass([CTFCharacterCell class])];
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

#pragma mark - Outlets
- (void)testThatTypeLabelExists {
    XCTAssertNotNil(cell.typeLabel, @"");
}

- (void)testThatLevelLabelExists {
    XCTAssertNotNil(cell.levelLabel, @"");
}

#pragma mark - configureCell
- (void)testThatCellShouldBeSetCorrectly {
    NSNumber *level = @(21);
    [cell configureCellWithType:@"Person" andLevel:@(21)];
    XCTAssertEqualObjects(cell.typeLabel.text, @"Person", @"");
    
    NSString *levelString =
    [NSString stringWithFormat:NSLocalizedStringFromTable(@"cell.character.label.level", @"BrowseCharacters", @""), level];
    XCTAssertEqualObjects(cell.levelLabel.text, levelString, @"");
}

- (void)testThatCellShouldBeSetWithEmptyStringsWhenNilPassed {
    [cell configureCellWithType:nil andLevel:nil];
    XCTAssertEqualObjects(cell.levelLabel.text, @"", @"");
    XCTAssertEqualObjects(cell.typeLabel.text, @"", @"");
}

// co gdy zapodam nil albo inne cos?

@end
