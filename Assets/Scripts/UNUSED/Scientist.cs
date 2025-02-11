using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : MonoBehaviour
{
    public Vector3 boxSize;
    public float castDistance;
    public LayerMask shadowMask;


    private void Update()
    {
        if (IsInShadow())
        {
            //die
            Debug.Log("dead");
            Destroy(gameObject);
        }
    }


    public bool IsInShadow()
    {
        if (Physics.BoxCast(transform.position, boxSize, -transform.up, Quaternion.identity, castDistance, shadowMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
