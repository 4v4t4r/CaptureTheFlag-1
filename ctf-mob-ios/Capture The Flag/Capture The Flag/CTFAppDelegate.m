//
//  CTFAppDelegate.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 06.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAppDelegate.h"
#import "CoreDataService.h"
#import "CTFLoginViewController.h"

#import "CTFAPIConnection.h"
#import "CTFAPIRKDescriptors.h"

#import "STKeychain.h"
#import "CTFAPILocalCredentialsStore.h"

@implementation CTFAppDelegate

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
{
    /// Configure Core Data
    CoreDataService *sharedCoreData = [CoreDataService new];
    [CoreDataService setSharedInstance:sharedCoreData];
    
    /// Configure RestKit
#warning [tsu] This is only test configuration. When official server url will be available I'll update it.
    NSURL *url = [NSURL URLWithString:@"http://iwrapperapp.com/abanalytics/api/"];
    
    RKObjectManager *manager = [RKObjectManager managerWithBaseURL:url];
    [manager.HTTPClient setAuthorizationHeaderWithUsername:@"analytics" password:@"0xgYjxGBKv"];
    [manager setRequestSerializationMIMEType:RKMIMETypeJSON];
    
    RKManagedObjectStore *managedObjectStore =
    [[RKManagedObjectStore alloc] initWithPersistentStoreCoordinator:sharedCoreData.persistentStoreCoordinator];
    manager.managedObjectStore = managedObjectStore;
    [manager.managedObjectStore createManagedObjectContexts];
    
    /// Configure RKDescriptors
    CTFAPIRKDescriptors *descriptors = [[CTFAPIRKDescriptors alloc] initWithManager:manager];
    [CTFAPIRKDescriptors setSharedInstance:descriptors];
    [manager addResponseDescriptorsFromArray:@[[descriptors getUserResponseDescriptor]]];
    
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
							
- (void)applicationWillResignActive:(UIApplication *)application
{
    // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
    // Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
}

- (void)applicationDidEnterBackground:(UIApplication *)application
{
    // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later. 
    // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
}

- (void)applicationWillEnterForeground:(UIApplication *)application
{
    // Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
}

- (void)applicationDidBecomeActive:(UIApplication *)application
{
    // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
}

- (void)applicationWillTerminate:(UIApplication *)application
{
    // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
}

@end
