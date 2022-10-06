using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CODE_Tocha : MonoBehaviour
{
    private Light _light;

    private void Start()
    {
        _light = this.GetComponent<Light>();
    }
}
