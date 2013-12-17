// DO NOT EDIT. This file is machine-generated and constantly overwritten.
// Make changes to CTFUser.m instead.

#import "_CTFUser.h"

const struct CTFUserAttributes CTFUserAttributes = {
	.email = @"email",
	.location = @"location",
	.nick = @"nick",
	.password = @"password",
	.userId = @"userId",
	.username = @"username",
};

const struct CTFUserRelationships CTFUserRelationships = {
	.characters = @"characters",
	.session = @"session",
};

const struct CTFUserFetchedProperties CTFUserFetchedProperties = {
};

@implementation CTFUserID
@end

@implementation _CTFUser

+ (id)insertInManagedObjectContext:(NSManagedObjectContext*)moc_ {
	NSParameterAssert(moc_);
	return [NSEntityDescription insertNewObjectForEntityForName:@"CTFUser" inManagedObjectContext:moc_];
}

+ (NSString*)entityName {
	return @"CTFUser";
}

+ (NSEntityDescription*)entityInManagedObjectContext:(NSManagedObjectContext*)moc_ {
	NSParameterAssert(moc_);
	return [NSEntityDescription entityForName:@"CTFUser" inManagedObjectContext:moc_];
}

- (CTFUserID*)objectID {
	return (CTFUserID*)[super objectID];
}

+ (NSSet*)keyPathsForValuesAffectingValueForKey:(NSString*)key {
	NSSet *keyPaths = [super keyPathsForValuesAffectingValueForKey:key];
	
	if ([key isEqualToString:@"userIdValue"]) {
		NSSet *affectingKey = [NSSet setWithObject:@"userId"];
		keyPaths = [keyPaths setByAddingObjectsFromSet:affectingKey];
		return keyPaths;
	}

	return keyPaths;
}




@dynamic email;






@dynamic location;






@dynamic nick;






@dynamic password;






@dynamic userId;



- (int32_t)userIdValue {
	NSNumber *result = [self userId];
	return [result intValue];
}

- (void)setUserIdValue:(int32_t)value_ {
	[self setUserId:[NSNumber numberWithInt:value_]];
}

- (int32_t)primitiveUserIdValue {
	NSNumber *result = [self primitiveUserId];
	return [result intValue];
}

- (void)setPrimitiveUserIdValue:(int32_t)value_ {
	[self setPrimitiveUserId:[NSNumber numberWithInt:value_]];
}





@dynamic username;






@dynamic characters;

	
- (NSMutableOrderedSet*)charactersSet {
	[self willAccessValueForKey:@"characters"];
  
	NSMutableOrderedSet *result = (NSMutableOrderedSet*)[self mutableOrderedSetValueForKey:@"characters"];
  
	[self didAccessValueForKey:@"characters"];
	return result;
}
	

@dynamic session;

	






@end
