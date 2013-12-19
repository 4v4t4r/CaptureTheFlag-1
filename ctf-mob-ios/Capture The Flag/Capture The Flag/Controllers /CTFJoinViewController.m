//
//  CTFJoinViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 16/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFJoinViewController.h"
#import "CTFDetailsJoinViewController.h"
#import "CTFJoinGameCell.h"
#import "CTFGame.h"
#import "CTFMap.h"
#import "ArrayDataSource.h"
#import "CoreDataService.h"

@interface CTFJoinViewController ()
@property (weak, nonatomic) IBOutlet UITableView *tableView;
@property (strong, nonatomic) IBOutlet ArrayDataSource *dataSource;
@end

@implementation CTFJoinViewController {
    NSArray *_content;
}

- (void)viewWillAppear:(BOOL)animated {
    [super viewWillAppear:animated];
    
    _content = [self _prepareContent];
    
    [_dataSource setItems:_content];
    [_dataSource setCellIdentifier:NSStringFromClass([CTFJoinGameCell class])];
    [_dataSource setConfigureCellBlock:^(CTFJoinGameCell *cell, CTFGame *game) {
        cell.titleLabel.text = game.name;
        cell.distanceLabel.text = @"1.2km";
        cell.startDateLabel.text = @"23-12-2013 14:30";
    }];
}

- (NSArray *)_prepareContent {
    NSMutableArray *content = [NSMutableArray new];
    
    CTFGame *game =
    [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFGame class])
                                  inManagedObjectContext:[CoreDataService sharedInstance].managedObjectContext];

    game.name = @"Potyczka na Jasnych Bł.";

    CTFMap *map =
    [NSEntityDescription insertNewObjectForEntityForName:NSStringFromClass([CTFMap class])
                                  inManagedObjectContext:[CoreDataService sharedInstance].managedObjectContext];
    map.createdBy = @"tomkowz";
    map.location = @[@(53.43485), @(14.566391)];
    game.map = map;
    
    [content addObject:game];
    return content;
}

- (void)viewDidLoad
{
    [super viewDidLoad];
}


#pragma mark - Segue support
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    static NSString *toDetailsJoin = @"ToDetailsJoin";
    if ([segue.identifier isEqualToString:toDetailsJoin]) {
        CTFDetailsJoinViewController *dvc = (CTFDetailsJoinViewController *)segue.destinationViewController;
        
        CTFGame *game = (CTFGame *)_content[_tableView.indexPathForSelectedRow.row];
        [dvc setGame:game];
    }
}


#pragma mark - CTFViewControllerProtocol
- (void)localizeUI {
    self.navigationItem.title = NSLocalizedString(@"view.join.navigation.title", nil);
}


#pragma mark - Accessors
- (UITableView *)tableView {
    return _tableView;
}

- (ArrayDataSource *)dataSource {
    return _dataSource;
}

@end
