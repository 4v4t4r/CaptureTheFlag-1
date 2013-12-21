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

@interface CTFProfileViewController () <UITextFieldDelegate>

@end

@implementation CTFProfileViewController {
    CTFAPIAccounts *_accounts;
    CTFUser *_user;
}

- (void)_configureTextFields {
    SEL action = @selector(testFieldDidChange);
    UIControlEvents events = UIControlEventEditingChanged;
    
    [_firstNameTextField addTarget:self action:action forControlEvents:events];
    [_lastNameTextField addTarget:self action:action forControlEvents:events];
    [_nickTextField addTarget:self action:action forControlEvents:events];
    [_emailTextField addTarget:self action:action forControlEvents:events];
}

- (void)testFieldDidChange {
//    CredentialsValidationResult result =
//    [CTFAPILocalCredentialsValidator validateSignInCredentialsWithUsername:_usernameTF.text password:_passwordTF.text];
//    
//    [_loginBtn setEnabled:(result == CredentialsValidationResultOK)];
}

- (void)viewDidLoad {
    [super viewDidLoad];
    [self _configureTextFields];
    _accounts = [[CTFAPIAccounts alloc] initWithConnection:[CTFAPIConnection sharedConnection]];
    [_accounts accountInfoForToken:[CTFSession sharedInstance].token block:^(CTFUser *user) {
        if (user) {
            _user = user;
            _firstNameTextField.text = user.firstName;
            _lastNameTextField.text = user.lastName;
            _nickTextField.text = user.nick ? : user.username;
            _emailTextField.text = user.email;
        }
    }];
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


#pragma mark - CTFViewControllerProtocol
- (void)localizeUI {
    self.navigationItem.title = NSLocalizedStringFromTable(@"navigationItem.title", @"Profile", nil);
    _firstNameTextField.placeholder = NSLocalizedStringFromTable(@"textField.firstName.placeholder", @"Profile", nil);
    _lastNameTextField.placeholder = NSLocalizedStringFromTable(@"textField.lastName.placeholder", @"Profile", nil);
    _nickTextField.placeholder = NSLocalizedStringFromTable(@"textField.nick.placeholder", @"Profile", nil);
    _emailTextField.placeholder = NSLocalizedStringFromTable(@"textField.email.placeholder", @"Profile", nil);
    _updateButton.title = NSLocalizedStringFromTable(@"button.update.title", @"Profile", nil);
}


#pragma mark - UITextFieldDelegate

@end
