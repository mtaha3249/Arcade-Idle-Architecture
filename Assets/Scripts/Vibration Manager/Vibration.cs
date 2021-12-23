////////////////////////////////////////////////////////////////////////////////
//  
// @author Benoît Freslon @benoitfreslon
// https://github.com/BenoitFreslon/Vibration
// https://benoitfreslon.com
// 
// Edited by Daniyal Azram 
// Added Haptic and Taptic Engine vibration presets
// - Notification Feedbacks
//   - Success, Warning, Failure
// - Impact Feedbacks
//   - Light, Medium, Heavy
//
// more info can be found here: https://developer.apple.com/design/human-interface-guidelines/ios/user-interaction/feedback/
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Runtime.InteropServices;

public class Vibration
{
    #region iOS Imports
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport ( "__Internal" )]
    private static extern void _Init ();

    [DllImport ( "__Internal" )]
    private static extern bool _HasVibrator ();

    [DllImport ( "__Internal" )]
    private static extern void _Vibrate ();

    [DllImport ( "__Internal" )]
    private static extern void _VibratePop ();

    [DllImport ( "__Internal" )]
    private static extern void _VibratePeek ();

    [DllImport ( "__Internal" )]
    private static extern void _VibrateNope ();

    [DllImport ( "__Internal" )]
    private static extern void _VibrateSuccess ();

    [DllImport ( "__Internal" )]
    private static extern void _VibrateWarning ();

    [DllImport ( "__Internal" )]
    private static extern void _VibrateFailure ();

    [DllImport ( "__Internal" )]
    private static extern void _VibrateLight ();

    [DllImport ( "__Internal" )]
    private static extern void _VibrateMedium ();

    [DllImport ( "__Internal" )]
    private static extern void _VibrateHeavy ();
#endif
    #endregion

    #region Android Imports
#if UNITY_ANDROID && !UNITY_EDITOR
    public static int defaultAmplitude;
    public static AndroidJavaClass vibrationEffectClass;
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject context;
    public static AndroidJavaObject vibrator;
#endif
    #endregion

    #region Initialization

    public static bool MuteVibration
    {
        get => PlayerPrefs.GetInt("mute_vibration") == 1;
        set => PlayerPrefs.SetInt("mute_vibration", value ? 1 : 0);
    }

    public static void Init()
    {
        UnityEngine.Debug.Log("Debug: Initing Vibrations");
#if UNITY_ANDROID && !UNITY_EDITOR

        UnityEngine.Debug.Log("Debug: SDK " + SDKVersion);
        unityPlayer = new AndroidJavaClass ( "com.unity3d.player.UnityPlayer" );
        currentActivity = unityPlayer.GetStatic<AndroidJavaObject> ( "currentActivity" );
        context = currentActivity.Call<AndroidJavaObject> ( "getApplicationContext" );
        vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

        if (SDKVersion >= 26)
        {
            vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
            defaultAmplitude = -1;

            AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context");
            string Context_VIBRATOR_SERVICE = contextClass.GetStatic<string>("VIBRATOR_SERVICE");
            vibrator = context.Call<AndroidJavaObject>("getSystemService", Context_VIBRATOR_SERVICE);
        }

#elif UNITY_IOS && !UNITY_EDITOR
        _Init();
#endif
    }

    //Keeping this method will force Unity to add vibration permissions to manifest automatically.
    public static void Vibrate()
    {
        Handheld.Vibrate();
    }
    #endregion

    #region iOS Notification Feedback Vibrations
    public static void VibrateSuccess()
    {
        if (!IsOnMobile() || MuteVibration)
            return;

#if UNITY_IOS && !UNITY_EDITOR
        _VibrateSuccess();
#elif UNITY_ANDROID && !UNITY_EDITOR
        Vibrate(80);
#endif
    }

    public static void VibrateWarning()
    {
        if (!IsOnMobile() || MuteVibration)
            return;

#if UNITY_IOS && !UNITY_EDITOR
        _VibrateWarning();
#elif UNITY_ANDROID && !UNITY_EDITOR
        Vibrate(80);
#endif

    }

    public static void VibrateFailure()
    {
        if (!IsOnMobile() || MuteVibration)
            return;

#if UNITY_IOS && !UNITY_EDITOR
        _VibrateFailure();
#elif UNITY_ANDROID && !UNITY_EDITOR
        Vibrate(80);
#endif
    }
    #endregion

    #region iOS Impact Feedback Vibrations
    public static void VibrateLight()
    {
        if (!IsOnMobile() || MuteVibration)
            return;

#if UNITY_IOS && !UNITY_EDITOR
        _VibrateLight ();
#elif UNITY_ANDROID && !UNITY_EDITOR
        UnityEngine.Debug.Log("Debug: Vibrating Light Success");
        Vibrate(30);
#endif
    }

    public static void VibrateMedium()
    {
        if (!IsOnMobile() || MuteVibration)
            return;

#if UNITY_IOS && !UNITY_EDITOR
        _VibrateMedium();
#elif UNITY_ANDROID && !UNITY_EDITOR
        Vibrate(60);
#endif
    }

    public static void VibrateHeavy()
    {
        if (!IsOnMobile() || MuteVibration)
            return;

#if UNITY_IOS && !UNITY_EDITOR
        _VibrateHeavy();
#elif UNITY_ANDROID && !UNITY_EDITOR
        Vibrate(80);
#endif

    }
    #endregion

    #region Taptic Engine Vibrations
    public static void VibratePeek()
    {
        if (!IsOnMobile() || MuteVibration) return;

#if UNITY_IOS && !UNITY_EDITOR
        _VibratePeek ();
#elif UNITY_ANDROID && !UNITY_EDITOR
         Vibrate(40);
#endif
    }

    public static void VibratePop()
    {
        if (!IsOnMobile() || MuteVibration) return;

#if UNITY_IOS && !UNITY_EDITOR
        _VibratePop();
#elif UNITY_ANDROID && !UNITY_EDITOR
        Vibrate(80);
#endif
    }

    public static void VibrateNope()
    {
        if (!IsOnMobile() || MuteVibration) return;

#if UNITY_IOS && !UNITY_EDITOR
        _VibrateNope ();
#elif UNITY_ANDROID && !UNITY_EDITOR
        Vibrate(80);
#endif
    }
    #endregion

    #region Older Android Vibrations
    public static void OldVibrate(long milliseconds)
    {
        if (!IsOnMobile() || MuteVibration) return;

#if UNITY_ANDROID && !UNITY_EDITOR
        vibrator.Call ( "vibrate", milliseconds );
#endif
    }

    public static void OldVibrate(long[] pattern, int repeat)
    {
        if (!IsOnMobile() || MuteVibration)
            return;

#if UNITY_ANDROID && !UNITY_EDITOR
        vibrator.Call ( "vibrate", pattern, repeat );
#endif
    }

    public static bool HasVibrator()
    {
        if (!IsOnMobile() || MuteVibration)
            return false;

#if UNITY_ANDROID && !UNITY_EDITOR
        return vibrator.Call<bool>("hasVibrator");    

#elif UNITY_IOS && !UNITY_EDITOR
        return _HasVibrator ();
#endif
        return false;
    }

    public static void Cancel()
    {
        if (!IsOnMobile())
            return;

#if UNITY_ANDROID && !UNITY_EDITOR
        vibrator.Call ( "cancel" );
#endif
    }
    #endregion

    #region Android SDK 26+ Vibrations
#if UNITY_ANDROID && !UNITY_EDITOR
    public static void Vibrate(long milliseconds)
    {
        if(MuteVibration)
            return;

        if (SDKVersion >= 26)
        {
            Vibrate(milliseconds, defaultAmplitude);
        }
        else
        {
            UnityEngine.Debug.Log("Debug: Old Vibrate()");
            OldVibrate(milliseconds);
        }
    }

    public static void Vibrate(long milliseconds, int amplitude)
    {

        if(MuteVibration)
            return;    

        if (SDKVersion >= 26)
        {
             UnityEngine.Debug.Log("Debug: Creating new vibration effect with time" + milliseconds);
             CreateVibrationEffect("createOneShot", new object[] { milliseconds, amplitude });
        }
        else
        {
            OldVibrate(milliseconds);
        }
    }

    private static void CreateVibrationEffect(string function, params object[] args)
    {
        AndroidJavaObject vibrationEffect = vibrationEffectClass.CallStatic<AndroidJavaObject>(function, args);
        vibrator.Call("vibrate", vibrationEffect);
        UnityEngine.Debug.Log("Debug: Called vibrater");
    }
#endif
    #endregion

    #region Helpers
    private static bool IsOnMobile()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            return true;

        return false;
    }

    private static bool Android
    {
        get
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return true;
#else
            return false;
#endif
        }
    }

    private static bool IOS
    {
        get
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return true;
#else
            return false;
#endif
        }
    }

    private static int SDKVersion
    {
        get
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
                return version.GetStatic<int>("SDK_INT");
#else
            return -1;
#endif
        }
    }

    private static bool HasAmplitudeControl
    {
        get
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (SDKVersion > 26)
                return vibrator.Call<bool>("hasAmplitudeControl");
            else
                return false;
#else
            return false;
#endif
        }
    }

    #endregion
}