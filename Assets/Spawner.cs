using UnityEngine;
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

    bool saved;

    UnityRandom urand;

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
            if (!saved)
            {
                FileWriter.instance.saveFile();
                Time.timeScale = 0;
                Debug.Log("FINISHED!!!");
                saved = true;
            }
        }
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
