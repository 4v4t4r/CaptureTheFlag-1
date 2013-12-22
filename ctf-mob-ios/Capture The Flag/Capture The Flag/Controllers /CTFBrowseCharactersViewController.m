//
//  CTFBrowseCharactersViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 22.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFBrowseCharactersViewController.h"

@implementation CTFBrowseCharactersViewController

- (void)viewDidLoad
{
    [super viewDidLoad];
	// Do any additional setup after loading the view.
}

- (void)localizeUI {
    self.navigationItem.title = NSLocalizedStringFromTable(@"navigationItem.title", @"BrowseCharacters", @"");
}

@end
