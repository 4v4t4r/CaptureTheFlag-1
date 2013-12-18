//
//  BaseModelTest.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "BaseModelTest.h"

@implementation BaseModelTest

- (void)setUp
{
    [super setUp];
    _service = [[CoreDataService alloc] initForUnitTesting];
    [CoreDataService setSharedInstance:_service];
}

- (void)tearDown
{
    [CoreDataService setSharedInstance:nil];
    _service = nil;
    [super tearDown];
}

@end
