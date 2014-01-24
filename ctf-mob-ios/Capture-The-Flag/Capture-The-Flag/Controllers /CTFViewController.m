//
//  CTFViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFViewController.h"

@implementation CTFViewController

- (void)viewDidLoad
{
    [super viewDidLoad];
    
    if ([self respondsToSelector:@selector(localizeUI)])
        [self performSelector:@selector(localizeUI)];
}

- (void)localizeUI {
    /// Not implemented in base class.
}

@end
