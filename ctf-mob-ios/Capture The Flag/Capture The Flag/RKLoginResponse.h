//
//  RKLoginResponse.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 09.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface RKLoginResponse : NSObject

@property (nonatomic) BOOL success;
@property (nonatomic) NSString *token;
@property (nonatomic) NSString *message;

+ (RKObjectMapping *)objectMapping;

@end