using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletScript : MonoBehaviour
{

    public BulletData bulletData;

    private Vector2 startPosition;
    private float conquoredDistance = 0;
    private Rigidbody2D rb2d;

    public UnityEvent OnHit = new UnityEvent();
    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Initialize(BulletData bulletData)
    {
        this.bulletData = bulletData;
        startPosition = transform.position;
        rb2d.velocity = transform.up * this.bulletData.speed;
    }

    private void Update()
    {
        conquoredDistance = Vector2.Distance(transform.position, startPosition);
        if (conquoredDistance >= bulletData.maxDistance)
        DisableObject();
    }

    private void DisableObject()
    {
        rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided " + collision.name);
        OnHit?.Invoke();
        var damagable = collision.GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.Hit(bulletData.damage);
        }
        
        DisableObject();
    }


}
