using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Follow Camera Pro Axis", menuName = "Utilities/Camera/Follow Camera Pro Axis", order = 0)]
public class FollowCameraProAxis : BaseCameraState
{
    [Flags]
    public enum AxisMask
    {
        X = 1,
        Y = 2,
        Z = 4
    }

    [SerializeField][Multiline]
    private string developerNote = "Follow Target and Look At given offset by offset";
    public AxisMask _axis;
    [Range(0.1f, 5)] public float followSpeed = 4;

    [Range(0.1f, 30)] public float lookSpeed = 10;

    public override void UpdateCamera(Transform target, Transform camera, Transform pivot, float deltaTime)
    {
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, posOffset, followSpeed * deltaTime);
        var cameraPosition = camera.position;
        if ((_axis & AxisMask.X) == AxisMask.X)
            cameraPosition.x = target.position.x;
        if ((_axis & AxisMask.Y) == AxisMask.Y)
            cameraPosition.y = target.position.y;
        if ((_axis & AxisMask.Z) == AxisMask.Z)
            cameraPosition.z = target.position.z;
        camera.position = Vector3.Lerp(camera.position, cameraPosition, followSpeed * deltaTime);
        camera.rotation = Quaternion.Slerp(camera.rotation, Quaternion.Euler(lookOffset), lookSpeed * deltaTime);
        UpdateFOV(camera);
    }
}