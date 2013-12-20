//
//  CTFProfileViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 17.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFProfileViewController.h"
#import "CTFAPIAccounts.h"
#import "CTFAPIConnection.h"
#import "CTFSession.h"

@implementation CTFProfileViewController {
    CTFAPIAccounts *_accounts;
}

- (void)viewDidLoad
{
    [super viewDidLoad];
    _accounts = [[CTFAPIAccounts alloc] initWithConnection:[CTFAPIConnection sharedConnection]];
    [_accounts accountInfoForToken:[CTFSession sharedInstance].token block:^(CTFUser *user) {
        NSLog(@"user = %@", user);
    }];
}

- (void)localizeUI {
    self.navigationItem.title = NSLocalizedString(@"view.profile.navigation.title", nil);
    self.firstNameTextField.placeholder = NSLocalizedString(@"view.profile.textField.firstName.placeholder", nil);
    self.lastNameTextField.placeholder = NSLocalizedString(@"view.profile.textField.lastName.placeholder", nil);
    self.nickTextField.placeholder = NSLocalizedString(@"view.profile.textField.nick.placeholder", nil);
    self.emailTextField.placeholder = NSLocalizedString(@"view.profile.textField.email.placeholder", nil);
}

@end
