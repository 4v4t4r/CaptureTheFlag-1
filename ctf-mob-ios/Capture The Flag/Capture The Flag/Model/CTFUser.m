//
//  CTFUser.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFUser.h"

@implementation CTFUser

@dynamic username;
@dynamic email;
@dynamic nick;

+ (instancetype)currentUser
{
    NSFetchRequest *request = [self fetchRequestWithPredicate:nil];
    request.fetchLimit = 1;
    NSArray *results = [[self moc] executeFetchRequest:request error:nil];
    
    if (results.count > 0)
        return results[0];
    else
        return nil;
}

@end
