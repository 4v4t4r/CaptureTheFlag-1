//
//  CTFAPIOBJToken.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 01.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface CTFAPIOBJToken : NSObject
@property NSString *value;

+ (RKResponseDescriptor *)responseDescriptor;

@end
