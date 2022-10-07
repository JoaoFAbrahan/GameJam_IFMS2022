using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CODE_Stairs : MonoBehaviour
{
    public string sceneToLoad;


    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.transform.parent.name == "PFB_Player")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
