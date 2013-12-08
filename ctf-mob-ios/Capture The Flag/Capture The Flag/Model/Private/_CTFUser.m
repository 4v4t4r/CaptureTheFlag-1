// DO NOT EDIT. This file is machine-generated and constantly overwritten.
// Make changes to CTFUser.m instead.

#import "_CTFUser.h"

const struct CTFUserAttributes CTFUserAttributes = {
	.email = @"email",
	.location = @"location",
	.nick = @"nick",
	.password = @"password",
	.username = @"username",
};

const struct CTFUserRelationships CTFUserRelationships = {
	.characters = @"characters",
	.game = @"game",
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
	

	return keyPaths;
}




@dynamic email;






@dynamic location;






@dynamic nick;






@dynamic password;






@dynamic username;






@dynamic characters;

	
- (NSMutableOrderedSet*)charactersSet {
	[self willAccessValueForKey:@"characters"];
  
	NSMutableOrderedSet *result = (NSMutableOrderedSet*)[self mutableOrderedSetValueForKey:@"characters"];
  
	[self didAccessValueForKey:@"characters"];
	return result;
}
	

@dynamic game;

	

@dynamic session;

	






@end
