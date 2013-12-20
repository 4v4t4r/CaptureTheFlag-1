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
 api/registration/
 */
- (RKRequestDescriptor *)registrationPOSTRequestDescriptor;
- (RKResponseDescriptor *)registrationPOSTResponseDescriptor;


/**
 api/profile
 */
- (RKResponseDescriptor *)profileResponseDescriptor;


/**
 api/users
 */
- (RKRequestDescriptor *)usersPATCHRequestDescriptor;

@end
