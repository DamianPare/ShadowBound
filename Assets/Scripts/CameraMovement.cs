using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float sensY; // y sensitvity
    public float sensX; // x sensitvity
                        // 
    //public CinemachineVirtualCamera vcam;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private Vector3 lastLocation;
    private Transform targetPosition;

    // Update is called once per frame

    private void Awake()
    {
        //targetPosition = transform;
        //vcam.Follow = targetPosition;
    }
    void Update()
    {

        //targetPosition.position = transform.position;   
        Debug.Log(lastLocation);
        if (ShadowManager.instance.isMoving)
        {
            //vcam.m_Follow.position = lastLocation;
        }

        if (ShadowManager.instance.isMoving == false)
        {
            //vcam.m_Follow.position = this.transform.position;
            //lastLocation = this.transform.position;
        }


        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        // prevents going past 90 degrees
        xRotation = Mathf.Clamp(xRotation, -10f, 50f);

        // camera rotation and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
