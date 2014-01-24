//
//  CTFNoCharactersCell.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 22.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFNoCharactersCell.h"

@implementation CTFNoCharactersCell

- (void)configure {
    _titleLabel.text = NSLocalizedStringFromTable(@"cell.noCharacters.label.title", @"BrowseCharacters", @"");
}

@end
