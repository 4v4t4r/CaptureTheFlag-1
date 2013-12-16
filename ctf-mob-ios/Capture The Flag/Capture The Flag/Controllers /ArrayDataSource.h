//
//  ArrayDataSource.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 16.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef void (^ConfigureCellBlock)(id cell, id object);

@interface ArrayDataSource : NSObject <UITableViewDataSource>

- initWithItems:(NSArray *)items cellIdentifier:(NSString *)cellIdentifier configureCellBlock:(ConfigureCellBlock)block;

- (void)setItems:(NSArray *)items;
- (void)setCellIdentifier:(NSString *)cellIdentifier;
- (void)setConfigureCellBlock:(ConfigureCellBlock)block;

@end
 