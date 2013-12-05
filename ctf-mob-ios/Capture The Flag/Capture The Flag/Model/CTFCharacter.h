//
//  CTFCharacter.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 05.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CustomManagedObject.h"

typedef enum {
    CTFCharacterTypePrivate = 0,
    CTFCharacterTypeMedic,
    CTFCharacterTypeCommandos,
    CTFCharacterTypeSpy
} CTFCharacterType;

@class CTFUser;

@interface CTFCharacter : CustomManagedObject

@property (nonatomic, retain) NSNumber * type;
@property (nonatomic, retain) NSNumber * totalTime;
@property (nonatomic, retain) NSNumber * totalScore;
@property (nonatomic, retain) NSNumber * health;
@property (nonatomic, retain) NSNumber * level;
@property (nonatomic, retain) NSNumber * active;
@property (nonatomic, retain) CTFUser *user;

+ (NSDictionary *)dictionaryResponseMapping;

@end
