//
//  Vibration.mm
//  https://videogamecreation.fr
//
//  Created by Benoît Freslon on 23/03/2017.
//  Copyright © 2018 Benoît Freslon. All rights reserved.
//
#import <Foundation/Foundation.h>
#import <AudioToolbox/AudioToolbox.h>
#import <UIKit/UIKit.h>

#import "Vibration.h"

#define USING_IPAD UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad

UINotificationFeedbackGenerator * notificationFeedbackGenerator = nil;
UIImpactFeedbackGenerator * lightImpact = nil;
UIImpactFeedbackGenerator * mediumImpact = nil;
UIImpactFeedbackGenerator * heavyImpact = nil;

@interface Vibration ()

@end

@implementation Vibration



//////////////////////////////////////////

#pragma mark - Vibrate

+ (BOOL) hasVibrator 
{
    return !(USING_IPAD);
}

+ (void) initFeedbacks
{
    printf("Debug: initFeedbacks");
    notificationFeedbackGenerator = [[UINotificationFeedbackGenerator alloc] init];
    
    lightImpact = [[UIImpactFeedbackGenerator alloc] initWithStyle:(UIImpactFeedbackStyleLight)];
    mediumImpact = [[UIImpactFeedbackGenerator alloc] initWithStyle:(UIImpactFeedbackStyleMedium)];
    heavyImpact = [[UIImpactFeedbackGenerator alloc] initWithStyle:(UIImpactFeedbackStyleHeavy)];
    
    [notificationFeedbackGenerator prepare];
    [lightImpact prepare];
    [mediumImpact prepare];
    [heavyImpact prepare];
    
    printf("Debug: Sucess");
}

+ (void) vibrate 
{
    AudioServicesPlaySystemSoundWithCompletion(1352, NULL);
}

+ (void) vibratePeek 
{
    printf("Debug: vibratePeek");
    AudioServicesPlaySystemSoundWithCompletion(1519, NULL); // Actuate `Peek` feedback (weak boom)
}

+ (void) vibratePop 
{
    printf("Debug: vibratePop");
    AudioServicesPlaySystemSoundWithCompletion(1520, NULL); // Actuate `Pop` feedback (strong boom)
}

+ (void) vibrateNope 
{
    printf("Debug: vibrateNope");
    AudioServicesPlaySystemSoundWithCompletion(1521, NULL); // Actuate `Nope` feedback (series of three weak booms)
}

+ (void) vibrateSuccess
{
    printf("Debug: vibrateSuccess");
    [notificationFeedbackGenerator notificationOccurred:(UINotificationFeedbackType)UINotificationFeedbackTypeSuccess];
    printf("Debug: Sucess");
}

+ (void) vibrateWarning
{
    printf("Debug: vibrateWarning");
    [notificationFeedbackGenerator notificationOccurred:(UINotificationFeedbackType)UINotificationFeedbackTypeWarning];
    printf("Debug: Sucess");
}

+ (void) vibrateFailure
{
    printf("Debug: vibrateFailure");
    [notificationFeedbackGenerator notificationOccurred:(UINotificationFeedbackType)UINotificationFeedbackTypeError];
    printf("Debug: Sucess");
}

+ (void) vibrateLight
{
    printf("Debug: vibrateLight");
    [lightImpact impactOccurred];
    printf("Debug: Sucess");
}

+ (void) vibrateMedium
{
    printf("Debug: vibrateMedium");
    [mediumImpact impactOccurred];
    printf("Debug: Sucess");
}

+ (void) vibrateHeavy
{
    printf("Debug: vibrateHeavy");
    [heavyImpact impactOccurred];
    printf("Debug: Sucess");
}

@end
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#pragma mark - "C"

extern "C" {
    
    //////////////////////////////////////////
    // Vibrate
    
    bool _HasVibrator () 
    {
        return [Vibration hasVibrator];
    }
 
    void    _Init()
    {
        [Vibration initFeedbacks];
    }

    void _Vibrate () 
    {
        [Vibration vibrate];
    }
    
    void _VibratePeek () 
    {
        [Vibration vibratePeek];
    }

    void _VibratePop () 
    {
        [Vibration vibratePop];
    }

    void _VibrateNope () 
    {
        [Vibration vibrateNope];
    }

    void _VibrateSuccess ()
    {
        [Vibration vibrateSuccess];
    }
    
    void _VibrateWarning ()
    {
        [Vibration vibrateWarning];
    }
    
    void _VibrateFailure ()
    {
        [Vibration vibrateFailure];
    }
    
    void _VibrateLight ()
    {
        [Vibration vibrateLight];
    }
    
    void _VibrateMedium ()
    {
        [Vibration vibrateMedium];
    }
    
    void _VibrateHeavy ()
    {
        [Vibration vibrateHeavy];
    }
}

