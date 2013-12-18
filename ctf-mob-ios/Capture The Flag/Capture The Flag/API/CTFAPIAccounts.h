//
//  CTFAPIAccounts.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 30.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@class CTFAPIConnection;
@class CTFUser;

typedef void (^TokenBlock)(NSString *token);
typedef void (^SignUpBlock)(BOOL success);
typedef void (^ProfileBlock)(CTFUser *user);

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
 @abstract Return token in block if successfuly logged in to server. Otherwise token is nil.
 @discussion Asynchronous method used to log in inter server with passed credentials.
 @param username The username to log in.
 @param password The password to log in.
 @param block SignInBlock called when server response. It return token value.
 */
- (void)signInWithUsername:(NSString *)username andPassword:(NSString *)password withBlock:(TokenBlock)block;

/**
 @define Method used to register new account.
 @abstract Return SignUpBlock with success if successfuly register. Otherwise return NO.
 @discussion Asynchronous method use to register acount on the server with passed credentials.
 @param username The username to register.
 @param email email address of user account.
 @param password password to the account.
 @param block SignUpBlock called when server response. It return success value.
 */
- (void)signUpWithUsername:(NSString *)username email:(NSString *)email password:(NSString *)password block:(SignUpBlock)block;

/**
 @define Method for getting account information associated with token
 @abstract Return block with user if user for token exists, otherwise nil.
 @discussion 
 @param token Token used to communicate with the server
 @param block ProfileBlock called after server response
 */
- (void)accountInfoForToken:(NSString *)token block:(ProfileBlock)block;

@end
