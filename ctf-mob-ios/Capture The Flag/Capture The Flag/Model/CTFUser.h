//
//  CTFUser.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 05.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CustomManagedObject.h"

@class CTFCharacter, CTFGame;

@interface CTFUser : CustomManagedObject

@property (nonatomic, retain) NSString * email;
@property (nonatomic, retain) NSString * nick;
@property (nonatomic, retain) NSString * username;
@property (nonatomic, retain) NSString * password;
@property (nonatomic, retain) id location;
@property (nonatomic, retain) CTFGame *game;
@property (nonatomic, retain) NSSet *characters;

+ (NSDictionary *)dictionaryForResponseMapping;

@end

@interface CTFUser (CoreDataGeneratedAccessors)

- (void)addCharactersObject:(CTFCharacter *)value;
- (void)removeCharactersObject:(CTFCharacter *)value;
- (void)addCharacters:(NSSet *)values;
- (void)removeCharacters:(NSSet *)values;

@end