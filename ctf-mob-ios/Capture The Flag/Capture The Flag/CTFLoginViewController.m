//
//  CTFLoginViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 06.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFLoginViewController.h"
#import "CTFAPICredentials.h"
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
    CredentialsValidationResult result =
    [CTFAPICredentials validateSignInCredentialsWithUsername:_usernameTF.text password:_passwordTF.text];
    
    if (result == CredentialsValidationResultOK) {
        _statusLabel.text = NSLocalizedString(@"view.login.label.status.logged", nil);
        [self.view endEditing:YES];
        
        /// Here will be post request.
        
        if (1) /// If successfuly logged to the server token will be provide in response
        {
            CTFUser *user = [CTFUser createObject];
            user.username = _usernameTF.text;
            BOOL result = [user loginUser];
            if (result) {
                /// Create new view and show
                UIStoryboard *storyboard = [UIStoryboard storyboardWithName:@"Main" bundle:nil];
                UINavigationController *mainNavigationController = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([UINavigationController class])];
                [self presentViewController:mainNavigationController animated:YES completion:^{
                    _usernameTF.text = @"";
                    _passwordTF.text = @"";
                    _statusLabel.text = @"";
                }];
            } else {
                /// We have check if this situation is possible in the future
                NSLog(@"Something really goes wrong. User is still logged in");
            }
        }
    } else
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
    CredentialsValidationResult result =
    [CTFAPICredentials validateSignInCredentialsWithUsername:_usernameTF.text password:_passwordTF.text];
    
    [_loginBtn setEnabled:(result == CredentialsValidationResultOK)];
}


#pragma mark - UITextFieldDelegate
- (BOOL)textFieldShouldReturn:(UITextField *)textField
{
    [textField resignFirstResponder];
    return YES;
}


#pragma mark - Segues
- (BOOL)shouldPerformSegueWithIdentifier:(NSString *)identifier sender:(id)sender
{
    if ([identifier isEqualToString:@"ToRegisterSegue"])
        return YES;
    return NO;
}

@end
