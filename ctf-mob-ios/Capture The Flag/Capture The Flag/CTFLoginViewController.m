//
//  CTFLoginViewController.m
//  Capture The Flag
//
//  Created by Tomasz Szulc on 06.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFLoginViewController.h"
#import "CTFGame.h"
#import "CTFUser.h"

#import "CTFAPICredentials.h"
#import "CTFAPIAccounts.h"
#import "CTFAPIConnection.h"
#import "CTFAPIMappings.h"

#import "CTFLocalCredentialsStore.h"
#import "CTFLocalCredentials.h"

@interface CTFLoginViewController ()

@end

@implementation CTFLoginViewController {
    CTFAPIAccounts *_accounts;
}

- (void)viewDidLoad
{
    [super viewDidLoad];
    [self localizeUI];

    [self configureTapBackground];
    [self configureTextFields];
//    [self getUserFromServer_test];
}

#pragma mark [tsu] only temporary for check if mapping is correctly. Remove it later. Something is wrong in tests and I need to check it here. Issue reported on RestKit github
- (void)getUserFromServer_test {
    CTFAPIConnection *connection = [CTFAPIConnection sharedConnection];
    NSLog(@"managedObjectStore = %@", connection.manager.managedObjectStore);
    
    RKEntityMapping *userMapping = [[CTFAPIMappings sharedInstance] userMapping];
    
    RKResponseDescriptor *descriptor = [RKResponseDescriptor responseDescriptorWithMapping:userMapping method:RKRequestMethodGET pathPattern:@"test" keyPath:nil statusCodes:RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful)];
    [connection.manager addResponseDescriptor:descriptor];

    [connection.manager getObject:nil path:@"test" parameters:Nil success:^(RKObjectRequestOperation *operation, RKMappingResult *mappingResult) {
        NSLog(@"success, %@", [mappingResult.firstObject class]);
    } failure:^(RKObjectRequestOperation *operation, NSError *error) {
        NSLog(@"failure");
    }];
}

- (void)viewWillAppear:(BOOL)animated {
    [super viewWillAppear:animated];
    [self fillTextFieldIfNecessary];
}

- (void)localizeUI {
    self.navigationItem.title = NSLocalizedString(@"view.login.navigation.title", nil);
    _usernameTF.placeholder = NSLocalizedString(@"view.login.textField.username.placeholder", nil);
    _passwordTF.placeholder = NSLocalizedString(@"view.login.textField.password.placeholder", nil);
    [_loginBtn setTitle:NSLocalizedString(@"view.login.button.login.title", nil) forState:UIControlStateNormal];
    [_registerBtn setTitle:NSLocalizedString(@"view.login.button.register.title", nil) forState:UIControlStateNormal];
}

- (void)configureTapBackground {
    UITapGestureRecognizer *gesture = [[UITapGestureRecognizer alloc] initWithTarget:self action:@selector(backgroundTapped)];
    [self.view addGestureRecognizer:gesture];
}

- (void)backgroundTapped {
    [self.view endEditing:YES];
}

- (IBAction)loginPressed
{
    NSString *username = _usernameTF.text;
    NSString *password = _passwordTF.text;
    
    CredentialsValidationResult result =
    [CTFAPICredentials validateSignInCredentialsWithUsername:username password:password];
    
    if (result == CredentialsValidationResultOK) {
        _statusLabel.text = NSLocalizedString(@"view.login.label.status.logged", nil);
        [self.view endEditing:YES];
        
        /// If successfuly logged to the server token will be provide in response
        _accounts = [[CTFAPIAccounts alloc] initWithConnection:[CTFAPIConnection sharedConnection]];
        [_accounts signInWithUsername:username andPassword:password withBlock:^(NSString *token) {
            
            if (token) {
                /// Configure game object with token and logged user
                CTFGame *game = [[CTFGame alloc] initWithToken:token];
            
                CTFUser *user = [CTFUser createObject];
                user.username = username;
                game.currentUser = user;
                
                [CTFGame setSharedInstance:game];
                
                /// Store login and password in the Keychain
                CTFLocalCredentials *credentials = [[CTFLocalCredentials alloc] initWithUsername:username password:password];
                BOOL stored = [[CTFLocalCredentialsStore sharedInstance] storeCredentials:credentials];
                
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
#warning [tsu] need implementation of UIAlertView which shows appropriate alert that user can't login... Need some error handling
            }
        }];
    } else
    {
        _statusLabel.text = NSLocalizedString(@"view.login.label.status.wrong_credentials", nil);
    }
}

- (void)configureTextFields {
    [_usernameTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
    [_passwordTF addTarget:self action:@selector(textFieldDidChange) forControlEvents:UIControlEventEditingChanged];
}

- (void)fillTextFieldIfNecessary {
    CTFLocalCredentials *credentials = [[CTFLocalCredentialsStore sharedInstance] getCredentials];
    if (credentials) {
        [_usernameTF setText:credentials.username];
        [_passwordTF setText:credentials.password];
    }
    [self textFieldDidChange];
}

- (void)textFieldDidChange {
    CredentialsValidationResult result =
    [CTFAPICredentials validateSignInCredentialsWithUsername:_usernameTF.text password:_passwordTF.text];
    
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

@end
