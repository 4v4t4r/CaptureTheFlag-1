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
    
    NSString *key = [CTFLocalCredentialsStore key];
    NSString *objectToStore = [NSString stringWithFormat:@"%@,%@", credentials.username, credentials.password];
    BOOL stored = [_keychain storeUsername:key andPassword:objectToStore forServiceName:key updateExisting:YES error:nil];
    return stored;
}

- (CTFLocalCredentials *)getCredentials {
    NSString *key = [CTFLocalCredentialsStore key];
    NSString *storedObject = [_keychain getPasswordForUsername:key andServiceName:key error:nil];
    NSArray *components = [storedObject componentsSeparatedByString:@","];
    
    CTFLocalCredentials *credentials = nil;
    if (components.count == 2)
        credentials = [[CTFLocalCredentials alloc] initWithUsername:components[0] password:components[1]];
    return credentials;
}

+ (NSString *)key {
    NSMutableString *key = [[NSMutableString alloc] initWithString:[[NSBundle mainBundle] bundleIdentifier]];
    return key;
}

@end
