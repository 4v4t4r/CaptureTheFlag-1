//
//  CustomManagedObject.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CustomManagedObject.h"
#import "CoreDataService.h"

@implementation CustomManagedObject

+ (instancetype)createObject
{
    id entity = [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([self class]) inManagedObjectContext:[self moc]];
    return entity;
}

+ (NSFetchRequest *)fetchRequestWithPredicate:(NSPredicate *)predicate
{
    NSFetchRequest *request = [[NSFetchRequest alloc] init];
    [request setEntity:[NSEntityDescription entityForName:NSStringFromClass([self class])
                                   inManagedObjectContext:[self moc]]];
    [request setPredicate:predicate];
    
    return request;
}


+ (NSManagedObjectContext *)moc
{
    return [CoreDataService sharedInstance].managedObjectContext;
}

@end
