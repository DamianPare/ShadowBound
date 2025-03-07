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
    [SerializeField]
    private float gracePeriod = 1f;

    public Vector3 boxSize;
    public float castDistance;
    public LayerMask shadowMask;

    private Rigidbody RigidBody;
    private Vector3 lastLocation;
    private GameObject groundedObject;
    private float gracePeriodTimer;

    [SerializeField] private float timeToMove;
    [SerializeField] private float distanceToMove;
    [SerializeField] private ParticleSystem melt;

    public bool isTeleporting;

    public static PlayerControl instance;

    public bool levelCompleted;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks cursor
        Cursor.visible = false; // sets cursor invisible
        gracePeriodTimer = Time.time;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gracePeriodTimer);
        UpdateVelocity();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            GameManager.instance.LevelCompleted = true;
            Debug.Log("Level Completed");
        }
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

        if (isTeleporting)
        {
            gracePeriodTimer = Time.time;
        }


        if (IsGrounded())
        {
            RigidBody.drag = 5f;
            gracePeriodTimer = Time.time;
            RigidBody.velocity = new Vector3(MovementVelocity.x, RigidBody.velocity.y, MovementVelocity.z);

            if (RigidBody.velocity.magnitude < 1 && lastLocation != transform.position && !isTeleporting)
            {
                lastLocation = transform.position;
            }
        }

        else if (Time.time - gracePeriodTimer > gracePeriod && !isTeleporting)
        {
            melt.Play();
            Debug.Log("AAHHHH IT BURNS MY FLESH IS MELTING OFF");
            RigidBody.drag = 20f;

            if (groundedObject.transform.hasChanged)
            {
                Debug.Log("GET FUCKED PUSSY BOY, I DID IT");
                ShadowManager.instance.MoveToCenter(groundedObject);
                groundedObject.transform.hasChanged = false;
            }

            else
            {
                Vector3 dest = new Vector3(lastLocation.x, (lastLocation.y - 0.5f), lastLocation.z);
                ShadowManager.instance.MoveToSpot(dest);
            }
        }


    }

    public bool IsGrounded()
    {
        if (Physics.BoxCast(transform.position, boxSize, -transform.up, out RaycastHit hitInfo, Quaternion.identity, castDistance, shadowMask))
        {
            if (groundedObject == hitInfo.transform.gameObject)
            {
                return true;
            }
            
            else
            {
                groundedObject = hitInfo.transform.gameObject;
                groundedObject.transform.hasChanged = false;
                return true;
            }
        }

        else
        {

            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }

    
}
