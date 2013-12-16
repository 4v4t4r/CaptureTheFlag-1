//
//  CTFJoinViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 16/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFJoinViewController.h"
#import "CTFJoinGameCell.h"

@interface CTFJoinViewController () <UITableViewDelegate, UITableViewDataSource>
@property (weak, nonatomic) IBOutlet UITableView *tableView;

@end

@implementation CTFJoinViewController {
    NSMutableArray *_content;
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
    return 10;
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
   
    NSString *cellIdentifier = NSStringFromClass([CTFJoinGameCell class]);
    CTFJoinGameCell *cell = (CTFJoinGameCell *)[tableView dequeueReusableCellWithIdentifier:cellIdentifier];
    
    if (!cell) {
        cell = [[CTFJoinGameCell alloc] initWithStyle:UITableViewCellStyleSubtitle reuseIdentifier:cellIdentifier];
    }

    cell.titleLabel.text = @"Na jasnych błoniach";
    cell.distanceLabel.text = @"1.2km";
    cell.startDateLabel.text = @"23-12-2013 14:30";
    
    return cell;
}


#pragma mark - CTFViewControllerProtocol
- (void)localizeUI {
    self.navigationItem.title = NSLocalizedString(@"view.join.navigation.title", nil);
}

@end
