//
//  CTFUser.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFUser.h"

@implementation CTFUser

@dynamic login;
@dynamic token;
@dynamic logged;

+ (instancetype)loggedUser
{
    NSPredicate *predicate = [NSPredicate predicateWithFormat:@"logged == %@", @(YES)];
    
    NSFetchRequest *request = [self fetchRequestWithPredicate:predicate];
    request.fetchLimit = 1;
    NSArray *results = [[self moc] executeFetchRequest:request error:nil];
    
    if (results.count > 0)
        return results[0];
    else
        return nil;
}

- (BOOL)isLogged
{
    return [self.logged boolValue];
}

- (BOOL)loginUser
{
    CTFUser *loggedUser = [CTFUser loggedUser];
    
    BOOL hasBeenLogged = NO;
    if (loggedUser)
    {
        if ([self isEqual:loggedUser])
            hasBeenLogged = YES;
    }
    else
    {
        self.logged = @(YES);
        hasBeenLogged = YES;
    }
    
    return hasBeenLogged;
}

- (void)logoutUser
{
    self.logged = @(NO);
    [self.managedObjectContext deleteObject:self];
}

@end
