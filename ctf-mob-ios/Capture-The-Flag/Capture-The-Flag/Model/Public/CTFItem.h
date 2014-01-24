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

@interface CTFItem : _CTFItem {}

@end
