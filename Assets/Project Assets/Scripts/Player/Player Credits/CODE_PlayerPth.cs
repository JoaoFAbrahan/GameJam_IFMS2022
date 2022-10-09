using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CODE_PlayerPth : MonoBehaviour
{
    public float speed = 10f;
    public Transform target;


    private void Start()
    {
    }

    private void Update()
    {
        Vector3 Direction = new Vector3(target.position.x, transform.position.y, transform.position.z) - transform.position;

        this.transform.Translate(Direction.normalized * speed * Time.deltaTime, Space.World);
    }
}