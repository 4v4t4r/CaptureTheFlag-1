//
//  CTFItemTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "BaseModelTest.h"
#import "CTFItem.h"
@interface CTFItemTests : BaseModelTest

@end

@implementation CTFItemTests

- (void)testThatItemShouldBeNotNil {
    CTFItem *item = [CTFItem createObject];
    XCTAssertNotNil(item, @"");
}

@end
