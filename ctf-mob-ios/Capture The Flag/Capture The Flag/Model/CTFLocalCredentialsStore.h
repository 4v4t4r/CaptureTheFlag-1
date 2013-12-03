//
//  CTFLocalCredentialsStore.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@class CTFLocalCredentials;
@class STKeychain;

@interface CTFLocalCredentialsStore : NSObject

+ (instancetype)sharedInstance;
+ (void)setSharedInstance:(CTFLocalCredentialsStore *)instance;

- (instancetype)initWithKeychain:(STKeychain *)keychain;

- (BOOL)storeCredentials:(CTFLocalCredentials *)credentials;
- (CTFLocalCredentials *)getCredentials;

@end
