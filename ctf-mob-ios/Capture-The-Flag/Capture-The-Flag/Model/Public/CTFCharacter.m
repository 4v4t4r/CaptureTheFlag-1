#import "CTFCharacter.h"


@interface CTFCharacter ()

@end

@implementation CTFCharacter

- (NSString *)typeString {
    NSString *typeString = @"";
    switch ([self.type integerValue]) {
        case 0:
            typeString = NSLocalizedStringFromTable(@"type.private", @"CTFCharacter", @"");
            break;
            
        case 1:
            typeString = NSLocalizedStringFromTable(@"type.medic", @"CTFCharacter", @"");
            break;
            
        case 2:
            typeString = NSLocalizedStringFromTable(@"type.commandos", @"CTFCharacter", @"");
            break;
            
        case 3:
            typeString = NSLocalizedStringFromTable(@"type.spy", @"CTFCharacter", @"");
            break;
    }
    
    return typeString;
}

@end
