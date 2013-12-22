//
//  CTFBrowseCharactersViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 22.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "CTFBrowseCharactersViewController.h"

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

@end
