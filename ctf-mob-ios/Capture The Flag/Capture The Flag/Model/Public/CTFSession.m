#import "CTFSession.h"


@interface CTFSession ()

@end


@implementation CTFSession {
    NSString *_token;
    CTFUser *_currentUser;
}

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
