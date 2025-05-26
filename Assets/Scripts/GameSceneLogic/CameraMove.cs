using System;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector3 lookOffset;
    
    public float movespeed;
    public float rotateSpeed;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 newPos = target.position + target.forward * offset.z;
        newPos += Vector3.up * offset.y;
        newPos += target.right * offset.x;
        transform.position = Vector3.Lerp(
            transform.position,
            newPos,
            movespeed * Time.deltaTime
        );
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(target.position + lookOffset - transform.position),
            rotateSpeed * Time.deltaTime
            );
    }
}
