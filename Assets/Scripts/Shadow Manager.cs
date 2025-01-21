using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class ShadowManager : MonoBehaviour
{
    private GameObject selectedShadow;
    public LayerMask shadowMask;
    public GameObject player;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, shadowMask))

        {

            if (Input.GetMouseButtonDown(0))
            {
                selectedShadow = hitInfo.transform.gameObject;
                Debug.Log(selectedShadow.name);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && selectedShadow != null)
        {
            ShadowMove();
        }
    }

    void ShadowMove()
    {
        player.transform.position = selectedShadow.transform.position;
        selectedShadow = null;
    }
}
