using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CODE_PlayerPth : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int wavePointIndex = 0;


    private void Start()
    {
        this.transform.position = GameObject.Find("START").transform.position;
        target = CODE_WaypointList.points[0];
    }

    private void Update()
    {
        Vector3 Direction = target.position - transform.position;
        this.transform.Translate(Direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(this.transform.position, target.position) <= 0.2f)
        {
            if (wavePointIndex == 1 || wavePointIndex == 4 || wavePointIndex == 6 || wavePointIndex == 9)
            {
                StartCoroutine(TimeToContinue());
            }
            else
            {
                GetNextWaypoint();

            }
        }
    }

    private void GetNextWaypoint()
    {
        if (wavePointIndex >= CODE_WaypointList.points.Length - 1)
        {
            Debug.Log("CABOOO");
        }

        wavePointIndex++;
        target = CODE_WaypointList.points[wavePointIndex];
    }

    public IEnumerator TimeToContinue()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(5f);
        GetNextWaypoint();
    }
}
