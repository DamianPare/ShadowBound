using UnityEngine;
using DG.Tweening;
using Sequence = DG.Tweening.Sequence;


public class ShadowManager : MonoBehaviour
{
    private GameObject selectedShadow;
    private Vector3 selectedSpot;
    public LayerMask shadowMask;
    public GameObject player;
    private bool isMoving = false;
    private Vector3 destination;

    [SerializeField] private float timeToMove;
    [SerializeField] private float distanceToMove;

    private void Update()
    {
        while (isMoving)
        {
            ShadowMove();
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, shadowMask))

        {

            if (Input.GetMouseButtonDown(0))
            {
                selectedSpot = hitInfo.point;
                selectedShadow = hitInfo.transform.gameObject;
                Debug.Log(selectedShadow.name);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift) && selectedShadow != null)
        {
            MoveToSpot();
        }

        else if (Input.GetKeyDown(KeyCode.Space) && selectedShadow != null)
        {
            MoveToCenter(selectedShadow);
        }
    }

    void MoveToCenter(GameObject shadow)
    {
        Vector3 center = shadow.GetComponent<Shadow>().FindCenter() + new Vector3(0,0.7f,0);
        selectedShadow = null;

        destination = center;
        isMoving = true;
    }

    void MoveToSpot()
    {
        Vector3 spot = selectedSpot;
        destination = spot;
        isMoving = true;
    }

    void ShadowMove()
    {
        PlayerControl.instance.isTeleporting = true;
        DG.Tweening.Sequence mySequence = DOTween.Sequence();

        mySequence.Append(player.transform.DOMove(player.transform.position + new Vector3(0, -2, 0), timeToMove));
        //mySequence.PrependInterval(1.5f);
        mySequence.Append(player.transform.DOMove(destination + new Vector3(0, -2, 0), 0));
        //mySequence.PrependInterval(1.5f);
        mySequence.Append(player.transform.DOMove(destination, timeToMove));
        isMoving = false;
        PlayerControl.instance.isTeleporting = false;

        if (transform.position == destination)
        {
            isMoving = false;

        }
    }
}
 