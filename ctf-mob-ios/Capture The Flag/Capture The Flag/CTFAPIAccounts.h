//
//  CTFAPIAccounts.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@class CTFAPIConnection;

typedef void (^SignInBlock)(BOOL success, NSString *token);

@interface CTFAPIAccounts : NSObject

/**
 @define Method used to initialize object with connection.
 @abstract Return initialized object.
 @discussion Class used to call user-releated (sign in and sign up) requests to the server.
 @param connection CTFAPIConnection object which holds information about connection to the server.
 */
- (instancetype)initWithConnection:(CTFAPIConnection *)connection;

/**
 @define Method used to login to the server.
 @abstract Return token in block if successfuly logged in to server. Otherwise success is NO and token is nil.
 @discussion Asynchronous method used to log in inter server with passed credentials.
 @param username The username to log in.
 @param password The password to log in.
 @param block SignInBlock called when server response. It return success and token value.
 */
- (void)signInWithUsername:(NSString *)username andPassword:(NSString *)password withBlock:(SignInBlock)block;

@end
