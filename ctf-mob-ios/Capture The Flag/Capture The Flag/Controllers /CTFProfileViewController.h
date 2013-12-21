//
//  CTFProfileViewController.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 17.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import "CTFViewController.h"

@interface CTFProfileViewController : CTFViewController

@property (weak, nonatomic) IBOutlet UITextField *firstNameTextField;
@property (weak, nonatomic) IBOutlet UITextField *lastNameTextField;
@property (weak, nonatomic) IBOutlet UITextField *nickTextField;
@property (weak, nonatomic) IBOutlet UITextField *emailTextField;
@property (weak, nonatomic) IBOutlet UIBarButtonItem *updateButton;

- (IBAction)updateProfile:(id)sender;

@end
