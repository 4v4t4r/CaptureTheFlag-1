// DO NOT EDIT. This file is machine-generated and constantly overwritten.
// Make changes to CTFGame.m instead.

#import "_CTFGame.h"

const struct CTFGameAttributes CTFGameAttributes = {
	.createdDate = @"createdDate",
	.desc = @"desc",
	.gameId = @"gameId",
	.maxPlayers = @"maxPlayers",
	.modifiedDate = @"modifiedDate",
	.name = @"name",
	.startTime = @"startTime",
	.status = @"status",
};

const struct CTFGameRelationships CTFGameRelationships = {
	.items = @"items",
	.map = @"map",
	.players = @"players",
};

const struct CTFGameFetchedProperties CTFGameFetchedProperties = {
};

@implementation CTFGameID
@end

@implementation _CTFGame

+ (id)insertInManagedObjectContext:(NSManagedObjectContext*)moc_ {
	NSParameterAssert(moc_);
	return [NSEntityDescription insertNewObjectForEntityForName:@"CTFGame" inManagedObjectContext:moc_];
}

+ (NSString*)entityName {
	return @"CTFGame";
}

+ (NSEntityDescription*)entityInManagedObjectContext:(NSManagedObjectContext*)moc_ {
	NSParameterAssert(moc_);
	return [NSEntityDescription entityForName:@"CTFGame" inManagedObjectContext:moc_];
}

- (CTFGameID*)objectID {
	return (CTFGameID*)[super objectID];
}

+ (NSSet*)keyPathsForValuesAffectingValueForKey:(NSString*)key {
	NSSet *keyPaths = [super keyPathsForValuesAffectingValueForKey:key];
	
	if ([key isEqualToString:@"maxPlayersValue"]) {
		NSSet *affectingKey = [NSSet setWithObject:@"maxPlayers"];
		keyPaths = [keyPaths setByAddingObjectsFromSet:affectingKey];
		return keyPaths;
	}
	if ([key isEqualToString:@"statusValue"]) {
		NSSet *affectingKey = [NSSet setWithObject:@"status"];
		keyPaths = [keyPaths setByAddingObjectsFromSet:affectingKey];
		return keyPaths;
	}

	return keyPaths;
}




@dynamic createdDate;






@dynamic desc;






@dynamic gameId;






@dynamic maxPlayers;



- (int16_t)maxPlayersValue {
	NSNumber *result = [self maxPlayers];
	return [result shortValue];
}

- (void)setMaxPlayersValue:(int16_t)value_ {
	[self setMaxPlayers:[NSNumber numberWithShort:value_]];
}

- (int16_t)primitiveMaxPlayersValue {
	NSNumber *result = [self primitiveMaxPlayers];
	return [result shortValue];
}

- (void)setPrimitiveMaxPlayersValue:(int16_t)value_ {
	[self setPrimitiveMaxPlayers:[NSNumber numberWithShort:value_]];
}





@dynamic modifiedDate;






@dynamic name;






@dynamic startTime;






@dynamic status;



- (int16_t)statusValue {
	NSNumber *result = [self status];
	return [result shortValue];
}

- (void)setStatusValue:(int16_t)value_ {
	[self setStatus:[NSNumber numberWithShort:value_]];
}

- (int16_t)primitiveStatusValue {
	NSNumber *result = [self primitiveStatus];
	return [result shortValue];
}

- (void)setPrimitiveStatusValue:(int16_t)value_ {
	[self setPrimitiveStatus:[NSNumber numberWithShort:value_]];
}





@dynamic items;

	
- (NSMutableSet*)itemsSet {
	[self willAccessValueForKey:@"items"];
  
	NSMutableSet *result = (NSMutableSet*)[self mutableSetValueForKey:@"items"];
  
	[self didAccessValueForKey:@"items"];
	return result;
}
	

@dynamic map;

	

@dynamic players;

	
- (NSMutableSet*)playersSet {
	[self willAccessValueForKey:@"players"];
  
	NSMutableSet *result = (NSMutableSet*)[self mutableSetValueForKey:@"players"];
  
	[self didAccessValueForKey:@"players"];
	return result;
}
	






@end
