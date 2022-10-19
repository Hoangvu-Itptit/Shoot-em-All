using System;
using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;

public class PlayerCtl : Human
{
    public Transform gunPos;
    private Bone bone;
    public SkeletonAnimation skeletonAnimation;

    [SpineBone(dataField: "skeletonAnimation")]
    public string boneName;

    public LineRenderer lineRenderer;
    private Camera cam;
    private LayerMask mapLayer;

    
    
    public int reflections;
    public float maxLength;

    private Ray2D ray;
    private RaycastHit2D hit;
    private Vector3 direction;
    // private BoneFollower _boneFollower;

    public override void Awake()
    {
        base.Awake();
        mapLayer = LayerMask.NameToLayer("map");
        if (skeletonAnimation == null) skeletonAnimation = GetComponent<SkeletonAnimation>();
        bone = skeletonAnimation.Skeleton.FindBone(boneName);
        // _boneFollower=GetComponent<BoneFollower>();
        cam = Camera.main;
    }

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        // BulletLine();

         Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        // Vector2 startPos = gunPos.position;
        // SetLinePosition(startPos, true);
        //
        // Vector2 direction = mousePos - startPos;
        // for (int i = 0; i < 10; i++)
        // {
        //     var hit = Physics2D.Raycast(startPos, direction, 10000, 1 << mapLayer);
        //     if (hit.collider)
        //     {
        //         var point = hit.point;
        //         Debug.DrawLine(startPos, point);
        //         startPos = point;
        //         Vector2 reflect = Vector2.Reflect(direction, hit.normal);
        //         direction = reflect;
        //         SetLinePosition(point);
        //     }
        //     else
        //     {
        //     }
        // }
        
        
        ray = new Ray2D(gunPos.position, mousePos - (Vector2)gunPos.position);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, gunPos.position);
        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            hit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength);
            if(hit.collider)
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray2D(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                if (hit.collider.tag != "Map")
                    break;
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
    }

    public override void MoveGun()
    {
        //gunPos.position = bone.GetWorldPosition(transform);
        // base.MoveGun();
        // var Ins = LevelCtl.Instance;
        // var pos = Ins.mousePos - hand.transform.position;
        // hand.transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(Ins.mousePos.x, Ins.mousePos.y, 0));
        // hand.transform.rotation = Quaternion.LookRotation(Ins.mousePos);
        // var deg = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        // hand.transform.rotation = Quaternion.Euler(0, 0, deg);
        // var mousePosition = Input.mousePosition;
        // var worldMousePosition = cam.ScreenToWorldPoint(mousePosition);
        // var skeletonSpacePoint = skeletonAnimation.transform.InverseTransformPoint(worldMousePosition);
        // skeletonSpacePoint.x *= skeletonAnimation.Skeleton.ScaleX;
        // skeletonSpacePoint.y *= skeletonAnimation.Skeleton.ScaleY;
        // bone.SetLocalPosition(skeletonSpacePoint);
    }

    private Vector2 lastMousePos;

    // void BulletLine()
    // {
    //     lastMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    //     
    //     Vector2 startPoint = gunPos.position;
    //     Vector2 dir = lastMousePos - startPoint;
    //
    //     lineRenderer.positionCount = 1;
    //     lineRenderer.SetPosition(0, startPoint);
    //     int lineCount = 0;
    //     while (lineCount < 10)
    //     {
    //         RaycastHit2D hit = Physics2D.Raycast(startPoint, dir, 10000, 1 << 3);
    //         // Debug.DrawRay(startPoint, dir, Color.green);
    //         if (hit.collider != null)
    //         {
    //             Debug.DrawRay(startPoint, hit.point - startPoint, Color.red);
    //             Debug.DrawRay(hit.point, hit.normal, Color.green);
    //             var hitPoint = hit.point;
    //             SetLinePosition(hitPoint);
    //             dir = Vector2.Reflect(dir, hit.normal);
    //             startPoint = hitPoint;
    //             lineCount++;
    //         }
    //         else
    //         {
    //             SetLinePosition(startPoint + dir.normalized * 10);
    //             Debug.DrawRay(startPoint, dir.normalized * 10, Color.red);
    //             return;
    //         }
    //     }
    // }

    void SetLinePosition(Vector2 pos, bool reset = false)
    {
        if (reset)
        {
            lineRenderer.positionCount = 1;
        }
        else
        {
            lineRenderer.positionCount++;
        }

        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
    }
}