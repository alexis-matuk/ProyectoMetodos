using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Spawner : MonoBehaviour {
    [SerializeField]
    GameObject user;
    [SerializeField]
    GameObject truck;

    public float peoplePerHour;
    public float trucksPerHour;

    bool invokedPeople;
    bool invokedTrucks;

    int peopleCounter;
    int truckCounter;

    float end;
    float start;

    bool ended;
    bool saved;

    UnityRandom urand;

    [SerializeField]
    GameObject endPanel;

    void Start()
    {
        peoplePerHour = VariableManager.instance.getArrivalRate(VariableManager.instance.arrivalRate);
        trucksPerHour = VariableManager.instance.getArrivalRate(VariableManager.instance.truckArrivalRate);
        invokedPeople = false;
        invokedTrucks = false;
        peopleCounter = 0;
        truckCounter = 0;
        urand = new UnityRandom();

        end = 0;
        start = VariableManager.instance.secondsElapsed;

        ended = false;
        saved = false;
    }

    void Update()
    {
        if (!invokedPeople)
        {
            InvokeRepeating("SpawnUser", 0f, (3600f / (VariableManager.instance.timeMultiplier * peoplePerHour)));
            invokedPeople = true;
        }

        if (!invokedTrucks)
        {
            InvokeRepeating("SpawnTruck", 0f, (3600f / (VariableManager.instance.timeMultiplier * trucksPerHour)));
            invokedTrucks = true;
        }

        end = VariableManager.instance.secondsElapsed;
        if (end - start >= 3600)
        {
            start = end;
            CancelInvoke("SpawnTruck");
            trucksPerHour = VariableManager.instance.getArrivalRate(VariableManager.instance.truckArrivalRate);
            invokedTrucks = false;
            truckCounter = 0;

            CancelInvoke("SpawnUser");
            peoplePerHour = VariableManager.instance.getArrivalRate(VariableManager.instance.arrivalRate);
            invokedPeople = false;
            peopleCounter = 0;
        }

        if ((VariableManager.instance.secondsElapsed / 3600f) >= VariableManager.instance.totalHoursToRun)
        {
            if (!ended)
            {
                gameObject.GetComponent<Timer>().enabled = false;
                gameObject.GetComponent<Limpieza>().enabled = false;
                Time.timeScale = 0;
                endPanel.SetActive(true);
                ended = true;
            }
        }
    }

    public void selectSave(bool _save)
    {
        if (_save && !saved)
        {
            saved = true;
            FileWriter.instance.saveFile();
        }
        endPanel.SetActive(false);
    }

    void SpawnTruck()
    {
        Instantiate(truck);
        truckCounter++;
    }

    void SpawnUser()
    {        
        Instantiate(user);
        peopleCounter++;
    }
}
