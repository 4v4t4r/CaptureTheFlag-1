//
//  CoreDataService.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 17.08.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <CoreData/CoreData.h>

@interface CoreDataService : NSObject

@property (readonly, strong, nonatomic) NSManagedObjectContext *managedObjectContext;
@property (readonly, strong, nonatomic) NSManagedObjectModel *managedObjectModel;
@property (readonly, strong, nonatomic) NSPersistentStoreCoordinator *persistentStoreCoordinator;

+ (CoreDataService *)sharedService;

- (void)saveContext;
- (NSURL *)applicationDocumentsDirectory;

@end
