using UnityEngine;

[CreateAssetMenu(fileName = "Follow Rotator", menuName = "Utilities/Camera/Follow Rotator", order = 0)]
public class FollowRotatorCamera : BaseCameraState
{
    [SerializeField][Multiline]
    private string developerNote = "Follow Target and Look At given offset by offset";
    [SerializeField] [Range(0.1f, 5)]
    float followSpeed = 4;

    [SerializeField] [Range(0, 50)] float rotateSpeed = 1;
    [SerializeField] [Range(0, 50)] float angleLerpSpeed = 10;
    [SerializeField] Vector3 rotateOffset;

    Vector3 offsetLerping;
    float xValue = 0;

    public override void UpdateCamera(Transform target, Transform camera, Transform pivot, float deltaTime)
    {
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, posOffset, followSpeed * deltaTime);
        camera.position = Vector3.Lerp(camera.position, target.position, followSpeed * deltaTime);
        xValue = Mathf.Lerp(camera.localEulerAngles.x, rotateOffset.x, angleLerpSpeed * deltaTime);
        offsetLerping = new Vector3(xValue, rotateOffset.y, rotateOffset.z);
        camera.ObjectRotator(rotateSpeed, Utility.Axis.Y, offsetLerping);
        UpdateFOV(camera);
    }
}