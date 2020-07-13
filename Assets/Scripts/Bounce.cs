using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    Rigidbody rb;
    public float MyVelocity;
    [Range(-1, 1)]
    public float SideSpeed;
    [Range(-1, 1)]
    public float ForwardSpeed;

    public int ChangeEveryXSteps;
    public int MySteps = 0;
    private Vector3 Direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetRandomDirection();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (MySteps >= ChangeEveryXSteps)
        {
            SetRandomDirection();
        }
        rb.velocity = Direction;
        MySteps++;
    }

    private void SetRandomDirection()
    {
        SideSpeed = Random.Range(-1f, 1f);
        ForwardSpeed = Random.Range(-1f, 1f);
        rb.velocity = new Vector3(SideSpeed, MyVelocity, ForwardSpeed);
        Direction = new Vector3(SideSpeed, MyVelocity, ForwardSpeed);

        MySteps = 0;
    }

}
