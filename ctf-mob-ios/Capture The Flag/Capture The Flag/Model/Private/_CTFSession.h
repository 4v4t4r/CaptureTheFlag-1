// DO NOT EDIT. This file is machine-generated and constantly overwritten.
// Make changes to CTFSession.h instead.

#import <CoreData/CoreData.h>


extern const struct CTFSessionAttributes {
	__unsafe_unretained NSString *fixedPassword;
	__unsafe_unretained NSString *token;
} CTFSessionAttributes;

extern const struct CTFSessionRelationships {
	__unsafe_unretained NSString *currentUser;
} CTFSessionRelationships;

extern const struct CTFSessionFetchedProperties {
} CTFSessionFetchedProperties;

@class CTFUser;




@interface CTFSessionID : NSManagedObjectID {}
@end

@interface _CTFSession : NSManagedObject {}
+ (id)insertInManagedObjectContext:(NSManagedObjectContext*)moc_;
+ (NSString*)entityName;
+ (NSEntityDescription*)entityInManagedObjectContext:(NSManagedObjectContext*)moc_;
- (CTFSessionID*)objectID;





@property (nonatomic, strong) NSString* fixedPassword;



//- (BOOL)validateFixedPassword:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong, readonly) NSString* token;



//- (BOOL)validateToken:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) CTFUser *currentUser;

//- (BOOL)validateCurrentUser:(id*)value_ error:(NSError**)error_;





@end

@interface _CTFSession (CoreDataGeneratedAccessors)

@end

@interface _CTFSession (CoreDataGeneratedPrimitiveAccessors)


- (NSString*)primitiveFixedPassword;
- (void)setPrimitiveFixedPassword:(NSString*)value;




- (NSString*)primitiveToken;
- (void)setPrimitiveToken:(NSString*)value;





- (CTFUser*)primitiveCurrentUser;
- (void)setPrimitiveCurrentUser:(CTFUser*)value;


@end
