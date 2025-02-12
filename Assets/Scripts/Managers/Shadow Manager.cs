using UnityEngine;
using DG.Tweening;
using Sequence = DG.Tweening.Sequence;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class ShadowManager : MonoBehaviour
{
    private GameObject selectedShadow;
    private Vector3 selectedSpot;
    public LayerMask shadowMask;
    public GameObject player;
    public bool isMoving = false;
    private Vector3 destination;
    public static ShadowManager instance;

    [SerializeField] private float timeToMove;
    [SerializeField] private float distanceToMove;
    [SerializeField] private float range;
    [SerializeField] private GameObject crosshairSmall;
    [SerializeField] private GameObject crosshairBig;
    private GameObject charSprite;
    private Animator characterAnim;
    private SpriteRenderer charRenderer;

    private void Awake()
    {
        charSprite = GameObject.Find("Sprite");
        instance = this;
    }

    private void Start()
    {
        charRenderer = charSprite.GetComponent<SpriteRenderer>();
        characterAnim = charSprite.GetComponent<Animator>();
    }
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

        StartCoroutine(ShadowMove());
    }

    IEnumerator ShadowMove()
    {
        characterAnim.SetBool("isTeleporting", true);
        PlayerControl.instance.isTeleporting = true;

        yield return new WaitForSeconds(1);

        DG.Tweening.Sequence mySequence = DOTween.Sequence();
        charRenderer.enabled = false;
        //mySequence.Append(player.transform.DOMove(player.transform.position + new Vector3(0, -2, 0), timeToMove));
        //mySequence.Append(player.transform.DOMove(destination + new Vector3(0, -2, 0), 0.5f));
        mySequence.Append(player.transform.DOMove(destination + new Vector3(0, -0.2f, 0), timeToMove).SetEase(Ease.Linear));
        mySequence.OnComplete(SetBool);

        yield return new WaitForSeconds(1);

        PlayerControl.instance.isTeleporting = false;
    }

    IEnumerator Delay()
    {
        characterAnim.SetBool("isTeleporting", true);
        PlayerControl.instance.isTeleporting = true;

        yield return new WaitForSeconds(1);

        ShadowMove();
    }

    void SetBool()
    {
        charRenderer.enabled = true;
        characterAnim.SetBool("isTeleporting", false);
        isMoving = false;
        //PlayerControl.instance.isTeleporting = false;
    }
}
 