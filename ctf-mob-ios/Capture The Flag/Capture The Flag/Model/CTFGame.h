//
//  CTFGame.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 03.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CustomManagedObject.h"

@class CTFUser;

@interface CTFGame : CustomManagedObject

@property (readonly) NSString *token;
@property (readonly) CTFUser *currentUser;

+ (instancetype)sharedInstance;
+ (void)setSharedInstance:(CTFGame *)game;

- (instancetype)initWithToken:(NSString *)token;

- (void)setCurrentUser:(CTFUser *)currentUser;

@end
