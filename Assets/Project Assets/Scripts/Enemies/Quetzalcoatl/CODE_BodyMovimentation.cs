using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CODE_BodyMovimentation : CODE_BodyRotation
{
    void Update()
    {
        RotateBody();
        MoveBody();
    }

    private void MoveBody()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, flySpeed * Time.deltaTime);
    }
}




