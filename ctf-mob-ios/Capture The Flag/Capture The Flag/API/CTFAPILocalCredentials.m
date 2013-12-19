//
//  CTFLocalCredentials.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFAPILocalCredentials.h"
#import "STKeychain.h"
@implementation CTFAPILocalCredentials

- (instancetype)initWithUsername:(NSString *)username password:(NSString *)password {
    
    if (!username || username.length == 0 ||
        !password || password.length == 0)
        return nil;
    
    self = [super init];
    if (self) {
        _username = username;
        _password = password;
    }
    return self;
}

@end
