// DO NOT EDIT. This file is machine-generated and constantly overwritten.
// Make changes to CTFGame.m instead.

#import "_CTFGame.h"

const struct CTFGameAttributes CTFGameAttributes = {
	.created_date = @"created_date",
	.desc = @"desc",
	.gameId = @"gameId",
	.map = @"map",
	.max_players = @"max_players",
	.modified_date = @"modified_date",
	.name = @"name",
	.start_time = @"start_time",
	.status = @"status",
};

const struct CTFGameRelationships CTFGameRelationships = {
	.items = @"items",
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
	
	if ([key isEqualToString:@"gameIdValue"]) {
		NSSet *affectingKey = [NSSet setWithObject:@"gameId"];
		keyPaths = [keyPaths setByAddingObjectsFromSet:affectingKey];
		return keyPaths;
	}
	if ([key isEqualToString:@"max_playersValue"]) {
		NSSet *affectingKey = [NSSet setWithObject:@"max_players"];
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




@dynamic created_date;






@dynamic desc;






@dynamic gameId;



- (int32_t)gameIdValue {
	NSNumber *result = [self gameId];
	return [result intValue];
}

- (void)setGameIdValue:(int32_t)value_ {
	[self setGameId:[NSNumber numberWithInt:value_]];
}

- (int32_t)primitiveGameIdValue {
	NSNumber *result = [self primitiveGameId];
	return [result intValue];
}

- (void)setPrimitiveGameIdValue:(int32_t)value_ {
	[self setPrimitiveGameId:[NSNumber numberWithInt:value_]];
}





@dynamic map;






@dynamic max_players;



- (int16_t)max_playersValue {
	NSNumber *result = [self max_players];
	return [result shortValue];
}

- (void)setMax_playersValue:(int16_t)value_ {
	[self setMax_players:[NSNumber numberWithShort:value_]];
}

- (int16_t)primitiveMax_playersValue {
	NSNumber *result = [self primitiveMax_players];
	return [result shortValue];
}

- (void)setPrimitiveMax_playersValue:(int16_t)value_ {
	[self setPrimitiveMax_players:[NSNumber numberWithShort:value_]];
}





@dynamic modified_date;






@dynamic name;






@dynamic start_time;






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
	

@dynamic players;

	
- (NSMutableSet*)playersSet {
	[self willAccessValueForKey:@"players"];
  
	NSMutableSet *result = (NSMutableSet*)[self mutableSetValueForKey:@"players"];
  
	[self didAccessValueForKey:@"players"];
	return result;
}
	






@end
