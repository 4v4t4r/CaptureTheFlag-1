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
    CTFCharacter *character = [CTFCharacter createObject];
    XCTAssertNotNil(character, @"");
}

@end
