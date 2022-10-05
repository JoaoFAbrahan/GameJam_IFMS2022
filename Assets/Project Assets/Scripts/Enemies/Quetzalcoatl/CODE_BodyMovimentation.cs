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

    /// <summary>
    /// Move enemy body
    /// </summary>
    private void MoveBody()
    {
        // Move the enemy towards the target position
        transform.position = Vector2.MoveTowards(transform.position, target.position, flySpeed * Time.deltaTime);
    }
}