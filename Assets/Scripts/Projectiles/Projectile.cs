using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : ItemBase
{
    [SerializeField] private Rigidbody rb;

    private Vector3 lastVelocity;
    private float curSpeed;
    private Vector3 direction;

    void OnEnable()
    {
        direction = Shooting.instance.Spawnpoint.transform.forward;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }

    private void LateUpdate()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.instance.PlaySound(TypeOfSound.Shoot_Hit, 0.1f);
        gameObject.SetActive(false);
    }
}
