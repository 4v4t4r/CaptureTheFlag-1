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
@property (nonatomic, retain) NSOrderedSet *characters;

@end


@interface CTFUser (CoreDataGeneratedAccessors)

- (void)insertObject:(CTFCharacter *)value inCharactersAtIndex:(NSUInteger)idx;
- (void)removeObjectFromCharactersAtIndex:(NSUInteger)idx;
- (void)insertCharacters:(NSArray *)value atIndexes:(NSIndexSet *)indexes;
- (void)removeCharactersAtIndexes:(NSIndexSet *)indexes;
- (void)replaceObjectInCharactersAtIndex:(NSUInteger)idx withObject:(CTFCharacter *)value;
- (void)replaceCharactersAtIndexes:(NSIndexSet *)indexes withCharacters:(NSArray *)values;
- (void)addCharactersObject:(CTFCharacter *)value;
- (void)removeCharactersObject:(CTFCharacter *)value;
- (void)addCharacters:(NSOrderedSet *)values;
- (void)removeCharacters:(NSOrderedSet *)values;

@end