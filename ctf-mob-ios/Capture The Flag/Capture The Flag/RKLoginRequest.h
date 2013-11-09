//
//  RKLoginRequest.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 09.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface RKLoginRequest : NSObject

@property (nonatomic) NSString *login;
@property (nonatomic) NSString *password;

+ (RKObjectMapping *)objectMapping;

@end
