using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CashierSetup : MonoBehaviour {

    Transform cashiers;

    Transform packing;
	// Use this for initialization

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Grid")
        {
            cashiers = GameObject.Find("Cajas").transform;
            packing = GameObject.Find("Embolsadoras").transform;
            for (int i = 0; i < cashiers.childCount; i++)
            {
                if (i >= VariableManager.instance.openCashiers)
                {
                    cashiers.GetChild(i).gameObject.SetActive(false);
                    packing.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    void OnLevelWasLoaded () { 
        if (SceneManager.GetActiveScene().name == "Grid")
        {
            cashiers = GameObject.Find("Cajas").transform;
            packing = GameObject.Find("Embolsadoras").transform;
            for (int i = 0; i < cashiers.childCount; i++)
            {
                if (i >= VariableManager.instance.openCashiers)
                {
                    cashiers.GetChild(i).gameObject.SetActive(false);
                    packing.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
