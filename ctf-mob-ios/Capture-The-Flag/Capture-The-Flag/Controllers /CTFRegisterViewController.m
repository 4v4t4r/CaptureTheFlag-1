//
//  CTFRegisterViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07/11/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFRegisterViewController.h"

#import "CTFAPIAccounts.h"
#import "CTFAPIConnection.h"
#import "CTFAPIUserDataValidator.h"
#import "CTFRegisterService.h"

@interface CTFRegisterViewController () <UIAlertViewDelegate>
@end

@implementation CTFRegisterViewController {
    UIAlertView *_successAlert;
    UIAlertView *_failureAlert;
    CTFRegisterService *_registerService;
}

#pragma mark - Lifecycle

- (void)viewDidLoad {
    [super viewDidLoad];
    [self _configureTapBackground];
    [self _configureTextFields];
}

- (void)_configureTapBackground {
    UITapGestureRecognizer *gesture = [[UITapGestureRecognizer alloc] initWithTarget:self action:@selector(_backgroundTapped)];
    [self.view addGestureRecognizer:gesture];
}

- (void)_backgroundTapped {
    [self.view endEditing:YES];
}

- (IBAction)registerPressed {
    if (!_registerService) {
        CTFAPIAccounts *accounts = [[CTFAPIAccounts alloc] initWithConnection:[CTFAPIConnection sharedConnection]];
        _registerService = [[CTFRegisterService alloc] initWithAccounts:accounts];
    }
    
    [_registerService registerWithUsername:_usernameTF.text password:_passwordTF.text rePassword:_rePasswordTF.text email:_emailTF.text responseBlock:^(RegisterState state) {
        switch (state) {
            case RegisterStateDifferentPasswords: {
                _statusLabel.text = NSLocalizedStringFromTable(@"label.status.different_password", @"Register", nil);
                break;
            }
                
            case RegisterStateInProgress: {
                [self _setUIEnabled:NO];
                [_activityIndicator startAnimating];
                break;
            }
                
            case RegisterStateSuccessful: {
                NSString *message = NSLocalizedStringFromTable(@"alert.registration.message.success", @"Register", nil);
                [self updateUIAfterRegistration];
                [self showRegisterAlertViewWithMessage:message success:YES];
                break;
            }
                
            case RegisterStateFailure: {
                NSString *message = NSLocalizedStringFromTable(@"alert.registration.message.failure", @"Register", nil);
                [self updateUIAfterRegistration];
                [self showRegisterAlertViewWithMessage:message success:NO];
                break;
            }
        }
    }];
}

- (void)showRegisterAlertViewWithMessage:(NSString *)message success:(BOOL)success {
    UIAlertView *alertView =
    [[UIAlertView alloc] initWithTitle:NSLocalizedStringFromTable(@"alert.registration.title", @"Register", nil)
                               message:message
                              delegate:Nil
                     cancelButtonTitle:NSLocalizedString(@"button.OK", nil)
                     otherButtonTitles:nil, nil];
    alertView.delegate = self;
    
    if (success) {
        _successAlert = alertView;
        [_successAlert show];
    } else {
        _failureAlert = alertView;
        [_failureAlert show];
    }
}

- (void)updateUIAfterRegistration {
    [_activityIndicator stopAnimating];
    [self _setUIEnabled:YES];
}

- (void)_setUIEnabled:(BOOL)state {
    [_emailTF setEnabled:state];
    [_usernameTF setEnabled:state];
    [_passwordTF setEnabled:state];
    [_rePasswordTF setEnabled:state];
    [_registerBtn setEnabled:state];
}

- (void)_configureTextFields {
    [_emailTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
    [_usernameTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
    [_passwordTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
    [_rePasswordTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
}

- (void)textFieldDidChange {
    BOOL registrationEnabled = NO;
    CredentialsValidationResult result =
    [CTFAPIUserDataValidator validateSignUpCredentialsWithUsername:_usernameTF.text
                                             emailAddress:_emailTF.text
                                                 password:_passwordTF.text
                                               rePassword:_rePasswordTF.text];
    
    if (result == CredentialsValidationResultOK) {
        registrationEnabled = YES;
    }
    
    [_registerBtn setEnabled:registrationEnabled];
}


#pragma mark - UITextFieldDelegate
- (BOOL)textFieldShouldReturn:(UITextField *)textField {
    [textField resignFirstResponder];
    return YES;
}


#pragma mark - UIAlertViewDelegate
- (void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex {
    if ([alertView isEqual:_successAlert] && buttonIndex == 0) {
        [self.navigationController popViewControllerAnimated:YES];
    }
}


#pragma mark - CTFViewControllerProtocol
- (void)localizeUI {
    self.navigationItem.title = NSLocalizedStringFromTable(@"navigationItem.title", @"Register", @"");
    _emailTF.placeholder = NSLocalizedStringFromTable(@"textField.email.placeholder", @"Register", @"");
    _usernameTF.placeholder = NSLocalizedStringFromTable(@"textField.username.placeholder", @"Register", @"");
    _passwordTF.placeholder = NSLocalizedStringFromTable(@"textField.password.placeholder", @"Register", @"");
    _rePasswordTF.placeholder = NSLocalizedStringFromTable(@"textField.re-password.placeholder", @"Register", @"");
    [_registerBtn setTitle:NSLocalizedStringFromTable(@"button.register.title", @"Register", @"") forState:UIControlStateNormal];
}

@end
