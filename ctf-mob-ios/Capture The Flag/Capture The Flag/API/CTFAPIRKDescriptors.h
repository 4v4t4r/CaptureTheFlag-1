//
//  CTFAPIRKDescriptors.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 06.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface CTFAPIRKDescriptors : NSObject

@property (readonly) RKObjectManager *manager;

+ (instancetype)sharedInstance;
+ (void)setSharedInstance:(CTFAPIRKDescriptors *)sharedInstance;
- (instancetype)initWithManager:(RKObjectManager *)manager;

#pragma mark - Descriptors
- (RKResponseDescriptor *)getUserResponseDescriptor;

#pragma mark - Mappings
- (RKEntityMapping *)userMapping;
- (RKEntityMapping *)characterMapping;

@end
