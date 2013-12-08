#import "_CTFItem.h"

typedef enum {
    CTFItemTypeUndefined = -1,
    CTFItemTypeRedFlag,
    CTFItemTypeBlueFlag,
    
    CTFItemTypeRedBase,
    CTFItemTypeBlueBase,
    
    CTFItemTypeMedicBox,
    
    CTFItemTypePistol,
    CTFItemTypeAmmo
} CTFItemType;

@interface CTFItem : _CTFItem {}

@end
