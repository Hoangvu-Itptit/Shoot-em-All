using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Human : MonoBehaviour
{
    public Rigidbody2D rb;

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag(Const.BULLETTAG))
        {
            Die();
        }
    }

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //Di chuyển súng theo mousePoint
        MoveGun();
        //Hình ảnh di chuyển của nhân vật;
        Animate();
    }

    public virtual void MoveGun()
    {
    }

    public virtual void Die()
    {
        Destroy(gameObject,1);
    }

    public virtual void Animate()
    {
        
    }
}
