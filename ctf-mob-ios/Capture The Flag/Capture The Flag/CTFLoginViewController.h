//
//  CTFLoginViewController.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 06.11.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface CTFLoginViewController : UIViewController

#pragma mark - Outlets
@property (weak) IBOutlet UITextField *usernameTF;
@property (weak) IBOutlet UITextField *passwordTF;
@property (weak) IBOutlet UIButton *loginBtn;
@property (weak) IBOutlet UIButton *registerBtn;
@property (weak) IBOutlet UILabel *statusLabel;

-(IBAction)loginPressed;

@end
