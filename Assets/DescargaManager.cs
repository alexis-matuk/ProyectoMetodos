using UnityEngine;
using System.Collections;

public class DescargaManager : MonoBehaviour {
    public bool isUnloading;
    public GameObject currentTruck;
    void Start()
    {
        isUnloading = false;
        currentTruck = null;
    }

    public void setCurrentTruck(GameObject _current)
    {
        currentTruck = _current;
    }

    void Update()
    {
        if (currentTruck != null)
        {            
            if (currentTruck.GetComponent<Truck>().waitingTime <= 0)
            {
                currentTruck = null;
                isUnloading = false;
            }
        }
    }

    public void UnloadTruck(GameObject truck)
    {
        truck.transform.SetParent(transform.Find("ItemsContainer").transform.Find("ItemsPanel").transform, true);
        truck.transform.localScale = new Vector2(1,1);
        float time = VariableManager.instance.getServiceRate(transform.tag);
        truck.GetComponent<Truck>().addWaitingTime(time);
        truck.GetComponent<Truck>().startTimer();
        truck.GetComponent<Truck>().setNextTarget("Salida");
        currentTruck = truck;
        isUnloading = true;
    }
}
