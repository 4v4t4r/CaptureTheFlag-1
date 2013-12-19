//
//  CTFCharacterTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "BaseModelTest.h"
#import "CTFCharacter.h"

@interface CTFCharacterTests : BaseModelTest

@end

@implementation CTFCharacterTests

- (void)testThatCharacterExists {
    CTFCharacter *character =
    [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFCharacter class])
                                  inManagedObjectContext:self.service.managedObjectContext];
    XCTAssertNotNil(character, @"");
}

@end
