using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    [SerializeField]
    GameObject user;
    [SerializeField]
    GameObject truck;

    public float peoplePerHour;
    float trucksPerHour;

    bool invokedPeople;
    bool invokedTrucks;

    public int peopleCounter;
    public int truckCounter;

    void Start()
    {
        peoplePerHour = VariableManager.instance.getArrivalRate(VariableManager.instance.arrivalRate);
        trucksPerHour = VariableManager.instance.getArrivalRate(VariableManager.instance.truckArrivalRate);
        invokedPeople = false;
        peopleCounter = 0;
    }

    void Update()
    {
        if (!invokedPeople)
        {
            InvokeRepeating("SpawnUser", 0f, (3600f / VariableManager.instance.timeMultiplier) / peoplePerHour);
            invokedPeople = true;
        }

        if (peopleCounter == peoplePerHour)
        {
            peoplePerHour = VariableManager.instance.getArrivalRate(VariableManager.instance.arrivalRate);
            invokedPeople = false;
            peopleCounter = 0;
        }

        if (!invokedTrucks)
        {
            InvokeRepeating("SpawnTruck", 0f, (3600f / VariableManager.instance.timeMultiplier) / trucksPerHour);
            invokedTrucks = true;
        }

        if (truckCounter == trucksPerHour)
        {
            trucksPerHour = VariableManager.instance.getArrivalRate(VariableManager.instance.truckArrivalRate);
            invokedTrucks = false;
            truckCounter = 0;
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
