using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CODE_Intro : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(videoPlayer.time >= 12f)
        {
            Debug.Log("Oi");
            SceneManager.LoadScene("Map_MainMenu");
        }
    }
}
