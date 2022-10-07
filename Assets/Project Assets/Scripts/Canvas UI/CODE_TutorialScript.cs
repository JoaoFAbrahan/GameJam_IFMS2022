using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CODE_TutorialScript : MonoBehaviour
{
    public TextMeshProUGUI[] messages;

    public bool t = CODE_ParameterBetweenScenes.calledFromMenu;

    int i = 0;

    private void Start()
    {
        if(CODE_ParameterBetweenScenes.calledFromMenu)
        {
            Debug.Log(CODE_ParameterBetweenScenes.calledFromMenu);
            transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0f;
            messages[0].gameObject.SetActive(true);
            messages[1].gameObject.SetActive(false);
            messages[2].gameObject.SetActive(false);
        }
       
    }

    private void Update()
    {
        if(CODE_ParameterBetweenScenes.calledFromMenu)
        {
            gameObject.SetActive(true);
            Debug.Log(CODE_ParameterBetweenScenes.calledFromMenu);
            if (Input.GetKeyDown(KeyCode.F) && i < 3)
            {
                messages[i].gameObject.SetActive(false);
                i++;
                if (i < 3)
                    messages[i].gameObject.SetActive(true);
            }

            if (i == 3)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                Time.timeScale = 1f;
                CODE_ParameterBetweenScenes.calledFromMenu = false;
                Debug.Log(CODE_ParameterBetweenScenes.calledFromMenu);
            }
        }
       
    }
}
