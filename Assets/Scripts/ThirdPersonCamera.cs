using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
   public float RotationSensitivity = 5.0f;
    public float BoomLength = 10.0f;
    public float LowestAngleInDegrees = 0.0f;
    public float HighestAngleInDegrees = 70.0f;
    public GameObject FollowTarget;
    public float RotationLag = 0.1f;
    public bool InverseX = false;
    public bool InverseY = true;
    public Vector3 Offset = Vector3.zero;
    public float AutoTargetPitch = 45.0f;

    private Quaternion TargetRotation;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLook();
        UpdatePositionFromLook();
        
    }

    void UpdateLook()
    {
        // Input corresponds to Yaw
        float yInput = InverseX ? -Input.GetAxis("Mouse X") : Input.GetAxis("Mouse X");

        // Input corresponds to Pitch
        float xInput = InverseY ? -Input.GetAxis("Mouse Y") : Input.GetAxis("Mouse Y");

        Vector3 EulerAngles = TargetRotation.eulerAngles;
        EulerAngles.y += (yInput * RotationSensitivity);
        float PitchDelta = (xInput * RotationSensitivity);

        //Class Exercise Clamp the Rotation
        float CurrentPitch = TargetRotation.eulerAngles.x;
        if (CurrentPitch > 180.0f)
        {
            CurrentPitch = CurrentPitch - 360.0f;
        }

        float DesiredPitch = CurrentPitch + PitchDelta;
        DesiredPitch = Mathf.Clamp(DesiredPitch, LowestAngleInDegrees, HighestAngleInDegrees);
        PitchDelta = DesiredPitch - CurrentPitch;
        EulerAngles.x += PitchDelta;
        // End of class exercise

        TargetRotation = Quaternion.Euler(EulerAngles);
        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, RotationLag);

    }

    void UpdatePositionFromLook()
    {
        Vector3 RelativeOffset = FollowTarget.transform.rotation * Offset;
        Vector3 Pivot = FollowTarget.transform.position + RelativeOffset;
        Vector3 LookDirection = transform.rotation * Vector3.forward;
        transform.position = Pivot - (BoomLength * LookDirection);
    } 
}
