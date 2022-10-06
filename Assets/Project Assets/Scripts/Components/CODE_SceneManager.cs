using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SAP2D;

public class CODE_SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(0).GetComponent<SAP2DPathfinder>().CalculateColliders();
    }
}
