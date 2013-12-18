#import "_CTFUser.h"

@interface CTFUser : _CTFUser {}
#warning [tsu] write tests
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