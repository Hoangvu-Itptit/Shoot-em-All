using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtl : Human
{
    public Rigidbody2D rb;
    public GameObject hand;
    public GameObject joint;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void MoveGun()
    {
        base.MoveGun();
        var Ins = LevelCtl.Instance;
        hand.transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(Ins.mousePos.x, Ins.mousePos.y, 0));
    }
}