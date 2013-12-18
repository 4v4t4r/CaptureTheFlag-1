#import "_CTFGame.h"

typedef enum {
    GameStatusInProgress,
    GameStatusCreated,
    GameStatusOnHold,
    GameStatusCancelled
} GameStatus;

typedef enum {
    GameTypeUndefined = -1,
    GameTypeFrags,
    GameTypeTime
} GameType;
#warning [tsu] write tests
@interface CTFGame : _CTFGame {}

@end
