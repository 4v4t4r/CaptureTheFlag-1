//
//  CTFLocalCredentialsStore.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@class CTFAPILocalCredentials;
@class STKeychain;

@interface CTFAPILocalCredentialsStore : NSObject

/**
 @define Method used to get shared object
 @abstract Object is midpoint between user and Keychain. It has ability to store credentials of single user.
 @discussion If sharedInstance has been set it return object, otherwise nil.
 @param
 */
+ (instancetype)sharedInstance;

/**
 @define Method used to set shared CTFLocalCredentialsStore object
 @abstract
 @discussion
 @param instance Instance of CTFLocalCredentialsStore class.
 */
+ (void)setSharedInstance:(CTFAPILocalCredentialsStore *)instance;

/**
 @define Method used to initialization object.
 @abstract
 @discussion
 @param keychain Object of class STKeychain.
 */
- (instancetype)initWithKeychain:(STKeychain *)keychain;

/**
 @define Method used to store credentials in Keychain
 @abstract If not nil return YES, otherwise NO
 @discussion
 @param credentials Credentials which will be stored in Keychain
 */
- (BOOL)storeCredentials:(CTFAPILocalCredentials *)credentials;

/**
 @define Method used to get credentials from Keychain
 @abstract
 @discussion
 @param
 */
- (CTFAPILocalCredentials *)getCredentials;

@end
