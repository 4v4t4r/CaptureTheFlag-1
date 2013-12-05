//
//  CTFUser.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 05.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CustomManagedObject.h"

@class CTFGame;

@interface CTFUser : CustomManagedObject

@property (nonatomic, retain) NSString * email;
@property (nonatomic, retain) NSString * nick;
@property (nonatomic, retain) NSString * username;
@property (nonatomic, retain) NSString * password;
@property (nonatomic, retain) id location;
@property (nonatomic, retain) CTFGame *game;
#warning [tsu] add characters to the model

+ (NSDictionary *)dictionaryForResponseMapping;

@end
