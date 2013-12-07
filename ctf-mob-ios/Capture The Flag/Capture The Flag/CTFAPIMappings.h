//
//  CTFAPIMappings.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 06.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface CTFAPIMappings : NSObject

@property (readonly) RKObjectManager *manager;

+ (instancetype)sharedInstance;
+ (void)setSharedInstance:(CTFAPIMappings *)sharedInstance;
- (instancetype)initWithManager:(RKObjectManager *)manager;

- (RKEntityMapping *)userMapping;
- (RKEntityMapping *)characterMapping;

@end
