//
//  Vibration.h
//  https://videogamecreation.fr
//
//  Created by Benoît Freslon on 23/03/2017.
//  Copyright © 2018 Benoît Freslon. All rights reserved.
//
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

@interface Vibration : NSObject
{
    
}

//////////////////////////////////////////

#pragma mark - Vibrate

+ (BOOL) hasVibrator;
+ (void) initFeedbacks;
+ (void) vibrate;
+ (void) vibratePeek;
+ (void) vibratePop;
+ (void) vibrateNope;
+ (void) vibrateSuccess;
+ (void) vibrateWarning;
+ (void) vibrateFailure;
+ (void) vibrateLight;
+ (void) vibrateMedium;
+ (void) vibrateHeavy;

//////////////////////////////////////////


@end

