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

@interface CTFGame : _CTFGame {}

@end
