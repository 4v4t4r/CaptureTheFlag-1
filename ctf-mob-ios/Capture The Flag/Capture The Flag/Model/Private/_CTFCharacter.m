// DO NOT EDIT. This file is machine-generated and constantly overwritten.
// Make changes to CTFCharacter.m instead.

#import "_CTFCharacter.h"

const struct CTFCharacterAttributes CTFCharacterAttributes = {
	.health = @"health",
	.level = @"level",
	.totalScore = @"totalScore",
	.totalTime = @"totalTime",
	.type = @"type",
};

const struct CTFCharacterRelationships CTFCharacterRelationships = {
	.game = @"game",
	.user = @"user",
};

const struct CTFCharacterFetchedProperties CTFCharacterFetchedProperties = {
};

@implementation CTFCharacterID
@end

@implementation _CTFCharacter

+ (id)insertInManagedObjectContext:(NSManagedObjectContext*)moc_ {
	NSParameterAssert(moc_);
	return [NSEntityDescription insertNewObjectForEntityForName:@"CTFCharacter" inManagedObjectContext:moc_];
}

+ (NSString*)entityName {
	return @"CTFCharacter";
}

+ (NSEntityDescription*)entityInManagedObjectContext:(NSManagedObjectContext*)moc_ {
	NSParameterAssert(moc_);
	return [NSEntityDescription entityForName:@"CTFCharacter" inManagedObjectContext:moc_];
}

- (CTFCharacterID*)objectID {
	return (CTFCharacterID*)[super objectID];
}

+ (NSSet*)keyPathsForValuesAffectingValueForKey:(NSString*)key {
	NSSet *keyPaths = [super keyPathsForValuesAffectingValueForKey:key];
	
	if ([key isEqualToString:@"healthValue"]) {
		NSSet *affectingKey = [NSSet setWithObject:@"health"];
		keyPaths = [keyPaths setByAddingObjectsFromSet:affectingKey];
		return keyPaths;
	}
	if ([key isEqualToString:@"levelValue"]) {
		NSSet *affectingKey = [NSSet setWithObject:@"level"];
		keyPaths = [keyPaths setByAddingObjectsFromSet:affectingKey];
		return keyPaths;
	}
	if ([key isEqualToString:@"totalScoreValue"]) {
		NSSet *affectingKey = [NSSet setWithObject:@"totalScore"];
		keyPaths = [keyPaths setByAddingObjectsFromSet:affectingKey];
		return keyPaths;
	}
	if ([key isEqualToString:@"totalTimeValue"]) {
		NSSet *affectingKey = [NSSet setWithObject:@"totalTime"];
		keyPaths = [keyPaths setByAddingObjectsFromSet:affectingKey];
		return keyPaths;
	}
	if ([key isEqualToString:@"typeValue"]) {
		NSSet *affectingKey = [NSSet setWithObject:@"type"];
		keyPaths = [keyPaths setByAddingObjectsFromSet:affectingKey];
		return keyPaths;
	}

	return keyPaths;
}




@dynamic health;



- (int16_t)healthValue {
	NSNumber *result = [self health];
	return [result shortValue];
}

- (void)setHealthValue:(int16_t)value_ {
	[self setHealth:[NSNumber numberWithShort:value_]];
}

- (int16_t)primitiveHealthValue {
	NSNumber *result = [self primitiveHealth];
	return [result shortValue];
}

- (void)setPrimitiveHealthValue:(int16_t)value_ {
	[self setPrimitiveHealth:[NSNumber numberWithShort:value_]];
}





@dynamic level;



- (int16_t)levelValue {
	NSNumber *result = [self level];
	return [result shortValue];
}

- (void)setLevelValue:(int16_t)value_ {
	[self setLevel:[NSNumber numberWithShort:value_]];
}

- (int16_t)primitiveLevelValue {
	NSNumber *result = [self primitiveLevel];
	return [result shortValue];
}

- (void)setPrimitiveLevelValue:(int16_t)value_ {
	[self setPrimitiveLevel:[NSNumber numberWithShort:value_]];
}





@dynamic totalScore;



- (int16_t)totalScoreValue {
	NSNumber *result = [self totalScore];
	return [result shortValue];
}

- (void)setTotalScoreValue:(int16_t)value_ {
	[self setTotalScore:[NSNumber numberWithShort:value_]];
}

- (int16_t)primitiveTotalScoreValue {
	NSNumber *result = [self primitiveTotalScore];
	return [result shortValue];
}

- (void)setPrimitiveTotalScoreValue:(int16_t)value_ {
	[self setPrimitiveTotalScore:[NSNumber numberWithShort:value_]];
}





@dynamic totalTime;



- (int16_t)totalTimeValue {
	NSNumber *result = [self totalTime];
	return [result shortValue];
}

- (void)setTotalTimeValue:(int16_t)value_ {
	[self setTotalTime:[NSNumber numberWithShort:value_]];
}

- (int16_t)primitiveTotalTimeValue {
	NSNumber *result = [self primitiveTotalTime];
	return [result shortValue];
}

- (void)setPrimitiveTotalTimeValue:(int16_t)value_ {
	[self setPrimitiveTotalTime:[NSNumber numberWithShort:value_]];
}





@dynamic type;



- (int16_t)typeValue {
	NSNumber *result = [self type];
	return [result shortValue];
}

- (void)setTypeValue:(int16_t)value_ {
	[self setType:[NSNumber numberWithShort:value_]];
}

- (int16_t)primitiveTypeValue {
	NSNumber *result = [self primitiveType];
	return [result shortValue];
}

- (void)setPrimitiveTypeValue:(int16_t)value_ {
	[self setPrimitiveType:[NSNumber numberWithShort:value_]];
}





@dynamic game;

	

@dynamic user;

	






@end
