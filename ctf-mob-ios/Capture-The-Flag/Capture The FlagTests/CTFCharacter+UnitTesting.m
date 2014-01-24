//
//  CTFCharacter+UnitTesting.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 24.01.2014.
//  Copyright (c) 2014 Tomasz Szulc. All rights reserved.
//

#import "CTFCharacter+UnitTesting.h"

@implementation CTFCharacter (UnitTesting)

- (instancetype)initWithEntity:(NSEntityDescription *)entity insertIntoManagedObjectContext:(NSManagedObjectContext *)context type:(NSNumber *)type {
    self = [super initWithEntity:entity insertIntoManagedObjectContext:context];
    if (self) {
        self.type = type;
    }
    return self;
}

@end
