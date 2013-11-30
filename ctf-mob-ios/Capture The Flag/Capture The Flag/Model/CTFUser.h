//
//  CTFUser.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CustomManagedObject.h"

@interface CTFUser : CustomManagedObject

@property (nonatomic) NSString *username;
@property (nonatomic) NSString *email;
@property (nonatomic) NSString *password;
@property (nonatomic) NSString *nick;

#warning ???: I'm not sure that location should be in CTFUser object. It should be in player? It's only necessary when user is in game. Maybe we should create some proxy object which will return us only these values which are necessary for gameplay?
//@property (nonatomic) CGPoint location;

#warning After first tests of properties above add this property too and test it.
//@property (nonatomic, readonly) NSArray *characters;

@property (nonatomic) NSNumber *logged;
@property (readonly) BOOL isLogged;

/** Return instance of CTFUser class from CoreData when exists, otherwise nil. */
+ (instancetype)loggedUser;

- (BOOL)loginUser;
- (void)logoutUser;

@end
