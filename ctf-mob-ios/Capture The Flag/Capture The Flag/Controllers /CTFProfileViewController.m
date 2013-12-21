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
    self.navigationItem.title = NSLocalizedStringFromTable(@"navigationItem.title", @"Profile", nil);
    self.firstNameTextField.placeholder = NSLocalizedStringFromTable(@"textField.firstName.placeholder", @"Profile", nil);
    self.lastNameTextField.placeholder = NSLocalizedStringFromTable(@"textField.lastName.placeholder", @"Profile", nil);
    self.nickTextField.placeholder = NSLocalizedStringFromTable(@"textField.nick.placeholder", @"Profile", nil);
    self.emailTextField.placeholder = NSLocalizedStringFromTable(@"textField.email.placeholder", @"Profile", nil);
    self.updateButton.title = NSLocalizedStringFromTable(@"button.update.title", @"Profile", nil);
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
