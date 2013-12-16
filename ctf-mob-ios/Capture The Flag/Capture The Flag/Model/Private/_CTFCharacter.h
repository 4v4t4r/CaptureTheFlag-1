// DO NOT EDIT. This file is machine-generated and constantly overwritten.
// Make changes to CTFCharacter.h instead.

#import <CoreData/CoreData.h>
#import "CustomManagedObject.h"

extern const struct CTFCharacterAttributes {
	__unsafe_unretained NSString *health;
	__unsafe_unretained NSString *level;
	__unsafe_unretained NSString *totalScore;
	__unsafe_unretained NSString *totalTime;
	__unsafe_unretained NSString *type;
} CTFCharacterAttributes;

extern const struct CTFCharacterRelationships {
	__unsafe_unretained NSString *game;
	__unsafe_unretained NSString *user;
} CTFCharacterRelationships;

extern const struct CTFCharacterFetchedProperties {
} CTFCharacterFetchedProperties;

@class CTFGame;
@class CTFUser;







@interface CTFCharacterID : NSManagedObjectID {}
@end

@interface _CTFCharacter : CustomManagedObject {}
+ (id)insertInManagedObjectContext:(NSManagedObjectContext*)moc_;
+ (NSString*)entityName;
+ (NSEntityDescription*)entityInManagedObjectContext:(NSManagedObjectContext*)moc_;
- (CTFCharacterID*)objectID;





@property (nonatomic, strong) NSNumber* health;



@property int16_t healthValue;
- (int16_t)healthValue;
- (void)setHealthValue:(int16_t)value_;

//- (BOOL)validateHealth:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSNumber* level;



@property int16_t levelValue;
- (int16_t)levelValue;
- (void)setLevelValue:(int16_t)value_;

//- (BOOL)validateLevel:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSNumber* totalScore;



@property int16_t totalScoreValue;
- (int16_t)totalScoreValue;
- (void)setTotalScoreValue:(int16_t)value_;

//- (BOOL)validateTotalScore:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSNumber* totalTime;



@property int16_t totalTimeValue;
- (int16_t)totalTimeValue;
- (void)setTotalTimeValue:(int16_t)value_;

//- (BOOL)validateTotalTime:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSNumber* type;



@property int16_t typeValue;
- (int16_t)typeValue;
- (void)setTypeValue:(int16_t)value_;

//- (BOOL)validateType:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) CTFGame *game;

//- (BOOL)validateGame:(id*)value_ error:(NSError**)error_;




@property (nonatomic, strong) CTFUser *user;

//- (BOOL)validateUser:(id*)value_ error:(NSError**)error_;





@end

@interface _CTFCharacter (CoreDataGeneratedAccessors)

@end

@interface _CTFCharacter (CoreDataGeneratedPrimitiveAccessors)


- (NSNumber*)primitiveHealth;
- (void)setPrimitiveHealth:(NSNumber*)value;

- (int16_t)primitiveHealthValue;
- (void)setPrimitiveHealthValue:(int16_t)value_;




- (NSNumber*)primitiveLevel;
- (void)setPrimitiveLevel:(NSNumber*)value;

- (int16_t)primitiveLevelValue;
- (void)setPrimitiveLevelValue:(int16_t)value_;




- (NSNumber*)primitiveTotalScore;
- (void)setPrimitiveTotalScore:(NSNumber*)value;

- (int16_t)primitiveTotalScoreValue;
- (void)setPrimitiveTotalScoreValue:(int16_t)value_;




- (NSNumber*)primitiveTotalTime;
- (void)setPrimitiveTotalTime:(NSNumber*)value;

- (int16_t)primitiveTotalTimeValue;
- (void)setPrimitiveTotalTimeValue:(int16_t)value_;




- (NSNumber*)primitiveType;
- (void)setPrimitiveType:(NSNumber*)value;

- (int16_t)primitiveTypeValue;
- (void)setPrimitiveTypeValue:(int16_t)value_;





- (CTFGame*)primitiveGame;
- (void)setPrimitiveGame:(CTFGame*)value;



- (CTFUser*)primitiveUser;
- (void)setPrimitiveUser:(CTFUser*)value;


@end
