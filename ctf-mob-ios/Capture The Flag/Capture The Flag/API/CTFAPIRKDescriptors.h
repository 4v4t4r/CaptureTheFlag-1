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
/// Response descriptor used to get whole user object with characters.
- (RKResponseDescriptor *)getUserResponseDescriptor;

#pragma mark - Mappings
- (RKEntityMapping *)characterMapping;
- (RKEntityMapping *)gameMapping;
- (RKEntityMapping *)mapMapping;
- (RKEntityMapping *)itemMapping;
- (RKEntityMapping *)userMapping;

@end
