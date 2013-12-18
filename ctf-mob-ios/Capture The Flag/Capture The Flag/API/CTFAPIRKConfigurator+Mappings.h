//
//  CTFAPIRKConfigurator+Mappings.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPIRKConfigurator.h"

@interface CTFAPIRKConfigurator (Mappings)
#warning [tsu] add description
- (RKEntityMapping *)entityMappingFromClass:(Class)aClass;

@end
