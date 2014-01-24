// DO NOT EDIT. This file is machine-generated and constantly overwritten.
// Make changes to CTFMap.m instead.

#import "_CTFMap.h"

const struct CTFMapAttributes CTFMapAttributes = {
	.createdBy = @"createdBy",
	.createdDate = @"createdDate",
	.desc = @"desc",
	.location = @"location",
	.mapId = @"mapId",
	.modifiedDate = @"modifiedDate",
	.name = @"name",
	.radius = @"radius",
};

const struct CTFMapRelationships CTFMapRelationships = {
	.game = @"game",
};

const struct CTFMapFetchedProperties CTFMapFetchedProperties = {
};

@implementation CTFMapID
@end

@implementation _CTFMap

+ (id)insertInManagedObjectContext:(NSManagedObjectContext*)moc_ {
	NSParameterAssert(moc_);
	return [NSEntityDescription insertNewObjectForEntityForName:@"CTFMap" inManagedObjectContext:moc_];
}

+ (NSString*)entityName {
	return @"CTFMap";
}

+ (NSEntityDescription*)entityInManagedObjectContext:(NSManagedObjectContext*)moc_ {
	NSParameterAssert(moc_);
	return [NSEntityDescription entityForName:@"CTFMap" inManagedObjectContext:moc_];
}

- (CTFMapID*)objectID {
	return (CTFMapID*)[super objectID];
}

+ (NSSet*)keyPathsForValuesAffectingValueForKey:(NSString*)key {
	NSSet *keyPaths = [super keyPathsForValuesAffectingValueForKey:key];
	
	if ([key isEqualToString:@"mapIdValue"]) {
		NSSet *affectingKey = [NSSet setWithObject:@"mapId"];
		keyPaths = [keyPaths setByAddingObjectsFromSet:affectingKey];
		return keyPaths;
	}
	if ([key isEqualToString:@"radiusValue"]) {
		NSSet *affectingKey = [NSSet setWithObject:@"radius"];
		keyPaths = [keyPaths setByAddingObjectsFromSet:affectingKey];
		return keyPaths;
	}

	return keyPaths;
}




@dynamic createdBy;






@dynamic createdDate;






@dynamic desc;






@dynamic location;






@dynamic mapId;



- (int32_t)mapIdValue {
	NSNumber *result = [self mapId];
	return [result intValue];
}

- (void)setMapIdValue:(int32_t)value_ {
	[self setMapId:[NSNumber numberWithInt:value_]];
}

- (int32_t)primitiveMapIdValue {
	NSNumber *result = [self primitiveMapId];
	return [result intValue];
}

- (void)setPrimitiveMapIdValue:(int32_t)value_ {
	[self setPrimitiveMapId:[NSNumber numberWithInt:value_]];
}





@dynamic modifiedDate;






@dynamic name;






@dynamic radius;



- (float)radiusValue {
	NSNumber *result = [self radius];
	return [result floatValue];
}

- (void)setRadiusValue:(float)value_ {
	[self setRadius:[NSNumber numberWithFloat:value_]];
}

- (float)primitiveRadiusValue {
	NSNumber *result = [self primitiveRadius];
	return [result floatValue];
}

- (void)setPrimitiveRadiusValue:(float)value_ {
	[self setPrimitiveRadius:[NSNumber numberWithFloat:value_]];
}





@dynamic game;

	






@end
