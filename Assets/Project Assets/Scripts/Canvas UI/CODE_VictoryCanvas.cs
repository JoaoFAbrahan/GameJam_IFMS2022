using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CODE_VictoryCanvas : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject timePanel;

    private void Update()
    {
        StartCoroutine(nameof(TimeToMenu));
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MAP_MainMenu");
        }
            
        
    }

    public IEnumerator TimeToMenu()
    {

        yield return new WaitForSeconds(40f);
        victoryPanel.SetActive(true);
        timePanel.SetActive(false);
    }
}
