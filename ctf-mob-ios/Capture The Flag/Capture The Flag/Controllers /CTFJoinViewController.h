//
//  CTFJoinViewController.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 16/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFViewController.h"
@class ArrayDataSource;
@interface CTFJoinViewController : CTFViewController <UITableViewDelegate>

@property (nonatomic, readonly) UITableView *tableView;
@property (nonatomic, readonly) ArrayDataSource *dataSource;

@end
