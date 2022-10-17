using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletCtl : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector3 velocity;
    public float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    public void Start()
    {
        // Debug.Log(velocity);
        velocity = (velocity - transform.position).normalized;
        // Debug.Log(velocity);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = velocity.normalized * moveSpeed;
        if (Input.GetMouseButton(0))
        {
            // Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (transform.position.magnitude >= 10f)
        {
            gameObject.SetActive(false);
            var Ins = LevelCtl.Instance;
            Ins.RemoveActive(Ins.listActiveBullet, Ins.listUnactiveBullet);
        }
        //Raypoint();
    }

    void Raypoint()
    {
        var pos = transform.position + velocity.normalized * 0.3f;
        RaycastHit2D hitInfo = Physics2D.Raycast(pos, velocity, 0);
        Debug.DrawRay(pos, velocity);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag(Const.MAPTAG))
            {
                Debug.Log(hitInfo.collider.name);
                Debug.DrawRay(hitInfo.point, hitInfo.normal, Color.yellow);
                Vector3 reflect = Vector3.Reflect(velocity, hitInfo.normal);
                Debug.DrawRay(hitInfo.point, reflect);
                velocity = reflect;
                // Debug.DrawLine(sprite.transform.position, hit.normal, Color.yellow, 10.0f);
            }
        }
        // Ray rayhit = new Ray(transform.position, velocity);
        // Debug.DrawRay(transform.position, velocity, Color.red, 0);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag(Const.MAPTAG))
        {
            Debug.Log(col.transform.name, col.transform);
            var contact = col.GetContact(0);
            Debug.DrawRay(contact.point, contact.normal, Color.yellow);
            Vector3 reflect = Vector3.Reflect(velocity, contact.normal);
            Debug.DrawRay(contact.point, reflect);
            velocity = reflect;
        }
    }
}