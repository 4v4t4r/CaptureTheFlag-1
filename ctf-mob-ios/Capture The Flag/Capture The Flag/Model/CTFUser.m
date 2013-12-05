//
//  CTFUser.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 05.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFUser.h"
#import "CTFGame.h"


@implementation CTFUser

@dynamic email;
@dynamic nick;
@dynamic username;
@dynamic password;
@dynamic location;
@dynamic game;
@dynamic characters;

+ (NSDictionary *)dictionaryForResponseMapping {
    return @{@"username" : @"username",
             @"email" : @"email",
             @"password" : @"password",
             @"nick" : @"nick",
             @"location" : @"location"/*,
             @"characters" : @"characters"*/};
}

@end
