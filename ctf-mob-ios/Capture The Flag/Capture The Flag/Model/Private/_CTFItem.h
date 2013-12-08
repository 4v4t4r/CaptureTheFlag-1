// DO NOT EDIT. This file is machine-generated and constantly overwritten.
// Make changes to CTFItem.h instead.

#import <CoreData/CoreData.h>
#import "CustomManagedObject.h"

extern const struct CTFItemAttributes {
	__unsafe_unretained NSString *desc;
	__unsafe_unretained NSString *location;
	__unsafe_unretained NSString *name;
	__unsafe_unretained NSString *type;
	__unsafe_unretained NSString *value;
} CTFItemAttributes;

extern const struct CTFItemRelationships {
	__unsafe_unretained NSString *game;
} CTFItemRelationships;

extern const struct CTFItemFetchedProperties {
} CTFItemFetchedProperties;

@class CTFGame;


@class NSObject;




@interface CTFItemID : NSManagedObjectID {}
@end

@interface _CTFItem : CustomManagedObject {}
+ (id)insertInManagedObjectContext:(NSManagedObjectContext*)moc_;
+ (NSString*)entityName;
+ (NSEntityDescription*)entityInManagedObjectContext:(NSManagedObjectContext*)moc_;
- (CTFItemID*)objectID;





@property (nonatomic, strong) NSString* desc;



//- (BOOL)validateDesc:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) id location;



//- (BOOL)validateLocation:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSString* name;



//- (BOOL)validateName:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSNumber* type;



@property int16_t typeValue;
- (int16_t)typeValue;
- (void)setTypeValue:(int16_t)value_;

//- (BOOL)validateType:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSNumber* value;



@property float valueValue;
- (float)valueValue;
- (void)setValueValue:(float)value_;

//- (BOOL)validateValue:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) CTFGame *game;

//- (BOOL)validateGame:(id*)value_ error:(NSError**)error_;





@end

@interface _CTFItem (CoreDataGeneratedAccessors)

@end

@interface _CTFItem (CoreDataGeneratedPrimitiveAccessors)


- (NSString*)primitiveDesc;
- (void)setPrimitiveDesc:(NSString*)value;




- (id)primitiveLocation;
- (void)setPrimitiveLocation:(id)value;




- (NSString*)primitiveName;
- (void)setPrimitiveName:(NSString*)value;




- (NSNumber*)primitiveType;
- (void)setPrimitiveType:(NSNumber*)value;

- (int16_t)primitiveTypeValue;
- (void)setPrimitiveTypeValue:(int16_t)value_;




- (NSNumber*)primitiveValue;
- (void)setPrimitiveValue:(NSNumber*)value;

- (float)primitiveValueValue;
- (void)setPrimitiveValueValue:(float)value_;





- (CTFGame*)primitiveGame;
- (void)setPrimitiveGame:(CTFGame*)value;


@end
