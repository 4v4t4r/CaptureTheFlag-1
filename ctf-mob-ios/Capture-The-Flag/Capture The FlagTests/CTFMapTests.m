//
//  CTFMapTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "BaseModelTest.h"
#import "CTFMap.h"
#import "CTFMap+UnitTesting.h"

@interface CTFMapTests : BaseModelTest

@end

@implementation CTFMapTests

- (void)testThatMapShouldExists {
    CTFMap *map =
    [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFMap class])
                                  inManagedObjectContext:self.service.managedObjectContext];
    XCTAssertNotNil(map, @"");
}

- (void)testThatLocationIsCorrectlyCastedToCLLocation {
    NSEntityDescription *entity =
    [NSEntityDescription entityForName:NSStringFromClass([CTFMap class]) inManagedObjectContext:self.service.managedObjectContext];
    NSArray *locationArray = @[@(10.5), @(20.1)];
    CTFMap *map = [[CTFMap alloc] initWithEntity:entity insertIntoManagedObjectContext:self.service.managedObjectContext location:locationArray];
    CLLocation *location = [[CLLocation alloc] initWithLatitude:[locationArray[0] floatValue] longitude:[locationArray[1] floatValue]];
    XCTAssertEqualWithAccuracy(map.locationCoordinates.coordinate.latitude, location.coordinate.latitude, 0.01, @"");
    XCTAssertEqualWithAccuracy(map.locationCoordinates.coordinate.longitude, location.coordinate.longitude, 0.01, @"");
}

@end
