using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private Vector3 _shadowCenter;
    private Vector3[] objectVertices;
    public Vector3 ShadowCenter => _shadowCenter;

    public Vector3 FindCenter()
    {
        objectVertices = transform.GetComponent<MeshCollider>().sharedMesh.vertices.Distinct().ToArray();

        var total = Vector3.zero;

        foreach (Vector3 point in objectVertices)
        {
            total += point;
        }

        _shadowCenter = transform.position + (total / objectVertices.Length);

        return _shadowCenter;
    }
}
