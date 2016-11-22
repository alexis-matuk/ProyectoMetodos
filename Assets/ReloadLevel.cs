using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReloadLevel : MonoBehaviour {
    
    public GameObject AcceptSave;
    public GameObject Overlay;
    public GameObject Map;
    public GameObject transitionManager;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Grid")
        {
            AcceptSave = GameObject.Find("AcceptSave");
            Overlay = GameObject.Find("Overlay");
            Map = GameObject.Find("Map");
            transitionManager = GameObject.Find("TransitionManager");

            Time.timeScale = 1;
            AcceptSave.SetActive(false);
            Map.SetActive(false);
            Overlay.SetActive(true);
            transitionManager.GetComponent<Timer>().enabled = true;
            transitionManager.GetComponent<Limpieza>().enabled = true;
        }
    }

    void OnLevelWasLoaded()
    {
        if (SceneManager.GetActiveScene().name == "Grid")
        {
            Debug.Log("Reloaded");
            transitionManager.GetComponent<Timer>().enabled = true;
            transitionManager.GetComponent<Limpieza>().enabled = true;
            Time.timeScale = 1;
            AcceptSave.SetActive(false);
            Map.SetActive(false);
            Overlay.SetActive(true);
        }
    }

}
