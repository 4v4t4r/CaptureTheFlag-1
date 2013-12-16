//
//  ArrayDataSource.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 16.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "ArrayDataSource.h"

@interface ArrayDataSource ()
@property (nonatomic, copy) ConfigureCellBlock configureCellBlock;
@end

@implementation ArrayDataSource {
    NSArray *_items;
    NSString *_cellIdentifier;
}

#pragma mark - External
- (id)initWithItems:(NSArray *)items cellIdentifier:(NSString *)cellIdentifier configureCellBlock:(ConfigureCellBlock)block {
    self = [super init];
    if (self) {
        _items = items;
        _cellIdentifier = cellIdentifier;
        _configureCellBlock = [block copy];
    }
    return self;
}

- (void)setItems:(NSArray *)items {
    _items = items;
}

- (void)setCellIdentifier:(NSString *)cellIdentifier {
    _cellIdentifier = cellIdentifier;
}

- (void)setConfigureCellBlock:(ConfigureCellBlock)block {
    _configureCellBlock = block;
}


#pragma mark - Private
- (id)_itemAtIndexPath:(NSIndexPath *)indexPath {
    return _items[indexPath.row];
}

#pragma mark - UITableViewDataSource
- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return _items.count;
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    id cell = [tableView dequeueReusableCellWithIdentifier:_cellIdentifier forIndexPath:indexPath];
    id item = [self _itemAtIndexPath:indexPath];
    _configureCellBlock(cell, item);
    return cell;
}


#pragma mark - Accessors
- (NSArray *)items {
    return _items;
}

- (NSString *)cellIdentifier {
    return _cellIdentifier;
}

@end
