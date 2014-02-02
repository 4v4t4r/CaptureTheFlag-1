//
//  CTFLoginService.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 02.02.2014.
//  Copyright (c) 2014 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "CTFAPIAccounts.h"

@interface CTFLoginService : NSObject

@property (nonatomic, strong) CTFAPIAccounts *accounts;

- (instancetype)initWithAccounts:(CTFAPIAccounts *)accounts;
@end
