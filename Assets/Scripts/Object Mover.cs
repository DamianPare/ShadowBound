using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private Vector3 distanceToMove;
    [SerializeField] private float timeToMove;

    private Transform startPos;

    private void Awake()
    {
        startPos = transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            moveObject();
        }
    }

    void moveObject()
    {
        objectToMove.transform.DOMove(objectToMove.transform.position + distanceToMove, timeToMove);
    }
}
