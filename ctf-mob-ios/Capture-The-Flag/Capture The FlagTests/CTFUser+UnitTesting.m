//
//  CTFUser+UnitTesting.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 27.01.2014.
//  Copyright (c) 2014 Tomasz Szulc. All rights reserved.
//

#import "CTFUser+UnitTesting.h"

@implementation CTFUser (UnitTesting)
- (instancetype)initWithEntity:(NSEntityDescription *)entity insertIntoManagedObjectContext:(NSManagedObjectContext *)context userId:(NSNumber *)userId {
    self = [super initWithEntity:entity insertIntoManagedObjectContext:context];
    if (self) {
        self.userId = userId;
    }
    return self;
}
@end
