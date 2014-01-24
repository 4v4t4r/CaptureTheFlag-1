//
//  CTFRegisterViewController.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07/11/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFViewController.h"

@interface CTFRegisterViewController : CTFViewController

#pragma mark - Outlets
@property (weak, nonatomic) IBOutlet UITextField *emailTF;
@property (weak, nonatomic) IBOutlet UITextField *usernameTF;
@property (weak, nonatomic) IBOutlet UITextField *passwordTF;
@property (weak, nonatomic) IBOutlet UITextField *rePasswordTF;
@property (weak, nonatomic) IBOutlet UIButton *registerBtn;
@property (weak, nonatomic) IBOutlet UILabel *statusLabel;
@property (weak, nonatomic) IBOutlet UIActivityIndicatorView *activityIndicator;

- (IBAction)registerPressed;

@end
