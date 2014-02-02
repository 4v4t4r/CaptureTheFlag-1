//
//  CTFLoginViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 06.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFLoginViewController.h"

#import "CTFAPIAccounts.h"
#import "CTFAPIConnection.h"
#import "CTFAPILocalCredentials.h"
#import "CTFAPILocalCredentialsStore.h"
#import "CTFAPIUserDataValidator.h"
#import "CoreDataService.h"
#import "CTFLoginService.h"

@interface CTFLoginViewController ()

@property (weak, nonatomic) IBOutlet UITextField *usernameTF;
@property (weak, nonatomic) IBOutlet UITextField *passwordTF;
@property (weak, nonatomic) IBOutlet UIButton *loginBtn;
@property (weak, nonatomic) IBOutlet UIButton *registerBtn;
@property (weak, nonatomic) IBOutlet UILabel *statusLabel;
@property (weak, nonatomic) IBOutlet UIActivityIndicatorView *activityIndicator;

-(IBAction)loginPressed;

@end

@implementation CTFLoginViewController {
    CTFLoginService *_loginService;
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

- (IBAction)loginPressed {
    if (!_loginService) {
        CTFAPIAccounts *accounts = [[CTFAPIAccounts alloc] initWithConnection:[CTFAPIConnection sharedConnection]];
        _loginService = [[CTFLoginService alloc] initWithAccounts:accounts];
    }
    
    [_loginService logInWithUsername:_usernameTF.text password:_passwordTF.text responseBlock:^(LoginState state) {
        switch (state) {
            case LoginStateCredentialsNotValid: {
                _statusLabel.text = NSLocalizedStringFromTable(@"label.status.wrong_credentials", @"Login", @"");
                break;
            }
                
            case LoginStateInProgress: {
                [_activityIndicator startAnimating];
                [self.view endEditing:YES];

                break;
            }
                
            case LoginStateSuccessful: {
                [_activityIndicator stopAnimating];
                _statusLabel.text = NSLocalizedStringFromTable(@"label.status.logged", @"Login", @"");
                
                UIStoryboard *storyboard = [UIStoryboard storyboardWithName:@"Main" bundle:nil];
                UINavigationController *mainNavigationController = [storyboard instantiateViewControllerWithIdentifier:NSStringFromClass([UINavigationController class])];
                [self presentViewController:mainNavigationController animated:YES completion:^{
                    _usernameTF.text = @"";
                    _passwordTF.text = @"";
                    _statusLabel.text = @"";
                }];
                break;
            }
                
            case LoginStateFailure: {
                [_activityIndicator stopAnimating];
                UIAlertView *alert =
                [[UIAlertView alloc] initWithTitle:NSLocalizedStringFromTable(@"alert.wrong_credentials.title", @"Login", @"")
                                           message:NSLocalizedStringFromTable(@"alert.wrong_credentials.message", @"Login", @"")
                                          delegate:nil
                                 cancelButtonTitle:NSLocalizedString(@"button.OK", nil)
                                 otherButtonTitles:nil, nil];
                [alert show];
                break;
            }
        }
    }];
}

- (void)_fillTextFieldIfNecessary {
    CTFAPILocalCredentials *credentials = [[CTFAPILocalCredentialsStore sharedInstance] getCredentials];
    if (credentials) {
        [_usernameTF setText:credentials.username];
        [_passwordTF setText:credentials.password];
    }
    [self textFieldDidChange];
}

- (void)_configureTextFields {
    [_usernameTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
    [_passwordTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
}

- (void)textFieldDidChange {
    CredentialsValidationResult result =
    [CTFAPIUserDataValidator validateSignInCredentialsWithUsername:_usernameTF.text password:_passwordTF.text];
    
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
    self.navigationItem.title = NSLocalizedStringFromTable(@"navigation.title", @"Login", @"");
    _usernameTF.placeholder = NSLocalizedStringFromTable(@"textField.username.placeholder", @"Login", @"");
    _passwordTF.placeholder = NSLocalizedStringFromTable(@"textField.password.placeholder", @"Login", @"");
    [_loginBtn setTitle:NSLocalizedStringFromTable(@"button.login.title", @"Login", @"") forState:UIControlStateNormal];
    [_registerBtn setTitle:NSLocalizedStringFromTable(@"button.register.title", @"Login", @"") forState:UIControlStateNormal];
}

@end
