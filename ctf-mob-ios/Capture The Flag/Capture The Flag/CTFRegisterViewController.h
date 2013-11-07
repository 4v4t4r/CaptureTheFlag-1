//
//  CTFRegisterViewController.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07/11/13.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface CTFRegisterViewController : UIViewController

#pragma mark - Outlets
@property (weak) IBOutlet UITextField *emailTF;
@property (weak) IBOutlet UITextField *usernameTF;
@property (weak) IBOutlet UITextField *passwordTF;
@property (weak) IBOutlet UITextField *rePasswordTF;
@property (weak) IBOutlet UIButton *registerBtn;

- (IBAction)registerPressed;

@end
