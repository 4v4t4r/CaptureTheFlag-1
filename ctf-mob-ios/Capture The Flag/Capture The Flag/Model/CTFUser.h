//
//  CTFUser.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CustomManagedObject.h"

@interface CTFUser : CustomManagedObject

@property (nonatomic) NSString *login;
@property (nonatomic) NSString *token;

/** Return instance of CTFUser class from CoreData when exists, otherwise nil. */
+ (instancetype)instance;

/** Logout user */
- (void)logout;

@end
