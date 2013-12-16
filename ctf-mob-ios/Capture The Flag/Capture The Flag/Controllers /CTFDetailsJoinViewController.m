//
//  CTFDetailsJoinViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 16/12/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFDetailsJoinViewController.h"
#import "CTFGame.h"

@interface CTFDetailsJoinViewController ()

@end

@implementation CTFDetailsJoinViewController {
    CTFGame *_game;
}

- (void)viewDidLoad
{
    [super viewDidLoad];
    
    self.navigationItem.title = _game.name;
}

- (void)setGame:(CTFGame *)game {
    _game = game;
}

@end
