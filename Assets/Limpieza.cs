﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Limpieza : MonoBehaviour {

    float endSeconds;
    float startSeconds;
    UnityRandom urand;

    AreaManager manager;

    Text cleaningText;
    bool cleaning;

    float accidenteStart;
    float accidenteEnd;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Grid")
        {
            manager = GameObject.Find("Compras").GetComponent<AreaManager>();
            cleaningText = GameObject.Find("Compras").transform.GetChild(3).GetComponent<Text>();
            cleaningText.gameObject.SetActive(false);
            endSeconds = 0;
            startSeconds = VariableManager.instance.secondsElapsed;
            urand = new UnityRandom();
            cleaning = false;
        }
    }

    void OnLevelWasLoaded () 
    { 
        if (SceneManager.GetActiveScene().name == "Grid")
        {
            manager = GameObject.Find("Compras").GetComponent<AreaManager>();
            cleaningText = GameObject.Find("Compras").transform.GetChild(3).GetComponent<Text>();
            cleaningText.gameObject.SetActive(false);
            endSeconds = 0;
            startSeconds = VariableManager.instance.secondsElapsed;
            urand = new UnityRandom();
            cleaning = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        endSeconds = VariableManager.instance.secondsElapsed;
        if (endSeconds - startSeconds >= 3600)
        {            
            startSeconds = endSeconds;
            float Accidente = urand.Value();
            if (Accidente <= VariableManager.instance.ProbabilidadAccidente)
            {                
                float cleanUpTime = VariableManager.instance.getServiceRate("Limpieza");
                manager.addCleaningTime(cleanUpTime);
                accidenteStart = VariableManager.instance.secondsElapsed;
                if (!cleaning)
                    StartCoroutine(clean(cleanUpTime));
            }
        } 
	}

    IEnumerator clean(float time)
    {
        cleaning = true;
        cleaningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(time / VariableManager.instance.timeMultiplier);
        cleaningText.gameObject.SetActive(false);
        cleaning = false;
    }
}
