using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace USER_INTERFACE
{
    public class CODE_MainMenu : MonoBehaviour
    {
        public string startScene;

        private SONORIZATION.CODE_SoundManager _soundManager;
        // Start is called before the first frame update
        void Start()
        {
            _soundManager = GameObject.Find("SoundManager").GetComponent<SONORIZATION.CODE_SoundManager>();
            _soundManager.PlayOSTMainMenu();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartGame()
        {
            _soundManager.PlayButtonSound(GameObject.Find("SoundManager").GetComponent<AudioSource>());
            SceneManager.LoadScene(startScene);
        }

        public void QuitGame()
        {
            Application.Quit();
            //Debug.Log("Quitting Game");
        }
    }
}

