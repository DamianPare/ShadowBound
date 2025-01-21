using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float MaximumSpeed = 6.0f;
    public Camera RotationToFollow;
    public float Momentum = 0.75f;

    [SerializeField]
    private Vector2 LastSpeed = Vector2.zero;
    [SerializeField]
    private Vector3 MovementVelocity = Vector3.zero;

    public Vector3 boxSize;
    public float castDistance;
    public LayerMask shadowMask;

    private Rigidbody RigidBody;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(IsGrounded());

        if (IsGrounded())
        {
            UpdateVelocity();
        }
        
        UpdateRotation();
    }

    void UpdateRotation()
    {
        float DesiredYaw = RotationToFollow.transform.rotation.eulerAngles.y;
        Vector3 EularAngles = transform.rotation.eulerAngles;
        EularAngles.y = DesiredYaw;
        transform.rotation = Quaternion.Euler(EularAngles);
    }

    void UpdateVelocity()
    {
        float xKeyboardInput = Input.GetAxis("Horizontal");
        float yKeyboardInput = Input.GetAxis("Vertical");

        Vector3 CurrentVelocity = RigidBody.GetRelativePointVelocity(Vector3.zero);


        float DesiredSpeed_y = xKeyboardInput * MaximumSpeed;
        float DesiredSpeed_x = yKeyboardInput * MaximumSpeed;

        float Speed_x = Mathf.Lerp(LastSpeed.x, DesiredSpeed_x, 1.0f - Momentum);
        float Speed_y = Mathf.Lerp(LastSpeed.y, DesiredSpeed_y, 1.0f - Momentum);

        LastSpeed.x = Speed_x;
        LastSpeed.y = Speed_y;

        MovementVelocity = GetComponent<Transform>().forward * Speed_x;
        MovementVelocity += GetComponent<Transform>().right * Speed_y;
        RigidBody.velocity = new Vector3(MovementVelocity.x, RigidBody.velocity.y, MovementVelocity.z);
    }

    public bool IsGrounded()
    {
        if (Physics.BoxCast(transform.position, boxSize, -transform.up, Quaternion.identity, castDistance, shadowMask))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }
}
