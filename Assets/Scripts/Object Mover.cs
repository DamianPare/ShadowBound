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

    public bool moveObject;
    [ConditionalField("moveObject")] public Vector3 distanceToMove;

    public bool rotateObject;
    [ConditionalField("rotateObject")] public Quaternion targetAngle;

    [SerializeField] private float timeToMove;

    private Vector3 startPos;
    private Quaternion startAngle;

    private void Awake()
    {
        startPos = objectToMove.transform.position;
        startAngle = objectToMove.transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
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
        if (objectToMove.transform.position == startPos)
        {
            objectToMove.transform.DOMove(objectToMove.transform.position + distanceToMove, timeToMove);
        }

        else
        {
            objectToMove.transform.DOMove(startPos, timeToMove);
        }
    }

    void RotateObject()
    {
        if (objectToMove.transform.rotation == startAngle)
        {
            objectToMove.transform.DORotate(objectToMove.transform.rotation * targetAngle.eulerAngles, timeToMove);
        }

        else
        {
            objectToMove.transform.DORotate(startAngle.eulerAngles, timeToMove);
        }
    }
}
