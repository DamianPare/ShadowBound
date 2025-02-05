using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine.Utility;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private Vector3 distanceToMove;
    [SerializeField] private float timeToMove;

    private Vector3 startPos;

    private void Awake()
    {
        startPos = objectToMove.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            MoveObject();
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
}
