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
        Vector3 Direction = target.position - transform.position;
        this.transform.Translate(Direction.normalized * speed * Time.deltaTime, Space.World);
    }
}