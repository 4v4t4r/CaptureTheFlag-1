//
//  CoreDataService.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CoreDataService.h"

@implementation CoreDataService

@synthesize managedObjectContext = _managedObjectContext;
@synthesize managedObjectModel = _managedObjectModel;
@synthesize persistentStoreCoordinator = _persistentStoreCoordinator;


#pragma mark - Setting singleton
static CoreDataService *sharedInstance = nil;
+ (void)setSharedInstance:(CoreDataService *)instance
{
    sharedInstance = instance;
}

+ (CoreDataService *)sharedInstance
{
    return sharedInstance;
}

#pragma mark - Initialization
- (id)init
{
    self = [super init];
    if (self)
    {
        NSURL *storeURL = [[self applicationDocumentsDirectory] URLByAppendingPathComponent:@"model.sqlite"];
        [self setUpStoreWithType:NSSQLiteStoreType andStoreURL:storeURL];
    }
    
    return self;
}

- (instancetype)initForUnitTesting
{
    self = [super init];
    if (self)
    {
        [self setUpStoreWithType:NSInMemoryStoreType andStoreURL:nil];
    }
    
    return self;
}

- (NSURL *)modelURL
{
    return [[NSBundle mainBundle] URLForResource:@"Model" withExtension:@"momd"];
}

/** Setup store. You didn't have to set storeURL if you want to use NSInMemoryStoreType */
- (void)setUpStoreWithType:(NSString*)type andStoreURL:(NSURL *)storeURL
{
    _managedObjectModel = [[NSManagedObjectModel alloc] initWithContentsOfURL:[self modelURL]];
    _persistentStoreCoordinator = [[NSPersistentStoreCoordinator alloc]
                                   initWithManagedObjectModel:_managedObjectModel];
    
    NSError *error = nil;
    NSPersistentStore *store =
    [_persistentStoreCoordinator addPersistentStoreWithType:type
                                              configuration:nil
                                                        URL:storeURL
                                                    options:nil
                                                      error:&error];
    if (!store)
    {
        NSLog(@"Unresolved error %@, %@", error, [error userInfo]);
        abort();
    }
    
    if (storeURL)
        _managedObjectContext = [[NSManagedObjectContext alloc] initWithConcurrencyType:NSMainQueueConcurrencyType];
    else
        _managedObjectContext = [NSManagedObjectContext new];
    
    [_managedObjectContext setPersistentStoreCoordinator:_persistentStoreCoordinator];
}


#pragma mark - Accessors
- (NSManagedObjectContext *)managedObjectContext
{
    return _managedObjectContext;
}

- (NSPersistentStoreCoordinator *)persistentStoreCoordinator
{
    return _persistentStoreCoordinator;
}


#pragma mark - Application's Documents directory
- (NSURL *)applicationDocumentsDirectory
{
    return [[[NSFileManager defaultManager] URLsForDirectory:NSDocumentDirectory inDomains:NSUserDomainMask] lastObject];
}


#pragma mark - Save support
- (void)saveContext
{
    NSError *error = nil;
    NSManagedObjectContext *managedObjectContext = _managedObjectContext;
    if (managedObjectContext != nil)
    {
        if ([managedObjectContext hasChanges] && ![managedObjectContext save:&error])
        {
            NSLog(@"Unresolved error %@, %@", error, [error userInfo]);
            abort();
        }
    }
}


@end
