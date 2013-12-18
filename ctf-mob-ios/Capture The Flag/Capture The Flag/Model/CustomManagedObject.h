//
//  CustomManagedObject.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <CoreData/CoreData.h>

@interface CustomManagedObject : NSManagedObject
#warning [tsu] write tests
+ (instancetype)createObject;

+ (NSFetchRequest *)fetchRequestWithPredicate:(NSPredicate *)predicate;

+ (NSManagedObjectContext *)moc;

@end
