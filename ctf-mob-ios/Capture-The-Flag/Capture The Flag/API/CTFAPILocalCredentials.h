//
//  CTFLocalCredentials.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface CTFAPILocalCredentials : NSObject

@property (readonly) NSString *username;
@property (readonly) NSString *password;

/**
 @define Method used to initialize CTFLocalCredentials object.
 @abstract Return initialized object or nil if isn't correctly initialized.
 @discussion Class used to hold username and password and user with CTFLocalCredentialsStore to store this values in Keychain.
 @param username Username belongs to account
 @param password Password to account
 */
- (instancetype)initWithUsername:(NSString *)username password:(NSString *)password;

@end
