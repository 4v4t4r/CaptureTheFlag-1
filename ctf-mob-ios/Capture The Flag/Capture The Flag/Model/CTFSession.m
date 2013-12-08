//
//  CTFSession.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 03.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFSession.h"
#import "CTFUser.h"

@implementation CTFSession {
    NSString *_token;
    CTFUser *_currentUser;
}

@dynamic token;
@dynamic currentUser;

static CTFSession *sharedInstance = nil;
+ (instancetype)sharedInstance {
    @synchronized (self) {
        return sharedInstance;
    }
}

+ (void)setSharedInstance:(CTFSession *)game {
    sharedInstance = game;
}

- (instancetype)initWithToken:(NSString *)token {
    if (!token)
        return nil;
    
    self = [CTFSession createObject];
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
