// DO NOT EDIT. This file is machine-generated and constantly overwritten.
// Make changes to CTFSession.m instead.

#import "_CTFSession.h"

const struct CTFSessionAttributes CTFSessionAttributes = {
	.token = @"token",
};

const struct CTFSessionRelationships CTFSessionRelationships = {
	.currentUser = @"currentUser",
};

const struct CTFSessionFetchedProperties CTFSessionFetchedProperties = {
};

@implementation CTFSessionID
@end

@implementation _CTFSession

+ (id)insertInManagedObjectContext:(NSManagedObjectContext*)moc_ {
	NSParameterAssert(moc_);
	return [NSEntityDescription insertNewObjectForEntityForName:@"CTFSession" inManagedObjectContext:moc_];
}

+ (NSString*)entityName {
	return @"CTFSession";
}

+ (NSEntityDescription*)entityInManagedObjectContext:(NSManagedObjectContext*)moc_ {
	NSParameterAssert(moc_);
	return [NSEntityDescription entityForName:@"CTFSession" inManagedObjectContext:moc_];
}

- (CTFSessionID*)objectID {
	return (CTFSessionID*)[super objectID];
}

+ (NSSet*)keyPathsForValuesAffectingValueForKey:(NSString*)key {
	NSSet *keyPaths = [super keyPathsForValuesAffectingValueForKey:key];
	

	return keyPaths;
}




@dynamic token;






@dynamic currentUser;

	






@end
