//
//  CoreDataService.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <CoreData/CoreData.h>

@interface CoreDataService : NSObject

@property (readonly, nonatomic) NSManagedObjectContext *managedObjectContext;
@property (readonly, nonatomic) NSPersistentStoreCoordinator *persistentStoreCoordinator;

+ (CoreDataService *)sharedInstance;
+ (void)setSharedInstance:(CoreDataService *)instance;

- (instancetype)init;
- (instancetype)initForUnitTesting;

- (void)saveContext;

@end
