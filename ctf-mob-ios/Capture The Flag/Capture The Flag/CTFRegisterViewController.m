//
//  CTFRegisterViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07/11/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFRegisterViewController.h"
#import "CTFCredentialValidator.h"

@implementation CTFRegisterViewController

- (void)viewDidLoad
{
    [super viewDidLoad];
    [self localizeUI];
    [self configureTapBackground];
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
    ValidationResult emailResult = [CTFCredentialValidator validCredential:_emailTF.text withType:CredentialTypeEmail];
    ValidationResult usernameResult = [CTFCredentialValidator validCredential:_usernameTF.text withType:CredentialTypeUsername];
    
    ValidationResult passwordResult = ValidationWrongCredentials;
    if ([_passwordTF.text isEqualToString:_rePasswordTF.text])
        passwordResult = [CTFCredentialValidator validCredential:_passwordTF.text withType:CredentialTypePassword];
    else
    {
        _statusLabel.text = NSLocalizedString(@"view.register.label.status.different_password", nil);
        return;
    }
    
    if (emailResult == ValidationOK &&
        usernameResult == ValidationOK &&
        passwordResult == ValidationOK)
    {
        _statusLabel.text = NSLocalizedString(@"view.register.label.status.registered", nil);
        [self.view endEditing:YES];
    }
    else if (emailResult == ValidationEmptyField ||
             usernameResult == ValidationEmptyField ||
             passwordResult == ValidationEmptyField)
    {
        _statusLabel.text = NSLocalizedString(@"view.register.label.status.empty_field", nil);
    }
    else if (emailResult == ValidationWrongCredentials ||
             usernameResult == ValidationWrongCredentials ||
             passwordResult == ValidationWrongCredentials)
    {
        _statusLabel.text = NSLocalizedString(@"view.register.label.status.wrong_credentials", nil);
    }
}

#pragma mark - UITextFieldDelegate
- (BOOL)textFieldShouldReturn:(UITextField *)textField
{
    [textField resignFirstResponder];
    return YES;
}

@end
