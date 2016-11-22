using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class ShowStats : MonoBehaviour {
    public static ShowStats instance = null;

    public GameObject PanelStats;

    public GameObject MapOverlay;
	// Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);           
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Grid")
        {
            PanelStats = GameObject.Find("LevelManager").GetComponent<ReloadLevel>().Overlay;
            MapOverlay = GameObject.Find("LevelManager").GetComponent<ReloadLevel>().Map;
        }
    }
        
    void OnLevelWasLoaded () {          
        if (SceneManager.GetActiveScene().name == "Grid")
        {
            PanelStats = GameObject.Find("LevelManager").GetComponent<ReloadLevel>().Overlay;
            MapOverlay = GameObject.Find("LevelManager").GetComponent<ReloadLevel>().Map;
        }
	}

    public void setStats(GameObject _button)
    {    
        string _stats = "";
        if (_button != null && _button.GetComponent<Truck>() == null)
        {
            User button = _button.GetComponent<User>();
            _stats += "id: " + button.id.ToString() + "\n";

            if (button.FarmaciaBool)
                _stats += "Farmacia: true\n";
            else
                _stats += "Farmacia: false\n";

            if (button.SalchichasBool)
                _stats += "Salchichonería: true\n";
            else
                _stats += "Salchichonería: false\n";

            if (button.CarnesBool)
                _stats += "Carnes: true\n";
            else
                _stats += "Carnes: false\n";

            if (button.AtencionClienteBool)
                _stats += "Servicio al Cliente: true\n";
            else
                _stats += "Servicio al Cliente: false\n";

            if (button.CompraBool)
                _stats += "Compras: true\n";
            else
                _stats += "Compras: false\n";

            TimeSpan timeSpan = TimeSpan.FromSeconds(button.waitingTime);
            string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            _stats += "Tiempo de espera: " + timeText + "\n";

            if (button.CompraBool)
                _stats += "Artículos: " + button.numberOfItems.ToString() + "\n";
        }
        else if(_button != null && _button.GetComponent<Truck>() != null)
        {
            Truck button = _button.GetComponent<Truck>();
            _stats += "id: " + button.id.ToString() + "\n";
            TimeSpan timeSpan = TimeSpan.FromSeconds(button.waitingTime);
            string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            _stats += "Tiempo de espera: " + timeText + "\n";
        }
        PanelStats.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = _stats;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PanelStats.SetActive(!PanelStats.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            MapOverlay.SetActive(!MapOverlay.activeSelf);
        }
	}
}
