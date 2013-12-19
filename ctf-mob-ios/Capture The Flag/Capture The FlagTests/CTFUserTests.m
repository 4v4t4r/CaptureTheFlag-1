//
//  CTFUserTests.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 18.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "BaseModelTest.h"
#import "CTFUser.h"

@interface CTFUserTests : BaseModelTest

@end

@implementation CTFUserTests

- (void)testThatUserShouldExists {
    CTFUser *user =
    [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFUser class])
                                  inManagedObjectContext:self.service.managedObjectContext];
    XCTAssertNotNil(user, @"");
}

@end
