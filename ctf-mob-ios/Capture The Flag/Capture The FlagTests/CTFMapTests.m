//
//  CTFMapTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "BaseModelTest.h"
#import "CTFMap.h"

@interface CTFMapTests : BaseModelTest

@end

@implementation CTFMapTests

- (void)testThatMapShouldExists {
    CTFMap *map = [CTFMap createObject];
    XCTAssertNotNil(map, @"");
}

@end
