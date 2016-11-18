using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Timer : MonoBehaviour {

    [SerializeField]
    GameObject timerText;
    public float secondsElapsed;
	// Update is called once per frame
	void Update () {
        secondsElapsed = Time.realtimeSinceStartup * VariableManager.instance.timeMultiplier;
        VariableManager.instance.secondsElapsed = secondsElapsed;
        TimeSpan timeSpan = TimeSpan.FromSeconds(secondsElapsed);
        string timeText = string.Format("{3:d}d:{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Days);
        timerText.GetComponent<Text>().text = timeText;
	}
}
