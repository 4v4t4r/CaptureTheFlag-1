//
//  CTFAPIConnection.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 01.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface CTFAPIConnection : NSObject

@property (readonly, nonatomic) RKObjectManager *manager;

/**
 @define Method used to set shared connection object
 @abstract
 @discussion
 @param connection object which holds information about connection to the server.
 */
+ (void)setSharedConnection:(CTFAPIConnection *)connection;

/**
 @define Returns CTFAPIConnection singleton.
 @abstract
 @discussion Returns singleton if exists, otherwise nil. You should first set sharedConnection object via setSharedConnection: method
 @param
 */
+ (instancetype)sharedConnection;

/**
 @define Method used to initailize connection object.
 @abstract 
 @discussion 
 @param manager RKObjectManager object.
 */
- (instancetype)initWithManager:(RKObjectManager *)manager;

@end
