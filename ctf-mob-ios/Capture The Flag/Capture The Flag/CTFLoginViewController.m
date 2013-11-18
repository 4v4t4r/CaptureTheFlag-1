//
//  CTFLoginViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 06.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFLoginViewController.h"
#import "CTFCredentialsValidator.h"
#import "CTFUser.h"

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
    ValidationResult loginResult = [CTFCredentialsValidator validCredential:_usernameTF.text withType:CredentialTypeUsername];
    ValidationResult passwordResult = [CTFCredentialsValidator validCredential:_passwordTF.text withType:CredentialTypePassword];
    
    if (loginResult == ValidationOK && passwordResult == ValidationOK)
    {
        _statusLabel.text = NSLocalizedString(@"view.login.label.status.logged", nil);
        [self.view endEditing:YES];
        
        /// Here will be post request.
        
        if (1) /// If successfuly logged to the server token will be provide in response
        {
            CTFUser *user = [CTFUser createObject];
            user.login = _usernameTF.text;
            user.token = @"token_from_server";
            
            /// Create new view and show
            UIStoryboard *storyboard = [UIStoryboard storyboardWithName:@"Main" bundle:nil];
            UINavigationController *mainNavigationController = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([UINavigationController class])];
            [self presentViewController:mainNavigationController animated:YES completion:^{
                _usernameTF.text = @"";
                _passwordTF.text = @"";
                _statusLabel.text = @"";
            }];
        }
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
    ValidationResult usernameResult = [CTFCredentialsValidator validCredential:_usernameTF.text withType:CredentialTypeUsername];
    ValidationResult passwordResult = [CTFCredentialsValidator validCredential:_passwordTF.text withType:CredentialTypePassword];
    
    BOOL loginEnabled = NO;
    if (usernameResult == ValidationOK && passwordResult == ValidationOK)
        loginEnabled = YES;
    
    [_loginBtn setEnabled:loginEnabled];
}


#pragma mark - UITextFieldDelegate
- (BOOL)textFieldShouldReturn:(UITextField *)textField
{
    [textField resignFirstResponder];
    return YES;
}

@end
