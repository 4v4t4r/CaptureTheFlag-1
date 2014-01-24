//
//  CTFMap+UnitTesting.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 24.01.2014.
//  Copyright (c) 2014 Tomasz Szulc. All rights reserved.
//

#import "CTFMap+UnitTesting.h"

@implementation CTFMap (UnitTesting)

- (id)initWithEntity:(NSEntityDescription *)entity insertIntoManagedObjectContext:(NSManagedObjectContext *)context location:(NSArray *)location {
    self = [super initWithEntity:entity insertIntoManagedObjectContext:context];
    if (self) {
        self.location = location;
    }
    return self;
}

@end
