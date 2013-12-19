// DO NOT EDIT. This file is machine-generated and constantly overwritten.
// Make changes to CTFGame.h instead.

#import <CoreData/CoreData.h>


extern const struct CTFGameAttributes {
	__unsafe_unretained NSString *createdDate;
	__unsafe_unretained NSString *desc;
	__unsafe_unretained NSString *gameId;
	__unsafe_unretained NSString *maxPlayers;
	__unsafe_unretained NSString *modifiedDate;
	__unsafe_unretained NSString *name;
	__unsafe_unretained NSString *startTime;
	__unsafe_unretained NSString *status;
} CTFGameAttributes;

extern const struct CTFGameRelationships {
	__unsafe_unretained NSString *items;
	__unsafe_unretained NSString *map;
	__unsafe_unretained NSString *players;
} CTFGameRelationships;

extern const struct CTFGameFetchedProperties {
} CTFGameFetchedProperties;

@class CTFItem;
@class CTFMap;
@class CTFCharacter;










@interface CTFGameID : NSManagedObjectID {}
@end

@interface _CTFGame : NSManagedObject {}
+ (id)insertInManagedObjectContext:(NSManagedObjectContext*)moc_;
+ (NSString*)entityName;
+ (NSEntityDescription*)entityInManagedObjectContext:(NSManagedObjectContext*)moc_;
- (CTFGameID*)objectID;





@property (nonatomic, strong) NSDate* createdDate;



//- (BOOL)validateCreatedDate:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSString* desc;



//- (BOOL)validateDesc:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSString* gameId;



//- (BOOL)validateGameId:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSNumber* maxPlayers;



@property int16_t maxPlayersValue;
- (int16_t)maxPlayersValue;
- (void)setMaxPlayersValue:(int16_t)value_;

//- (BOOL)validateMaxPlayers:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSDate* modifiedDate;



//- (BOOL)validateModifiedDate:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSString* name;



//- (BOOL)validateName:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSDate* startTime;



//- (BOOL)validateStartTime:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSNumber* status;



@property int16_t statusValue;
- (int16_t)statusValue;
- (void)setStatusValue:(int16_t)value_;

//- (BOOL)validateStatus:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSSet *items;

- (NSMutableSet*)itemsSet;




@property (nonatomic, strong) CTFMap *map;

//- (BOOL)validateMap:(id*)value_ error:(NSError**)error_;




@property (nonatomic, strong) NSSet *players;

- (NSMutableSet*)playersSet;





@end

@interface _CTFGame (CoreDataGeneratedAccessors)

- (void)addItems:(NSSet*)value_;
- (void)removeItems:(NSSet*)value_;
- (void)addItemsObject:(CTFItem*)value_;
- (void)removeItemsObject:(CTFItem*)value_;

- (void)addPlayers:(NSSet*)value_;
- (void)removePlayers:(NSSet*)value_;
- (void)addPlayersObject:(CTFCharacter*)value_;
- (void)removePlayersObject:(CTFCharacter*)value_;

@end

@interface _CTFGame (CoreDataGeneratedPrimitiveAccessors)


- (NSDate*)primitiveCreatedDate;
- (void)setPrimitiveCreatedDate:(NSDate*)value;




- (NSString*)primitiveDesc;
- (void)setPrimitiveDesc:(NSString*)value;




- (NSString*)primitiveGameId;
- (void)setPrimitiveGameId:(NSString*)value;




- (NSNumber*)primitiveMaxPlayers;
- (void)setPrimitiveMaxPlayers:(NSNumber*)value;

- (int16_t)primitiveMaxPlayersValue;
- (void)setPrimitiveMaxPlayersValue:(int16_t)value_;




- (NSDate*)primitiveModifiedDate;
- (void)setPrimitiveModifiedDate:(NSDate*)value;




- (NSString*)primitiveName;
- (void)setPrimitiveName:(NSString*)value;




- (NSDate*)primitiveStartTime;
- (void)setPrimitiveStartTime:(NSDate*)value;




- (NSNumber*)primitiveStatus;
- (void)setPrimitiveStatus:(NSNumber*)value;

- (int16_t)primitiveStatusValue;
- (void)setPrimitiveStatusValue:(int16_t)value_;





- (NSMutableSet*)primitiveItems;
- (void)setPrimitiveItems:(NSMutableSet*)value;



- (CTFMap*)primitiveMap;
- (void)setPrimitiveMap:(CTFMap*)value;



- (NSMutableSet*)primitivePlayers;
- (void)setPrimitivePlayers:(NSMutableSet*)value;


@end
