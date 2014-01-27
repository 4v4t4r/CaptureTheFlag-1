//
//  CTFUser+UnitTesting.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 27.01.2014.
//  Copyright (c) 2014 Tomasz Szulc. All rights reserved.
//

#import "CTFUser.h"

@interface CTFUser (UnitTesting)
- (instancetype)initWithEntity:(NSEntityDescription *)entity insertIntoManagedObjectContext:(NSManagedObjectContext *)context userId:(NSNumber *)userId;

@end
