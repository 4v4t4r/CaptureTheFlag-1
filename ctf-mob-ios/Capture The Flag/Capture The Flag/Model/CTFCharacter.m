//
//  CTFCharacter.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 05.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFCharacter.h"
#import "CTFUser.h"


@implementation CTFCharacter

@dynamic type;
@dynamic totalTime;
@dynamic totalScore;
@dynamic health;
@dynamic level;
@dynamic active;
@dynamic user;

+ (NSDictionary *)dictionaryResponseMapping {
    return @{@"type": @"type",
             @"total_time": @"totalTime",
             @"total_score": @"totalScore",
             @"health": @"health",
             @"level": @"level",
             @"is_active": @"active"};
}

@end
