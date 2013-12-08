#import "_CTFSession.h"

@interface CTFSession : _CTFSession {}

+ (instancetype)sharedInstance;
+ (void)setSharedInstance:(CTFSession *)game;

- (instancetype)initWithToken:(NSString *)token;
- (void)setCurrentUser:(CTFUser *)currentUser;

@end
