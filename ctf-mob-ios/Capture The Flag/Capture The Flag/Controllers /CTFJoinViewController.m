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

@interface CTFJoinViewController () <UITableViewDelegate, UITableViewDataSource>
@property (weak, nonatomic) IBOutlet UITableView *tableView;

@end

@implementation CTFJoinViewController {
    NSMutableArray *_content;
}

- (void)viewWillAppear:(BOOL)animated {
    [super viewWillAppear:animated];
    
    [self _prepareContent];
}

#warning [tsu] implement!
- (void)_prepareContent {
    if (!_content) {
        _content = [NSMutableArray new];
    } else {
        [_content removeAllObjects];
    }
    
    CTFGame *game = [CTFGame createObject];
    game.name = @"Potyczka na Jasnych Bł.";

    CTFMap *map = [CTFMap createObject];
    map.createdBy = @"tomkowz";
    map.location = @[@(53.43485), @(14.566391)];
    game.map = map;
    
    [_content addObject:game];
}

- (void)viewDidLoad
{
    [super viewDidLoad];
}


#pragma mark - UITableViewDelegagte & DataSource
- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
    return 1;
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return _content.count;
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
   
    NSString *cellIdentifier = NSStringFromClass([CTFJoinGameCell class]);
    CTFJoinGameCell *cell = (CTFJoinGameCell *)[tableView dequeueReusableCellWithIdentifier:cellIdentifier];
    
    if (!cell) {
        cell = [[CTFJoinGameCell alloc] initWithStyle:UITableViewCellStyleSubtitle reuseIdentifier:cellIdentifier];
    }

    CTFGame *game = _content[indexPath.row];
    cell.titleLabel.text = game.name;
    cell.distanceLabel.text = @"1.2km";
    cell.startDateLabel.text = @"23-12-2013 14:30";
    
    return cell;
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

@end
