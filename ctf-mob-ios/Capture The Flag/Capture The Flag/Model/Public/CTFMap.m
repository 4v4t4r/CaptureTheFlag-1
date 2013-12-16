#import "CTFMap.h"


@interface CTFMap ()

// Private interface goes here.

@end


@implementation CTFMap

- (CLLocation *)locationCoordinates {
    NSArray *location = (NSArray *)self.location;
    return [[CLLocation alloc] initWithLatitude:[location[0] floatValue] longitude:[location[1] floatValue]];
}
@end
