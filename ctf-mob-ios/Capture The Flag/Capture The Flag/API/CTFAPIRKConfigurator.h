//
//  CTFAPIRKConfigurator.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface CTFAPIRKConfigurator : NSObject

@property (nonatomic, readonly) RKObjectManager *manager;

- (id)initWithManager:(RKObjectManager *)manager;
- (void)configure;

@end
