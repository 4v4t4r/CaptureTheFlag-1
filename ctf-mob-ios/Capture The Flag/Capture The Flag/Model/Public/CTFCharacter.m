#import "CTFCharacter.h"


@interface CTFCharacter ()

@end

@implementation CTFCharacter

- (NSString *)typeString {
    NSString *typeString = @"";
    switch ([self.type integerValue]) {
        case 0:
            typeString = @"Private";
            break;

        case 1:
            typeString = @"Medic";
            break;
            
        case 2:
            typeString = @"Commandos";
            break;
            
        case 3:
            typeString = @"Spy";
            break;
    }
    return typeString;
}

@end
