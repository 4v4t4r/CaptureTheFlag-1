//
//  CTFLoginViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 06.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFLoginViewController.h"

#import "CTFSession.h"
#import "CTFUser.h"

#import "CTFAPILocalCredentialsValidator.h"
#import "CTFAPIAccounts.h"
#import "CTFAPIConnection.h"

#import "CTFAPILocalCredentialsStore.h"
#import "CTFAPILocalCredentials.h"

@interface CTFLoginViewController ()

@end

@implementation CTFLoginViewController {
    CTFAPIAccounts *_accounts;
}

- (void)viewDidLoad
{
    [super viewDidLoad];

    [self _configureTapBackground];
    [self _configureTextFields];
}

- (void)viewWillAppear:(BOOL)animated {
    [super viewWillAppear:animated];
    [self _fillTextFieldIfNecessary];
}

- (void)_configureTapBackground {
    UITapGestureRecognizer *gesture = [[UITapGestureRecognizer alloc] initWithTarget:self action:@selector(_backgroundTapped)];
    [self.view addGestureRecognizer:gesture];
}

- (void)_backgroundTapped {
    [self.view endEditing:YES];
}

- (IBAction)loginPressed
{
    NSString *username = _usernameTF.text;
    NSString *password = _passwordTF.text;
    
    CredentialsValidationResult result =
    [CTFAPILocalCredentialsValidator validateSignInCredentialsWithUsername:username password:password];
    
    if (result == CredentialsValidationResultOK) {
        [_activityIndicator startAnimating];
        [self.view endEditing:YES];
        
        /// If successfuly logged to the server token will be provide in response
        _accounts = [[CTFAPIAccounts alloc] initWithConnection:[CTFAPIConnection sharedConnection]];
        [_accounts signInWithUsername:username andPassword:password withBlock:^(NSString *token) {
            
            if (token) {
                [_activityIndicator stopAnimating];
                _statusLabel.text = NSLocalizedString(@"view.login.label.status.logged", nil);

                /// Configure game object with token and logged user
                CTFSession *session = [[CTFSession alloc] initWithToken:token];
            
                CTFUser *user = [CTFUser createObject];
                user.username = username;
                session.currentUser = user;
                
                [CTFSession setSharedInstance:session];
                
                /// Store login and password in the Keychain
                CTFAPILocalCredentials *credentials = [[CTFAPILocalCredentials alloc] initWithUsername:username password:password];
                BOOL stored = [[CTFAPILocalCredentialsStore sharedInstance] storeCredentials:credentials];
                
                if (stored) {
                    /// Create new view and show
                    UIStoryboard *storyboard = [UIStoryboard storyboardWithName:@"Main" bundle:nil];
                    UINavigationController *mainNavigationController = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([UINavigationController class])];
                    [self presentViewController:mainNavigationController animated:YES completion:^{
                        _usernameTF.text = @"";
                        _passwordTF.text = @"";
                        _statusLabel.text = @"";
                    }];
                } else {
                    UIAlertView *alertView =
                    [[UIAlertView alloc] initWithTitle:NSLocalizedString(@"view.login.alert.cant_store_credentials.title", nil)
                                               message:NSLocalizedString(@"view.login.alert.cant_store_credentials.message", nil)
                                              delegate:Nil
                                     cancelButtonTitle:NSLocalizedString(@"button.OK", nil)
                                     otherButtonTitles:nil, nil];
                    [alertView show];
                }
            } else {
                [_activityIndicator stopAnimating];
#warning [tsu] need implementation of UIAlertView which shows appropriate alert that user can't login... Need some error handling
            }
        }];
    } else {
        _statusLabel.text = NSLocalizedString(@"view.login.label.status.wrong_credentials", nil);
    }
}

- (void)_configureTextFields {
    [_usernameTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
    [_passwordTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
}

- (void)_fillTextFieldIfNecessary {
    CTFAPILocalCredentials *credentials = [[CTFAPILocalCredentialsStore sharedInstance] getCredentials];
    if (credentials) {
        [_usernameTF setText:credentials.username];
        [_passwordTF setText:credentials.password];
    }
    [self textFieldDidChange];
}

- (void)textFieldDidChange {
    CredentialsValidationResult result =
    [CTFAPILocalCredentialsValidator validateSignInCredentialsWithUsername:_usernameTF.text password:_passwordTF.text];
    
    [_loginBtn setEnabled:(result == CredentialsValidationResultOK)];
}


#pragma mark - UITextFieldDelegate
- (BOOL)textFieldShouldReturn:(UITextField *)textField {
    [textField resignFirstResponder];
    return YES;
}


#pragma mark - Segues
- (BOOL)shouldPerformSegueWithIdentifier:(NSString *)identifier sender:(id)sender {
    if ([identifier isEqualToString:@"ToRegisterSegue"])
        return YES;
    return NO;
}


#pragma mark - CTFViewControllerProtocol
- (void)localizeUI {
    self.navigationItem.title = NSLocalizedString(@"view.login.navigation.title", nil);
    _usernameTF.placeholder = NSLocalizedString(@"view.login.textField.username.placeholder", nil);
    _passwordTF.placeholder = NSLocalizedString(@"view.login.textField.password.placeholder", nil);
    [_loginBtn setTitle:NSLocalizedString(@"view.login.button.login.title", nil) forState:UIControlStateNormal];
    [_registerBtn setTitle:NSLocalizedString(@"view.login.button.register.title", nil) forState:UIControlStateNormal];
}

@end
