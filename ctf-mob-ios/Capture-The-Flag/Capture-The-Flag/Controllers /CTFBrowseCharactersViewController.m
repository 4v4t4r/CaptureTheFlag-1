//
//  CTFBrowseCharactersViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 22.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFBrowseCharactersViewController.h"
#import "CTFNoCharactersCell.h"
#import "CTFCharacterCell.h"
#import "CTFCharacter.h"

@implementation CTFBrowseCharactersViewController {
    NSArray *_characters;
}

#pragma mark - External methods
- (void)setCharacters:(NSArray *)characters {
    _characters = characters;
    [_tableView reloadData];
}

#pragma mark - TableViewDelegate & DataSource
- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return (_characters.count > 0) ? _characters.count : 1;
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    id cellToReturn;
    if (_characters.count == 0) {
        CTFNoCharactersCell *cell = [tableView dequeueReusableCellWithIdentifier:NSStringFromClass([CTFNoCharactersCell class]) forIndexPath:indexPath];
        [cell configure];
        cellToReturn = cell;
    } else {
        CTFCharacter *character = _characters[indexPath.row];
        CTFCharacterCell *cell = [tableView dequeueReusableCellWithIdentifier:NSStringFromClass([CTFCharacterCell class]) forIndexPath:indexPath];
        [cell configureCellWithType:character.typeString andLevel:character.level];
        cellToReturn = cell;
    }
    
    return cellToReturn;
}


#pragma mark - CTFViewControllerProtocol
- (void)localizeUI {
    self.navigationItem.title = NSLocalizedStringFromTable(@"navigationItem.title", @"BrowseCharacters", @"");
}

@end
