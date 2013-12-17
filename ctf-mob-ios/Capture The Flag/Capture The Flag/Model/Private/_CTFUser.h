// DO NOT EDIT. This file is machine-generated and constantly overwritten.
// Make changes to CTFUser.h instead.

#import <CoreData/CoreData.h>
#import "CustomManagedObject.h"

extern const struct CTFUserAttributes {
	__unsafe_unretained NSString *email;
	__unsafe_unretained NSString *location;
	__unsafe_unretained NSString *nick;
	__unsafe_unretained NSString *password;
	__unsafe_unretained NSString *userId;
	__unsafe_unretained NSString *username;
} CTFUserAttributes;

extern const struct CTFUserRelationships {
	__unsafe_unretained NSString *characters;
	__unsafe_unretained NSString *session;
} CTFUserRelationships;

extern const struct CTFUserFetchedProperties {
} CTFUserFetchedProperties;

@class CTFCharacter;
@class CTFSession;


@class NSObject;





@interface CTFUserID : NSManagedObjectID {}
@end

@interface _CTFUser : CustomManagedObject {}
+ (id)insertInManagedObjectContext:(NSManagedObjectContext*)moc_;
+ (NSString*)entityName;
+ (NSEntityDescription*)entityInManagedObjectContext:(NSManagedObjectContext*)moc_;
- (CTFUserID*)objectID;





@property (nonatomic, strong) NSString* email;



//- (BOOL)validateEmail:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) id location;



//- (BOOL)validateLocation:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSString* nick;



//- (BOOL)validateNick:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSString* password;



//- (BOOL)validatePassword:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSNumber* userId;



@property int32_t userIdValue;
- (int32_t)userIdValue;
- (void)setUserIdValue:(int32_t)value_;

//- (BOOL)validateUserId:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSString* username;



//- (BOOL)validateUsername:(id*)value_ error:(NSError**)error_;





@property (nonatomic, strong) NSOrderedSet *characters;

- (NSMutableOrderedSet*)charactersSet;




@property (nonatomic, strong) CTFSession *session;

//- (BOOL)validateSession:(id*)value_ error:(NSError**)error_;





@end

@interface _CTFUser (CoreDataGeneratedAccessors)

- (void)addCharacters:(NSOrderedSet*)value_;
- (void)removeCharacters:(NSOrderedSet*)value_;
- (void)addCharactersObject:(CTFCharacter*)value_;
- (void)removeCharactersObject:(CTFCharacter*)value_;

@end

@interface _CTFUser (CoreDataGeneratedPrimitiveAccessors)


- (NSString*)primitiveEmail;
- (void)setPrimitiveEmail:(NSString*)value;




- (id)primitiveLocation;
- (void)setPrimitiveLocation:(id)value;




- (NSString*)primitiveNick;
- (void)setPrimitiveNick:(NSString*)value;




- (NSString*)primitivePassword;
- (void)setPrimitivePassword:(NSString*)value;




- (NSNumber*)primitiveUserId;
- (void)setPrimitiveUserId:(NSNumber*)value;

- (int32_t)primitiveUserIdValue;
- (void)setPrimitiveUserIdValue:(int32_t)value_;




- (NSString*)primitiveUsername;
- (void)setPrimitiveUsername:(NSString*)value;





- (NSMutableOrderedSet*)primitiveCharacters;
- (void)setPrimitiveCharacters:(NSMutableOrderedSet*)value;



- (CTFSession*)primitiveSession;
- (void)setPrimitiveSession:(CTFSession*)value;


@end
