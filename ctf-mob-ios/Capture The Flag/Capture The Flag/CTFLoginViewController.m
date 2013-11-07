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
	// Do any additional setup after loading the view, typically from a nib.
}

- (void)localizeUI
{
    self.navigationItem.title = NSLocalizedString(@"view.login.navigation.title", nil);
    _usernameTF.placeholder = NSLocalizedString(@"view.login.textField.username.placeholder", nil);
    _passwordTF.placeholder = NSLocalizedString(@"view.login.textField.password.placeholder", nil);
    [_loginBtn setTitle:NSLocalizedString(@"view.login.button.login.title", nil) forState:UIControlStateNormal];
    [_registerBtn setTitle:NSLocalizedString(@"view.login.button.register.title", nil) forState:UIControlStateNormal];
}

- (IBAction)loginPressed
{
    BOOL areFieldsValid = [self validateLoginCredentials:_usernameTF.text password:_passwordTF.text];
    if (areFieldsValid)
    {
        NSLog(@"Logged!");
    }
    else
    {
        NSLog(@"Wrong credentials");
    }
}

- (BOOL)validateLoginCredentials:(NSString *)username password:(NSString *)password
{
    BOOL validUsername = [CTFCredentialValidator validCredential:username withType:CredentialTypeUsername];
    BOOL validPassword = [CTFCredentialValidator validCredential:password withType:CredentialTypePassword];

    return (validUsername && validPassword);
}

@end
