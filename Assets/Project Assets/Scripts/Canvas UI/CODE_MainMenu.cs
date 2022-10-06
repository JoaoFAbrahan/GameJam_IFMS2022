using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace USER_INTERFACE
{
    public class CODE_MainMenu : MonoBehaviour
    {
        public string startScene;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartGame()
        {
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Application.Quit();
            //Debug.Log("Quitting Game");
        }
    }
}

