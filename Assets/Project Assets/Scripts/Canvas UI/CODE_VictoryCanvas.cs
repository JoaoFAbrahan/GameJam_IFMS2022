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
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(nameof(TimeToMenu));
        }
    }

    public IEnumerator TimeToMenu()
    {
        
        victoryPanel.SetActive(true);
        timePanel.SetActive(false);
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("MAP_MainMenu");
    }
}
