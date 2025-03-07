using System.Collections;
using MyBox;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _spawnpoint;
    [SerializeField] private ItemData _defaultProjectile;
    [SerializeField] private float _launchForce;
    [SerializeField] private float fireRate;

    public static Shooting instance;

    public Transform Spawnpoint => _spawnpoint;

    private ItemData _currentProjectil;
    private float _horizontalInput;
    private float _fireCooldown;
    private float _abilityCountdown;
    private bool _hasSpecialProjectile;
    private bool _isFiring = false;
    public bool _hasPowerUp { get; private set; }
    public PoolManager _poolManager { get; private set; }

    private float nextFire;


    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;   
    }
    void Start()
    {
        
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && GameManager.instance.isPaused == false && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire();
        }
    }

    private void OnEnable()
    {
        _currentProjectil = _defaultProjectile;
    }

    private void OnDisable()
    {
        _isFiring = false;
        StopAllCoroutines();
    }

    private void Fire()
    {
        AudioManager.instance.PlaySound(TypeOfSound.Shoot, 1);
        _isFiring = true;
        var i = _poolManager.GetItem(_currentProjectil, this);
        var rbi = i.GetComponent<Rigidbody>();
        rbi.velocity = Spawnpoint.transform.forward * _launchForce;
        StartCoroutine(FireCooldown());
    }

    IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(_fireCooldown);
        _isFiring = false;
    }

    public void SetUp(PoolManager main)
    {
        _poolManager = main;
    }

    public void ReturnObject(ItemBase item)
    {
        _poolManager.RemoveItem(item);
    }
}
