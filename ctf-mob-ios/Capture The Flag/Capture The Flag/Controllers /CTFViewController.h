//
//  CTFViewController.h
//  Capture The Flag
//
//  Created by Tomasz Szulc on 07.12.2013.
//  Copyright (c) 2013 Tomasz Szulc. All rights reserved.
//

#import <UIKit/UIKit.h>

@protocol CTFViewControllerProtocol <NSObject>
@optional
/// Method called in viewDidLoad of CTFViewController. Override this method if you want to localize UI in controller.
- (void)localizeUI;
@end

@interface CTFViewController : UIViewController <CTFViewControllerProtocol>

@end
