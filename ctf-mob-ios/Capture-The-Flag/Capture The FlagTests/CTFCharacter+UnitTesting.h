//
//  CTFCharacter+UnitTesting.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 24.01.2014.
//  Copyright (c) 2014 Tomasz Szulc. All rights reserved.
//

#import "CTFCharacter.h"

@interface CTFCharacter (UnitTesting)

- (instancetype)initWithEntity:(NSEntityDescription *)entity insertIntoManagedObjectContext:(NSManagedObjectContext *)context type:(NSNumber *)type;

@end
