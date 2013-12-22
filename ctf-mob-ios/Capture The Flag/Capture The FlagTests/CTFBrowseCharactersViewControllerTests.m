//
//  CTFBrowseCharactersViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 22.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFBrowseCharactersViewController.h"
#import "CTFNoCharactersCell.h"
#import "CTFCharacter.h"
#import "CTFCharacterCell.h"

@interface CTFCharacterMock : NSObject

- (NSNumber *)level;
- (NSString *)typeString;

@end

@implementation CTFCharacterMock

- (NSNumber *)level {
    return @(21);
}

- (NSString *)typeString {
    return @"Person";
}

@end

@interface CTFBrowseCharactersViewControllerTests : XCTestCase

@end

@implementation CTFBrowseCharactersViewControllerTests {
    UIStoryboard *storyboard;
    CTFBrowseCharactersViewController *_vc;
}

- (void)setUp
{
    [super setUp];
    storyboard = [UIStoryboard storyboardWithName:@"Main" bundle:nil];
    _vc = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([CTFBrowseCharactersViewController class])];
    [_vc view];
}

- (void)tearDown
{
    storyboard = nil;
    _vc = nil;
    [super tearDown];
}

- (void)testThatViewControllerExists {
    XCTAssertNotNil(_vc, @"");
}


#pragma mark - Outlets
- (void)testThatTableViewExists {
    XCTAssertNotNil(_vc.tableView, @"");
}

#pragma mark - Delegate and Datasource
- (void)testThatTableViewHasDelegate {
    XCTAssertEqualObjects(_vc.tableView.delegate, _vc, @"");
}

- (void)testThatTableViewHasDataSource {
    XCTAssertEqualObjects(_vc.tableView.dataSource, _vc, @"");
}

#pragma mark - Localization
- (void)testThatNavigationItemTitleIsLocalized {
    XCTAssertEqualObjects(_vc.navigationItem.title, NSLocalizedStringFromTable(@"navigationItem.title", @"BrowseCharacters", @""), @"");
}


#pragma mark - life cycle
- (void)testThatThereShouldBeNoCharactersCell {
    [_vc setCharacters:@[]];
    id cell = [_vc.tableView cellForRowAtIndexPath:[_vc.tableView indexPathsForVisibleRows][0]];
    XCTAssertEqual([cell class], [CTFNoCharactersCell class], @"");
}

- (void)testThatThereShouldBeFewCharacterCells {
    CTFCharacterMock *characterMock = [CTFCharacterMock new];
    [_vc setCharacters:@[characterMock]];
    
    id cell = [_vc.tableView cellForRowAtIndexPath:[_vc.tableView indexPathsForVisibleRows][0]];
    XCTAssertEqual([cell class], [CTFCharacterCell class], @"");
}

@end
