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
            ShadowMove(selectedShadow);
        }
    }

    void ShadowMove(GameObject shadow)
    {
        player.transform.position = shadow.GetComponent<Shadow>().FindCenter() + new Vector3(0,0.7f,0);
        selectedShadow = null;    
    }
}
