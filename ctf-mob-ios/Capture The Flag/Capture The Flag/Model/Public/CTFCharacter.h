#import "_CTFCharacter.h"

typedef enum {
    CTFCharacterTypeUndefined = -1,
    CTFCharacterTypePrivate = 0,
    CTFCharacterTypeMedic,
    CTFCharacterTypeCommandos,
    CTFCharacterTypeSpy
} CTFCharacterType;

@interface CTFCharacter : _CTFCharacter {}

@end
