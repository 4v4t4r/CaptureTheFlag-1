//
//  CTFRegisterViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07/11/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFRegisterViewController.h"
#import "CTFAPICredentials.h"

@implementation CTFRegisterViewController

- (void)viewDidLoad
{
    [super viewDidLoad];
    [self localizeUI];
    [self configureTapBackground];
    [self configureTextFields];
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

- (void)configureTapBackground
{
    UITapGestureRecognizer *gesture = [[UITapGestureRecognizer alloc] initWithTarget:self action:@selector(backgroundTapped)];
    [self.view addGestureRecognizer:gesture];
}

- (void)backgroundTapped
{
    [self.view endEditing:YES];
}

- (IBAction)registerPressed
{
    if (![_passwordTF.text isEqualToString:_rePasswordTF.text])
    {
        _statusLabel.text = NSLocalizedString(@"view.register.label.status.different_password", nil);
        return;
    }
#warning Implement rest of the registration path
}

- (void)configureTextFields
{
    [_emailTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
    [_usernameTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
    [_passwordTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
    [_rePasswordTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
}

- (void)textFieldDidChange
{
    BOOL registrationEnabled = NO;
    CredentialsValidationResult result =
    [CTFAPICredentials validateSignUpCredentialsWithUsername:_usernameTF.text
                                             emailAddress:_emailTF.text
                                                 password:_passwordTF.text
                                               rePassword:_rePasswordTF.text];
    
    if (result == CredentialsValidationResultOK) {
        registrationEnabled = YES;
    }
    
    [_registerBtn setEnabled:registrationEnabled];
}

#pragma mark - UITextFieldDelegate
- (BOOL)textFieldShouldReturn:(UITextField *)textField
{
    [textField resignFirstResponder];
    return YES;
}

@end
