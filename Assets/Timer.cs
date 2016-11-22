using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class Timer : MonoBehaviour {

    public GameObject timerText;
    public float secondsElapsed;
	// Update is called once per frame

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Grid")
        {
            secondsElapsed = 0;
            timerText = GameObject.Find("LevelManager").GetComponent<ReloadLevel>().Overlay.transform.GetChild(1).gameObject;
        }
    }

    void OnLevelWasLoaded()
    {
        if (SceneManager.GetActiveScene().name == "Grid")
        {
            secondsElapsed = 0;
            timerText = GameObject.Find("LevelManager").GetComponent<ReloadLevel>().Overlay.transform.GetChild(1).gameObject;
        }
    }

	void Update () {
        secondsElapsed = Time.timeSinceLevelLoad * VariableManager.instance.timeMultiplier;
        VariableManager.instance.secondsElapsed = secondsElapsed;
        TimeSpan timeSpan = TimeSpan.FromSeconds(secondsElapsed);
        string timeText = string.Format("{3:d}d:{0:D2}h:{1:D2}m:{2:D2}s", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Days);
        timerText.GetComponent<Text>().text = timeText;
	}
}
