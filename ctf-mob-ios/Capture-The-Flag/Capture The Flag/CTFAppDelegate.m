//
//  CTFAppDelegate.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 06.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAppDelegate.h"
#import "CTFLoginViewController.h"

#import "CTFAPIConnection.h"
#import "CTFAPIRKConfigurator.h"
#import "CTFAPILocalCredentialsStore.h"

#import "STKeychain.h"
#import "CoreDataService.h"

@implementation CTFAppDelegate

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
{
    /// Configure Core Data
    CoreDataService *sharedCoreData = [CoreDataService new];
    [CoreDataService setSharedInstance:sharedCoreData];
    
    /// Configure RestKit
    NSURL *url = [NSURL URLWithString:@"http://78.133.154.39:8888"];
    
    RKObjectManager *manager = [RKObjectManager managerWithBaseURL:url];
    [manager setRequestSerializationMIMEType:RKMIMETypeJSON];
    
    RKManagedObjectStore *managedObjectStore =
    [[RKManagedObjectStore alloc] initWithPersistentStoreCoordinator:sharedCoreData.persistentStoreCoordinator];
    manager.managedObjectStore = managedObjectStore;
    [manager.managedObjectStore createManagedObjectContexts];
    
    /// Configure RestKit Manager
    CTFAPIRKConfigurator *configurator = [[CTFAPIRKConfigurator alloc] initWithManager:manager];
    [configurator configure];
    
    /// Configure CTFAPIConnection
    CTFAPIConnection *connection = [[CTFAPIConnection alloc] initWithManager:manager];
    [CTFAPIConnection setSharedConnection:connection];

    /// Configure CredentialsStore
    CTFAPILocalCredentialsStore *credentialsStore = [[CTFAPILocalCredentialsStore alloc] initWithKeychain:[STKeychain sharedInstance]];
    [CTFAPILocalCredentialsStore setSharedInstance:credentialsStore];
    
    /// Load view
    UIStoryboard *storyboard = [UIStoryboard storyboardWithName:@"LoginAndRegister" bundle:nil];
    UINavigationController *nav = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([UINavigationController class])];
    
    if (!self.window)
        self.window = [[UIWindow alloc] initWithFrame:[UIScreen mainScreen].bounds];

    self.window.rootViewController = nav;

    [self.window makeKeyAndVisible];
    
    return YES;
}

- (void)applicationWillTerminate:(UIApplication *)application
{
#ifdef DEBUG
    extern void __gcov_flush(void);
    __gcov_flush();
#endif
    // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
}

#ifdef DEBUG
+ (void)initialize {
    [[NSUserDefaults standardUserDefaults] setValue:@"XCTestLog,GcovTestObserver"
                                             forKey:@"XCTestObserverClass"];
}
#endif

@end
