#import "_CTFItem.h"

typedef enum {
    CTFItemTypeRedFlag,
    CTFItemTypeBlueFlag,
    
    CTFItemTypeRedBase,
    CTFItemTypeBlueBase,
    
    CTFItemTypeMedicBox,
    
    CTFItemTypePistol,
    CTFItemTypeAmmo
} CTFItemType;
#warning [tsu] write tests
@interface CTFItem : _CTFItem {}

@end
