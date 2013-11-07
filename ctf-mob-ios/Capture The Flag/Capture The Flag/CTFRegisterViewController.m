//
//  CTFRegisterViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07/11/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFRegisterViewController.h"

@implementation CTFRegisterViewController

- (void)viewDidLoad
{
    [super viewDidLoad];
    [self localizeUI];
}

- (void)localizeUI
{
    self.navigationItem.title = NSLocalizedString(@"view.register.navigation.title", nil);
    _emailTF.placeholder = NSLocalizedString(@"view.register.textField.email.placeholder", nil);
    _usernameTF.placeholder = NSLocalizedString(@"view.register.textField.username.placeholder", nil);
    _passwordTF.placeholder = NSLocalizedString(@"view.register.textField.password.placeholder", nil);
    _rePasswordTF.placeholder = NSLocalizedString(@"view.register.textField.re-password.placeholder", nil);
    [_registerBtn setTitle:NSLocalizedString(@"view.register.button.register.title", nil) forState:UIControlStateNormal];
}

@end
