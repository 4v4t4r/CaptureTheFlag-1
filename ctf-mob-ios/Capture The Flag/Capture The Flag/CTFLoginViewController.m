//
//  CTFLoginViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 06.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFLoginViewController.h"
#import "CTFCredentialValidator.h"

@interface CTFLoginViewController ()

@end

@implementation CTFLoginViewController

- (void)viewDidLoad
{
    [super viewDidLoad];
    [self localizeUI];

    [self configureTapBackground];
    [self configureTextFields];
}

- (void)localizeUI
{
    self.navigationItem.title = NSLocalizedString(@"view.login.navigation.title", nil);
    _usernameTF.placeholder = NSLocalizedString(@"view.login.textField.username.placeholder", nil);
    _passwordTF.placeholder = NSLocalizedString(@"view.login.textField.password.placeholder", nil);
    [_loginBtn setTitle:NSLocalizedString(@"view.login.button.login.title", nil) forState:UIControlStateNormal];
    [_registerBtn setTitle:NSLocalizedString(@"view.login.button.register.title", nil) forState:UIControlStateNormal];
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

- (IBAction)loginPressed
{
    ValidationResult loginResult = [CTFCredentialValidator validCredential:_usernameTF.text withType:CredentialTypeUsername];
    ValidationResult passwordResult = [CTFCredentialValidator validCredential:_passwordTF.text withType:CredentialTypePassword];
    
    if (loginResult == ValidationOK && passwordResult == ValidationOK)
    {
        _statusLabel.text = NSLocalizedString(@"view.login.label.status.logged", nil);
        [self.view endEditing:YES];
    }
    else if (loginResult == ValidationEmptyField || passwordResult == ValidationEmptyField)
    {
        _statusLabel.text = NSLocalizedString(@"view.login.label.status.empty_field", nil);
    }
    else
    {
        _statusLabel.text = NSLocalizedString(@"view.login.label.status.wrong_credentials", nil);
    }
}

- (void)configureTextFields
{
    [_usernameTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
    [_passwordTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
}

- (void)textFieldDidChange
{
    BOOL enabled = _usernameTF.text.length > 0 && _passwordTF.text.length > 0;
    [_loginBtn setEnabled:enabled];
}


#pragma mark - UITextFieldDelegate
- (BOOL)textFieldShouldReturn:(UITextField *)textField
{
    [textField resignFirstResponder];
    return YES;
}

@end
