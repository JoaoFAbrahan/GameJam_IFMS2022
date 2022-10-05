using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CODE_BodyRotation : MonoBehaviour
{
    public float rotationSpeed;
    public float flySpeed;
    public Transform target;

    private Vector2 _direction;

    // Update is called once per frame
    void Update()
    {
        RotateBody();
    }

    protected void RotateBody()
    {
        _direction = target.position - transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
