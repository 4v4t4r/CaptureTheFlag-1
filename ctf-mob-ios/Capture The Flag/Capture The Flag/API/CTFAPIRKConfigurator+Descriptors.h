//
//  CTFAPIRKConfigurator+Descriptors.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIRKConfigurator.h"

@interface CTFAPIRKConfigurator (Descriptors)

/**
 api/users
 */
#warning [tsu] add tests for request
- (RKRequestDescriptor *)usersPOSTRequestDescriptor;
#warning [tsu] add tests for response
- (RKResponseDescriptor *)usersPOSTResponseDescriptor;


/**
 api/profile
 */
#warning [tsu] add tests for response
- (RKResponseDescriptor *)profileResponseDescriptor;

@end
