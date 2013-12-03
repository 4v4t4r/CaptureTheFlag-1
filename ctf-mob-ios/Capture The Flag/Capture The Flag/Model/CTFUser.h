//
//  CTFUser.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 03.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CustomManagedObject.h"

@class CTFGame;

@interface CTFUser : CustomManagedObject

@property (nonatomic, retain) NSString * email;
@property (nonatomic, retain) NSString * nick;
@property (nonatomic, retain) NSString * username;
@property (nonatomic, retain) CTFGame *game;

@end
