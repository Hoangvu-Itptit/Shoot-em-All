using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCtl : MonoBehaviour
{
    public static LevelCtl Instance;

    public PlayerCtl player;
    public GameObject bulletPrefabs;
    public List<GameObject> listActiveBullet;
    public List<GameObject> listUnactiveBullet;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            var gun = player.gunPos.position;

            if (listUnactiveBullet.Count != 0)
            {
                listUnactiveBullet[0].transform.position = gun;
                var Bullet = listUnactiveBullet[0].GetComponent<BulletCtl>();
                Bullet.velocity = new Vector3(mousePos.x, mousePos.y, 0);
                Bullet.Start();
                listUnactiveBullet[0].SetActive(true);
                listActiveBullet.Add(listUnactiveBullet[0]);
                listUnactiveBullet.RemoveAt(0);
                return;
            }

            GameObject bullet = Instantiate(bulletPrefabs, gun, Quaternion.identity);
            bullet.GetComponent<BulletCtl>().velocity = new Vector3(mousePos.x, mousePos.y, 0);
            listActiveBullet.Add(bullet);
        }
    }

    public void RemoveActive(List<GameObject> listActive, List<GameObject> listUnactive)
    {
        for (var index = 0; index < listActive.Count;)
        {
            var goj = listActive[index];
            if (!goj.activeInHierarchy)
            {
                listUnactive.Add(goj);
                listActive.RemoveAt(index);
            }
            else index++;
        }
    }
}