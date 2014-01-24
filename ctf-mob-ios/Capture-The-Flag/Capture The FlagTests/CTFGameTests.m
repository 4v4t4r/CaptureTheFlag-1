//
//  CTFGame.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "BaseModelTest.h"
#import "CTFGame.h"

@interface CTFGameTests : BaseModelTest

@end

@implementation CTFGameTests

- (void)testThatGameShouldExists {
    CTFGame *game =
    [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFGame class])
                                  inManagedObjectContext:self.service.managedObjectContext];
    XCTAssertNotNil(game, @"");
}

@end
