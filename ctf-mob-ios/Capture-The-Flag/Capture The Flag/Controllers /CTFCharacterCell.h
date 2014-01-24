//
//  CTFCharacterCell.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 22.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface CTFCharacterCell : UITableViewCell
@property (weak, nonatomic) IBOutlet UILabel *typeLabel;
@property (weak, nonatomic) IBOutlet UILabel *levelLabel;

- (void)configureCellWithType:(NSString *)type andLevel:(NSNumber *)level;

@end
