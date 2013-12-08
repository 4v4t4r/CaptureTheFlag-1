//
//  CTFLocalCredentialsStore.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFLocalCredentialsStore.h"
#import "CTFLocalCredentials.h"
#import "STKeychain.h"

@implementation CTFLocalCredentialsStore {
    STKeychain *_keychain;
}

+ (instancetype)sharedInstance {
    @synchronized (self) {
        return sharedInstance;
    }
}

static CTFLocalCredentialsStore *sharedInstance = nil;
+ (void)setSharedInstance:(CTFLocalCredentialsStore *)instance {
    sharedInstance = instance;
}

- (instancetype)initWithKeychain:(STKeychain *)keychain {
    if (!keychain)
        return nil;
    
    self = [super init];
    if (self) {
        _keychain = keychain;
    }
    return self;
}

- (BOOL)storeCredentials:(CTFLocalCredentials *)credentials {
    if (!credentials)
        return NO;
    
    NSString *key = [CTFLocalCredentialsStore _key];
    NSString *objectToStore = [NSString stringWithFormat:@"%@,%@", credentials.username, credentials.password];
    NSError *error = nil;
    BOOL stored = [_keychain storeUsername:key andPassword:objectToStore forServiceName:key updateExisting:YES error:&error];
    if (error) {
        NSLog(@"%s, %@", __PRETTY_FUNCTION__, [error localizedDescription]);
    }
    return stored;
}

- (CTFLocalCredentials *)getCredentials {
    NSString *key = [CTFLocalCredentialsStore _key];
    NSError *error = nil;
    NSString *storedObject = [_keychain getPasswordForUsername:key andServiceName:key error:&error];
   
    CTFLocalCredentials *credentials = nil;
    if (!error) {
        NSArray *components = [storedObject componentsSeparatedByString:@","];
        
        if (components.count == 2)
            credentials = [[CTFLocalCredentials alloc] initWithUsername:components[0] password:components[1]];
    } else {
        NSLog(@"%s, %@", __PRETTY_FUNCTION__, [error localizedDescription]);
    }
    
    return credentials;
}

+ (NSString *)_key {
    NSMutableString *key = [[NSMutableString alloc] initWithString:[[NSBundle mainBundle] bundleIdentifier]];
    return key;
}

@end
