using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace USER_INTERFACE
{
    public class CODE_PauseMenu : MonoBehaviour
    {
        public string mainMenu;

        public GameObject pauseScreen;
        public bool isPaused;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseUnpause();
            }
        }

        public void PauseUnpause()
        {
            if (isPaused)
            {
                isPaused = false;
                pauseScreen.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                isPaused = true;
                pauseScreen.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(mainMenu);
            Time.timeScale = 1f;
        }
    }
}

