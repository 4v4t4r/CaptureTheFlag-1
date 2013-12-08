//
//  CTFItem.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 08.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CustomManagedObject.h"

typedef enum {
    CTFItemTypeRedFlag,
    CTFItemTypeBlueFlag,
    
    CTFItemTypeRedBase,
    CTFItemTypeBlueBase,
    
    CTFItemTypeMedicBox,
    
    CTFItemTypePistol,
    CTFItemTypeAmmo
} CTFItemType;

@interface CTFItem : CustomManagedObject

@property (nonatomic, retain) NSString * name;
@property (nonatomic, retain) NSString * desc;
@property (nonatomic, retain) NSNumber * type;
@property (nonatomic, retain) id location;
@property (nonatomic, retain) NSNumber * value;

@end
