using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine.Utility;
using MyBox.Internal;
using MyBox;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private Material usableMaterial;
    [SerializeField] private Material unusableMaterial;

    public bool moveObject;
    [ConditionalField("moveObject")] public Vector3 distanceToMove;

    public bool rotateObject;
    [ConditionalField("rotateObject")] public Quaternion angleToRotate;

    [SerializeField] private float timeToMove;

    private Vector3 startPos;
    private Vector3 startAngle;
    private Renderer buttonRenderer;
    private bool isMoving;
    private Vector3 targetPos;
    private Vector3 targetAngle;

    private void Awake()
    {
        isMoving = false;
        buttonRenderer = gameObject.GetComponent<MeshRenderer>();
        startPos = objectToMove.transform.position;
        startAngle = objectToMove.transform.rotation.eulerAngles;
        targetPos = objectToMove.transform.position + distanceToMove;
        targetAngle = objectToMove.transform.rotation * angleToRotate.eulerAngles;
    }

    private void Start()
    {
        buttonRenderer.material = usableMaterial;
    }

    private void Update()
    {
        if (!AtSpawn() || !AtTarget())
        {
            isMoving = true;
            buttonRenderer.material = unusableMaterial;
        }

        if (AtSpawn() || AtTarget())
        {
            isMoving = false;
            buttonRenderer.material = usableMaterial;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9 && !isMoving)
        {

            if (moveObject)
            {            
                MoveObject();
            }

            if (rotateObject)
            {
                RotateObject();
            }
        }
    }

    void MoveObject()
    {

        if (AtTarget())
        {
            objectToMove.transform.DOMove(startPos, timeToMove);
        }

        if (AtSpawn())
        {
            objectToMove.transform.DOMove(objectToMove.transform.position + distanceToMove, timeToMove);
        }
    }

    void RotateObject()
    {

        if (AtTarget())
        {
            objectToMove.transform.DORotate(startAngle, timeToMove);
        }

        if (AtSpawn())
        {
            objectToMove.transform.DORotate(objectToMove.transform.rotation * angleToRotate.eulerAngles, timeToMove);
        }
    }

    private bool AtTarget()
    {
        if (moveObject && objectToMove.transform.position == targetPos)
        {
            return true;
        }

        else if (rotateObject && objectToMove.transform.rotation.eulerAngles == targetAngle)
        {
            return true;
        }

        return false;
    }

    private bool AtSpawn()
    {
        if (moveObject && objectToMove.transform.position == startPos)
        {
            return true;
        }

        else if (rotateObject && objectToMove.transform.rotation.eulerAngles == startAngle)
        {
            return true;
        }

        return false;
    }
}
