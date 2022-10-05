using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CODE_BodyRotation : MonoBehaviour
{
    public float rotationSpeed;
    public float flySpeed;
    public Transform target;

    private Vector2 _direction;

    void Update()
    {
        RotateBody();
    }


    /// <summary>
    /// Rotate enemy body
    /// </summary>
    protected void RotateBody()
    {
        // Get target position. It must be the player for the head and a body part for the rest.
        _direction = target.position - transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        // Creates a rotation pointing towards the target direction
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Apply rotation to the enemy
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}