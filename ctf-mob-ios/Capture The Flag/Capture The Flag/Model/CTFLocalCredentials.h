//
//  CTFLocalCredentials.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface CTFLocalCredentials : NSObject

@property (readonly) NSString *username;
@property (readonly) NSString *password;

- (instancetype)initWithUsername:(NSString *)username password:(NSString *)password;

@end
