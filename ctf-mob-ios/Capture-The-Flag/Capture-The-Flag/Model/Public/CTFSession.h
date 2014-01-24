#import "_CTFSession.h"

@interface CTFSession : _CTFSession {}

/** 
 (fixedPassword) This property is temporary. It's fix for PATCH requests to the /api/users/id/
 Remove this property when fix on server side will be available.
 Remove it also from the model.
*/
+ (instancetype)sharedInstance;
+ (void)setSharedInstance:(CTFSession *)game;

- (instancetype)initWithToken:(NSString *)token;
- (void)setCurrentUser:(CTFUser *)currentUser;

@end
