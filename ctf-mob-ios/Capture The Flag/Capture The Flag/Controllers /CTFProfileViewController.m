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
#import "CTFUser.h"

@implementation CTFProfileViewController {
    CTFAPIAccounts *_accounts;
    CTFUser *_user;
}

- (void)viewDidLoad
{
    [super viewDidLoad];
    _accounts = [[CTFAPIAccounts alloc] initWithConnection:[CTFAPIConnection sharedConnection]];
    [_accounts accountInfoForToken:[CTFSession sharedInstance].token block:^(CTFUser *user) {
        if (user) {
            _user = user;
            self.firstNameTextField.text = user.firstName;
            self.lastNameTextField.text = user.lastName;
            self.nickTextField.text = user.nick ? : user.username;
            self.emailTextField.text = user.email;
        }
    }];
}

- (void)localizeUI {
    self.navigationItem.title = NSLocalizedString(@"view.profile.navigation.title", nil);
    self.firstNameTextField.placeholder = NSLocalizedString(@"view.profile.textField.firstName.placeholder", nil);
    self.lastNameTextField.placeholder = NSLocalizedString(@"view.profile.textField.lastName.placeholder", nil);
    self.nickTextField.placeholder = NSLocalizedString(@"view.profile.textField.nick.placeholder", nil);
    self.emailTextField.placeholder = NSLocalizedString(@"view.profile.textField.email.placeholder", nil);
}

- (IBAction)updateProfile:(id)sender {
    [self _prepareUserToUpdate:_user];
    [_accounts updateInfoForUser:_user block:^(BOOL success) {
        NSLog(@"success = %d", success);
    }];
}

- (void)_prepareUserToUpdate:(CTFUser *)user {
    _user.firstName = self.firstNameTextField.text;
    _user.lastName = self.lastNameTextField.text;
    _user.nick = self.nickTextField.text;
    _user.email = self.emailTextField.text;
}

@end
