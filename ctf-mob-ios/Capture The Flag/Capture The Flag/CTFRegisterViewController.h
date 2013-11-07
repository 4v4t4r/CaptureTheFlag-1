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
@property (weak, nonatomic) IBOutlet UITextField *emailTF;
@property (weak, nonatomic) IBOutlet UITextField *usernameTF;
@property (weak, nonatomic) IBOutlet UITextField *passwordTF;
@property (weak, nonatomic) IBOutlet UITextField *rePasswordTF;
@property (weak, nonatomic) IBOutlet UIButton *registerBtn;
@property (weak, nonatomic) IBOutlet UILabel *statusLabel;

- (IBAction)registerPressed;

@end
