#import "_CTFCharacter.h"

typedef enum {
    CTFCharacterTypePrivate = 0,
    CTFCharacterTypeMedic,
    CTFCharacterTypeCommandos,
    CTFCharacterTypeSpy
} CTFCharacterType;

@interface CTFCharacter : _CTFCharacter {}
@property (readonly, nonatomic) NSString *typeString;
@end
