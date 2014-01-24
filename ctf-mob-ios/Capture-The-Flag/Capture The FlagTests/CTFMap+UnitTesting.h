//
//  CTFMap+UnitTesting.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 24.01.2014.
//  Copyright (c) 2014 Tomasz Szulc. All rights reserved.
//

#import "CTFMap.h"

@interface CTFMap (UnitTesting)

- (id)initWithEntity:(NSEntityDescription *)entity insertIntoManagedObjectContext:(NSManagedObjectContext *)context location:(NSArray *)location;

@end
