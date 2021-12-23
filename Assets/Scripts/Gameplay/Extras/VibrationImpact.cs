using UnityEngine;

public class VibrationImpact : MonoBehaviour, IGenericCallback
{
    public enum VibrationType
    {
        Light,
        Medium,
        Heavy,
        Failure,
        Success,
        Warning
    }

    public VibrationType _vibrationType;
    
    public void OnEventRaisedCallback(params object[] param)
    {
        switch (_vibrationType)
        {
            case VibrationType.Light:
                Vibration.VibrateLight();
                break;
            case VibrationType.Medium:
                Vibration.VibrateMedium();
                break;
            case VibrationType.Heavy:
                Vibration.VibrateHeavy();
                break;
            case VibrationType.Failure:
                Vibration.VibrateFailure();
                break;
            case VibrationType.Success:
                Vibration.VibrateSuccess();
                break;
            case VibrationType.Warning:
                Vibration.VibrateWarning();
                break;
        }
    }
}
