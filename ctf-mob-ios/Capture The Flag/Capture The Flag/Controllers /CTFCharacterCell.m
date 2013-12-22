//
//  CTFCharacterCell.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 22.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFCharacterCell.h"

@implementation CTFCharacterCell

- (void)configureCellWithType:(NSString *)type andLevel:(NSNumber *)level {
    NSString *stringType = @"";
    if (type)
        stringType = type;
    
    NSString *stringLevel = @"";
    if (level) {
        stringLevel = [NSString stringWithFormat:NSLocalizedStringFromTable(@"cell.character.label.level", @"BrowseCharacters", @""), level];
    }
    
    _typeLabel.text = stringType;
    _levelLabel.text = stringLevel;
}

@end
