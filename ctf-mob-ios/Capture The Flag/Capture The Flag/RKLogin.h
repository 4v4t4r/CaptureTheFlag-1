//
//  RKLogin.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 09.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface RKLogin : NSObject

@property (nonatomic) BOOL success;
@property (nonatomic) NSString *token;
@property (nonatomic) NSString *message;

+ (RKObjectMapping *)loginMapping;

@end
