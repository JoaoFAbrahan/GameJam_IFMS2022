using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace USER_INTERFACE
{
    public class CODE_PauseMenu : MonoBehaviour
    {
        public string mainMenu;

        public GameObject pauseScreen;
        public bool isPaused;

        public TextMeshProUGUI timerText;

        private Color _flipColor = Color.white;
        private static float _timeCount;

        private bool changedColorThisFrame;
        public float timer;
        private GameObject _deathImage;

        // Start is called before the first frame update
        void Start()
        {
            if (this.gameObject.name == "PFB_UI Canvas Levels")
            {
                _deathImage = this.transform.GetChild(1).gameObject;
            }
        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
            if(this.gameObject.name == "PFB_UI Canvas Levels")
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseUnpause();
                }
            }
           
        }

        public void RefreshTimeText(int Min, int Sec)
        {
            timerText.text = Min.ToString() + ":" + Sec.ToString();
        }

        public void WarningTextColor()
        {
            if (timer <= 0f)
            {
                timerText.color = FlipFlop();
                timer = 1f;

            }

        }

        private Color FlipFlop()
        { 
            return _flipColor == timerText.color ? Color.red : Color.white;
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

        public void DeathMenu()
        {
            this._deathImage.SetActive(true);
            StartCoroutine(nameof(TimeToContinue));
        }
        
        public IEnumerator TimeToContinue()
        {
            Time.timeScale = 1f;
            yield return new WaitForSeconds(5f);
            Debug.Log("Ola");
            CODE_ParameterBetweenScenes.calledFromMenu = false;
            SceneManager.LoadScene("MAP_Level1");
        }
    }
}

