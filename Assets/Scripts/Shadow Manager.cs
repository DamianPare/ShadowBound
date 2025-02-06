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
    [SerializeField] private GameObject crosshairSmall;
    [SerializeField] private GameObject crosshairBig;

    private void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, shadowMask))
        {
            crosshairBig.SetActive(false);
            crosshairSmall.SetActive(true);

            if (Input.GetMouseButtonDown(1) && !isMoving)
            {
                selectedSpot = hitInfo.point;
                selectedShadow = hitInfo.transform.gameObject;
                Debug.Log(selectedShadow.name);
                MoveToSpot();
            }
        }

        else
        {
            crosshairBig.SetActive(true);
            crosshairSmall.SetActive(false);
        }
    }

    //void MoveToCenter(GameObject shadow)
    //{
    //    Vector3 center = shadow.GetComponent<Shadow>().FindCenter() + new Vector3(0,0.7f,0);
    //    selectedShadow = null;

    //    destination = center;
    //    isMoving = true;
    //}

    void MoveToSpot()
    {
        Vector3 spot = selectedSpot;
        destination = spot + new Vector3(0, 0.65f, 0);
        isMoving = true;

        ShadowMove();
    }

    void ShadowMove()
    {
        PlayerControl.instance.isTeleporting = true;
        DG.Tweening.Sequence mySequence = DOTween.Sequence();

        mySequence.Append(player.transform.DOMove(player.transform.position + new Vector3(0, -2, 0), timeToMove));
        mySequence.Append(player.transform.DOMove(destination + new Vector3(0, -2, 0), 0.5f));
        mySequence.Append(player.transform.DOMove(destination, timeToMove));
        isMoving = false;
        PlayerControl.instance.isTeleporting = false;

        if (transform.position == destination)
        {
            isMoving = false;
        }
    }
}
 