using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerCtl : Human
{
    public GameObject hand;
    public Transform gunPos;

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
        var pos = Ins.mousePos - hand.transform.position;
        // hand.transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(Ins.mousePos.x, Ins.mousePos.y, 0));
        // hand.transform.rotation = Quaternion.LookRotation(Ins.mousePos);
        var deg = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        hand.transform.rotation =Quaternion.Euler(0,0,deg);
        // var mousePosition = Input.mousePosition;
        // var worldMousePosition = cam.ScreenToWorldPoint(mousePosition);
        // var skeletonSpacePoint = skeletonAnimation.transform.InverseTransformPoint(worldMousePosition);
        // skeletonSpacePoint.x *= skeletonAnimation.Skeleton.ScaleX;
        // skeletonSpacePoint.y *= skeletonAnimation.Skeleton.ScaleY;
        // bone.SetLocalPosition(skeletonSpacePoint);
    }
}