//
//  CTFAPIRKConfigurator+Mappings.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIRKConfigurator.h"

@interface CTFAPIRKConfigurator (Mappings)
/**
 @define Method returns RKEntityMapping for given class.
 @abstract Returns EntityMapping for entity class, otherwise nil.
 @discussion 
 @param aClass Class of entity object.
 */
- (RKEntityMapping *)entityMappingFromClass:(Class)aClass;

@end
