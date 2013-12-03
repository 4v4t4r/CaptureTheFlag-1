//
//  CTFGame.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 03.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFGame.h"
#import "CTFUser.h"

@implementation CTFGame {
    NSString *_token;
    CTFUser *_currentUser;
}

@dynamic token;
@dynamic currentUser;

static CTFGame *sharedInstance = nil;
+ (instancetype)sharedInstance {
    @synchronized (self) {
        return sharedInstance;
    }
}

+ (void)setSharedInstance:(CTFGame *)game {
    sharedInstance = game;
}

- (instancetype)initWithToken:(NSString *)token {
    if (!token)
        return nil;
    
    self = [CTFGame createObject];
    if (self) {
        _token = token;
    }
    return self;
}

#pragma mark - Accessors
- (void)setCurrentUser:(CTFUser *)currentUser {
    _currentUser = currentUser;
}

- (NSString *)token {
    return _token;
}

- (CTFUser *)currentUser {
    return _currentUser;
}

@end
