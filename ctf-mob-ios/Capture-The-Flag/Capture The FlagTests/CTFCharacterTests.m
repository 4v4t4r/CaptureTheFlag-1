//
//  CTFCharacterTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "BaseModelTest.h"
#import "CTFCharacter.h"
#import "CTFCharacter+UnitTesting.h"

#import <OCMock/OCMock.h>

@interface CTFCharacterTests : BaseModelTest

@end

@implementation CTFCharacterTests

- (void)testThatCharacterExists {
    CTFCharacter *character = [self character];
    XCTAssertNotNil(character, @"");
}

- (void)testThatPrivateTypeShouldBeReturned {
    CTFCharacter *character = [self fakeCharacterWithType:@(CTFCharacterTypePrivate)];
    XCTAssertEqualObjects(character.typeString, NSLocalizedStringFromTable(@"type.private", @"CTFCharacter", @""), @"");
}

- (void)testThatMedicTypeShouldBeReturned {
    CTFCharacter *character = [self fakeCharacterWithType:@(CTFCharacterTypeMedic)];
    XCTAssertEqualObjects(character.typeString, NSLocalizedStringFromTable(@"type.medic", @"CTFCharacter", @""), @"");
}

- (void)testThatCommandosTypeShouldBeReturned {
    CTFCharacter *character = [self fakeCharacterWithType:@(CTFCharacterTypeCommandos)];
    XCTAssertEqualObjects(character.typeString, NSLocalizedStringFromTable(@"type.commandos", @"CTFCharacter", @""), @"");
}

- (void)testThatSpyTypeShouldBeReturned {
    CTFCharacter *character = [self fakeCharacterWithType:@(CTFCharacterTypeSpy)];
    XCTAssertEqualObjects(character.typeString, NSLocalizedStringFromTable(@"type.spy", @"CTFCharacter", @""), @"");
}

- (CTFCharacter *)character {
   return [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFCharacter class])
                                  inManagedObjectContext:self.service.managedObjectContext];
}

- (CTFCharacter *)fakeCharacterWithType:(NSNumber *)type {
    NSEntityDescription *entity = [NSEntityDescription entityForName:NSStringFromClass([CTFCharacter class]) inManagedObjectContext:self.service.managedObjectContext];

    return [[CTFCharacter alloc] initWithEntity:entity insertIntoManagedObjectContext:self.service.managedObjectContext type:type];
}

@end
