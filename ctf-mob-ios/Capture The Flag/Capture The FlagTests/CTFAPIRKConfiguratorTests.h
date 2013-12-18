//
//  CTFAPIRKConfiguratorTests.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <XCTest/XCTest.h>
#import <RestKit/RestKit.h>

@class CoreDataService;
@class CTFAPIRKConfigurator;

@interface CTFAPIRKConfiguratorTests : XCTestCase
@property (nonatomic) CoreDataService *service;
@property (nonatomic) RKObjectManager *manager;
@property (nonatomic) CTFAPIRKConfigurator *configurator;
@end

