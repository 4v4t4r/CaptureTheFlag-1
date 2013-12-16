// DO NOT EDIT. This file is machine-generated and constantly overwritten.
// Make changes to CTFMap.h instead.

#import <CoreData/CoreData.h>
#import "CustomManagedObject.h"

extern const struct CTFMapAttributes {
	__unsafe_unretained NSString *createdBy;
	__unsafe_unretained NSString *createdDate;
	__unsafe_unretained NSString *desc;
	__unsafe_unretained NSString *location;
	__unsafe_unretained NSString *mapId;
	__unsafe_unretained NSString *modifiedDate;
	__unsafe_unretained NSString *name;
	__unsafe_unretained NSString *radius;
} CTFMapAttributes;

extern const struct CTFMapRelationships {
	__unsafe_unretained NSString *game;
} CTFMapRelationships;

extern const struct CTFMapFetchedProperties {
} CTFMapFetchedProperties;

@class CTFGame;




@class NSObject;





@interface CTFMapID : NSManagedObjectID {}
@end

@interface _CTFMap : CustomManagedObject {}
+ (id)insertInManagedObjectContext:(NSManagedObjectContext*)moc_;
+ (NSString*)entityName;
+ (NSEntityDescription*)entityInManagedObjectContext:(NSManagedObjectContext*)moc_;
- (CTFMapID*)objectID;





@property (nonatomic, strong) NSString* createdBy;



//- (BOOL)validateCreatedBy:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSDate* createdDate;



//- (BOOL)validateCreatedDate:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSString* desc;



//- (BOOL)validateDesc:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) id location;



//- (BOOL)validateLocation:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSNumber* mapId;



@property int32_t mapIdValue;
- (int32_t)mapIdValue;
- (void)setMapIdValue:(int32_t)value_;

//- (BOOL)validateMapId:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSDate* modifiedDate;



//- (BOOL)validateModifiedDate:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSString* name;



//- (BOOL)validateName:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSNumber* radius;



@property float radiusValue;
- (float)radiusValue;
- (void)setRadiusValue:(float)value_;

//- (BOOL)validateRadius:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) CTFGame *game;

//- (BOOL)validateGame:(id*)value_ error:(NSError**)error_;





@end

@interface _CTFMap (CoreDataGeneratedAccessors)

@end

@interface _CTFMap (CoreDataGeneratedPrimitiveAccessors)


- (NSString*)primitiveCreatedBy;
- (void)setPrimitiveCreatedBy:(NSString*)value;




- (NSDate*)primitiveCreatedDate;
- (void)setPrimitiveCreatedDate:(NSDate*)value;




- (NSString*)primitiveDesc;
- (void)setPrimitiveDesc:(NSString*)value;




- (id)primitiveLocation;
- (void)setPrimitiveLocation:(id)value;




- (NSNumber*)primitiveMapId;
- (void)setPrimitiveMapId:(NSNumber*)value;

- (int32_t)primitiveMapIdValue;
- (void)setPrimitiveMapIdValue:(int32_t)value_;




- (NSDate*)primitiveModifiedDate;
- (void)setPrimitiveModifiedDate:(NSDate*)value;




- (NSString*)primitiveName;
- (void)setPrimitiveName:(NSString*)value;




- (NSNumber*)primitiveRadius;
- (void)setPrimitiveRadius:(NSNumber*)value;

- (float)primitiveRadiusValue;
- (void)setPrimitiveRadiusValue:(float)value_;





- (CTFGame*)primitiveGame;
- (void)setPrimitiveGame:(CTFGame*)value;


@end
